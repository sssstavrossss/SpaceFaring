using System.Collections.Generic;
using System.Web.Mvc;
using Space101.DAL;
using Space101.Helper_Models;
using Space101.Repositories;
using Space101.ViewModels.TicketViewModels;

namespace Space101.Controllers
{
    [Authorize(Roles = AvailableRoles.Admin + "," + AvailableRoles.DatabaseManager)]
    public class TicketController : Controller
    {
        private ApplicationDbContext context;
        private TicketRepository ticketRepository;

        public TicketController()
        {
            context = new ApplicationDbContext();
            ticketRepository = new TicketRepository(context);
        }

        protected override void Dispose(bool disposing)
        {
            context.Dispose();
        }

        // GET: Ticket
        public ActionResult Index()
        {
            var tickets = ticketRepository.GetAllTickets();

            var viewModel = new List<DisplayTicketViewModel>();
            tickets.ForEach(t => viewModel.Add(new DisplayTicketViewModel(t)));

            return View(viewModel);
        }
    }
}