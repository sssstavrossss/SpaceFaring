using Microsoft.AspNet.Identity;
using Space101.DAL;
using Space101.Models;
using Space101.Persistence;
using Space101.Repositories;
using Space101.ViewModels.OrderViewModels;
using Space101.ViewModels.PlanetViewModels;
using Space101.ViewModels.TicketViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using PayPal.Api;
using Space101.PaymentConfiguration;
using System.Globalization;
using Space101.ViewModels.RaceViewModels;
using Space101.Hub_Services;
using IronPdf;
using Space101.Services;
using Space101.Helper_Models;
using System.Net;

namespace Space101.Controllers
{
    [AllowAnonymous]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly TicketRepository ticketRepository;
        private readonly OrderRepository orderRepository;
        private readonly RaceRepository raceRepository;
        private readonly PlanetRepository planetRepository;
        private readonly FlightSeatRepository flightSeatRepository;
        private readonly UnitOfWork unitOfWork;

        public OrderController()
        {
            context = new ApplicationDbContext();
            ticketRepository = new TicketRepository(context);
            orderRepository = new OrderRepository(context);
            raceRepository = new RaceRepository(context);
            planetRepository = new PlanetRepository(context);
            flightSeatRepository = new FlightSeatRepository(context);
            unitOfWork = new UnitOfWork(context);
        }
        protected override void Dispose(bool disposing)
        {
            context.Dispose();
        }

        [Authorize(Roles = AvailableRoles.Admin + "," + AvailableRoles.DatabaseManager)]
        public ActionResult Index()
        {
            var orders = orderRepository.GetOrders();

            var viewModel = new List<DisplayOrderViewModel>();

            orders.ForEach(o => viewModel.Add(new DisplayOrderViewModel(o)));

            return View(viewModel);
        }

        public ActionResult Create()
        {
            int refreshInterval = Convert.ToInt32(TempData["RefreshInterval"]);
            ViewBag.RefreshInterval = refreshInterval + 1;

            if (Session["flight"] == null || Session["flightSeats"] == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            Flight flight = Session["flight"] as Flight;
            List<FlightSeat> flightSeats = Session["flightSeats"] as List<FlightSeat>;

            //For holding the seats when user refreshes the view of the form
            foreach (var flightSeat in flightSeats)
            {
                if (flightSeat.IsAvailable)
                {
                    flightSeat.HoldSeat();
                    flightSeatRepository.ModifySeats(flightSeat);
                }
            }
            unitOfWork.Complete();

            var flightSeatIds = flightSeats.Select(fs => fs.FlightSeatId).ToArray();
            var flightService = new FlightHubService();
            flightService.FlightSeatsClosed(flightSeatIds);

            var ticketViewModels = new List<TicketFormViewModel>();
            flightSeats.ForEach(fs => ticketViewModels.Add(new TicketFormViewModel(flight.FlightId, fs)));
            
            var ticketContainer = InitializeTicketContainer(ticketViewModels);
            var orderViewModel = new OrderTicketsFormViewModel(null,null,ticketContainer.GetTotalPrice());
            var viewModel = new OrderTicketFormContainerViewModel(orderViewModel, ticketContainer, flight);

            return View(viewModel);
        }
        private IEnumerable<LightRaceViewModel> InitializeRaces()
        {
            var races = raceRepository.GetRaces();

            var viewmodels = new List<LightRaceViewModel>();
            races.ForEach(r => viewmodels.Add(LightRaceViewModel.CreateFromModel(r)));

            return viewmodels;
        }
        private IEnumerable<LightPlanetViewModel> InitializePlanets()
        {
            var planets = planetRepository.GetPlanetsRaw();

            var viewmodels = new List<LightPlanetViewModel>();
            planets.ForEach(p => viewmodels.Add(LightPlanetViewModel.CreateFromModel(p)));

            return viewmodels;
        }
        private TicketFormContainer InitializeTicketContainer(List<TicketFormViewModel> ticketFormViewModels)
        {
            var races = InitializeRaces();
            var planets = InitializePlanets();
            return new TicketFormContainer(ticketFormViewModels, races, planets);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult ValidateOrder(OrderTicketsFormViewModel OrderTicketsFormViewModel, List<TicketFormViewModel> ticketFormViewModels)
        {
            if (!ModelState.IsValid)
            {
                //The refresh interval is used for preventing the user refresh the order page. Here is initialized ia a valid state.
                TempData["RefreshInterval"] = -1;
                Flight flight = Session["flight"] as Flight;
                var ticketContainer = InitializeTicketContainer(ticketFormViewModels);
                var viewModel = new OrderTicketFormContainerViewModel(OrderTicketsFormViewModel, ticketContainer, flight);
                return View("Create", viewModel);
            }

            string userId = null;
            if (User.Identity.IsAuthenticated)
                userId = User.Identity.GetUserId();

            var tickets = new List<Ticket>();
            ticketFormViewModels.ForEach(tv => tickets.Add(new Ticket(tv)));

            var ticketOrders = new List<TicketOrder>();
            tickets.ForEach(t => ticketOrders.Add(new TicketOrder(t, OrderTicketsFormViewModel.OrderId)));

            var order = new Models.Order(OrderTicketsFormViewModel.BuyerEmail, userId, ticketOrders);

            Session["order"] = order;
            Session["tickets"] = tickets;

            return RedirectToAction("PaymentWithPaypal");
        }
        public ActionResult SaveOrder(string invoiceId)
        {
            var tickets = Session["tickets"] as List<Ticket>;
            var order = Session["order"] as Models.Order;

            order.SetInvoiceId(invoiceId);

            ticketRepository.AddMultiple(tickets);
            orderRepository.Add(order);

            unitOfWork.Complete();

            ClearOrderSession();

            return RedirectToAction("PrintOrderData", "Order", new {Id = order.OrderId });
        }

        public ActionResult PrintOrderData(long? Id)
        {
            if (Id == null)
                return new HttpNotFoundResult();

            var order = orderRepository.GetFullOrderById(Id.Value);

            if (order.UserId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();

            var viewModel = PrintOrderViewModel.CreateViewModel(order);

            return View(viewModel);
        }

        public ActionResult PrintPdf(long? OrderId)
        {
            if (OrderId == null)
                return new HttpNotFoundResult();

            var orderHs = orderRepository.GetOrderIDHasSet();

            if (!orderHs.Contains(OrderId.Value))
                return new HttpNotFoundResult();

            var order = orderRepository.GetFullOrderById(OrderId.Value);

            if (order.UserId == User.Identity.GetUserId() || User.IsInRole("ADMIN"))
            {
                var viewModel = PrintOrderViewModel.CreateViewModel(order);
                var Renderer = new HtmlToPdf();
                string filePath = Server.MapPath(Url.Content("~/Content/bootstrapPDF.css"));
                Renderer.PrintOptions.CustomCssUrl = filePath;
                var PDF = Renderer.RenderHtmlAsPdf(GeneratePDFHtml.ReturnHtml(viewModel));
                return File(PDF.BinaryData, "application/pdf", $"SpaceFaring--{order.InvoiceId}.Pdf");

            } else
                return new HttpNotFoundResult();

        }

        public ActionResult ReleaseOrder()
        {
            var tempFlight = Session["flight"] as Flight;
            List<FlightSeat> flightSeats = Session["flightSeats"] as List<FlightSeat>;
            flightSeats.ForEach(s => s.ReleaseSeat());
            foreach (var flightSeat in flightSeats)
            {
                context.Entry(flightSeat).State = EntityState.Modified;
            }

            context.SaveChanges();

            var flightSeatIds = flightSeats.Select(fs => fs.FlightSeatId).ToArray();
            var flightService = new FlightHubService();
            flightService.FlightSeatsOpened(flightSeatIds);

            ClearOrderSession();

            return RedirectToAction("ChooseSeats", "Flight", new { id = tempFlight.FlightId });
        }

        private void ClearOrderSession()
        {
            Session.Remove("flight");
            Session.Remove("flightSeats");
            Session.Remove("tickets");
            Session.Remove("order");
        }

        // * * * * * * * * * * * * * * * * * PayMent Methods * * * * * * * * * * * * * * * * * * * * 
        //PayPal

        private Payment payment;
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            this.payment = new Payment()
            {
                id = paymentId
            };
            return this.payment.Execute(apiContext, paymentExecution);
        }
        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            //create itemlist and add item objects to it  
            var itemList = new ItemList()
            {
                items = new List<Item>()
            };
            
            var tickets = Session["tickets"] as List<Ticket>;
            var order = Session["order"] as Models.Order;
            var flight = Session["flight"] as Flight;

            //Adding Item Details like name, currency, price etc  
            foreach (var ticket in tickets)
            {
                itemList.items.Add(new Item()
                {
                    name = $"Ticket: From {flight.FlightPath.Departure.Name} - To {flight.FlightPath.Destination.Name} \nFlight Date: {flight.Date}",
                    currency = "USD",
                    price = ticket.Price.ToString("N2", CultureInfo.CreateSpecificCulture("en-US")),
                    quantity = "1",
                    sku = $"Seat: {ticket.SeatId}"
                });
            }

            var payer = new Payer()
            {
                payment_method = "paypal"
            };

            // Configure Redirect Urls here with RedirectUrls object  
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl
            };

            //Adding Tax, shipping and Subtotal details
            var details = new Details()
            {
                tax = "0",
                shipping = "0",
                subtotal = order.TotalPrice.ToString("N2",CultureInfo.CreateSpecificCulture("en-US"))
            };

            var amount = new Amount()
            {
                currency = "USD", //TODO In future -> more currencies
                total = details.subtotal /*(Convert.ToDouble(details.tax) + Convert.ToDouble(details.shipping) + Convert.ToDouble(details.subtotal)).ToString("N2", CultureInfo.CreateSpecificCulture("en-US"))*/, // Total must be equal to sum of tax, shipping and subtotal.  
                details = details
            };
            var transactionList = new List<Transaction>();

            // Adding description about the transaction  
            transactionList.Add(new Transaction()
            {
                description = "Ticket Purchase",
                invoice_number = Guid.NewGuid().ToString(),
                amount = amount,
                item_list = itemList
            });

            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };

            // Create a payment using a APIContext  
            return this.payment.Create(apiContext);
        }
        public ActionResult PaymentWithPaypal(string Cancel = null)
        {
            string orderBill = "";
            //getting the apiContext  
            APIContext apiContext = PaypalConfiguration.GetAPIContext();
            try
            { 
                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/Order/PaymentWithPayPal?";
                    var guid = Guid.NewGuid().ToString();
                    //CreatePayment function gives us the payment approval url on which payer is redirected for paypal account payment  
                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);
                    //get links returned from paypal in response to Create function call  
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;
                    while (links.MoveNext())
                    {
                        Links link = links.Current;
                        if (link.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment  
                            paypalRedirectUrl = link.href;
                        }
                    }
                    // saving the paymentID in the key guid  
                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                { 
                    var guid = Request.Params["guid"];
                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                    orderBill = executedPayment.transactions.First().invoice_number;
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return RedirectToAction("ReleaseOrder", "Order");
                    }
                    Session.Remove(guid);
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("ReleaseOrder", "Order");
            }
            //on successful payment, redirect to save order action.  
            return RedirectToAction("SaveOrder","Order", new { invoiceId = orderBill });
        }

    }
}