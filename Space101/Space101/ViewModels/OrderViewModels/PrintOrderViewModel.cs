using Space101.Models;
using Space101.ViewModels.TicketViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Space101.ViewModels.OrderViewModels
{
    public class PrintOrderViewModel
    {
        public long OrderId { get; set; }
        public string BuyerEmail { get; set; }
        public string InvoiceId { get; set; }
        public DateTime CreationDate { get; set; }
        public decimal TotalPrice { get; set; }
        public int FlightId { get; set; }
        public bool isVip { get; set; }
        public ICollection<PrintTicketViewModel> Tickets { get; set; }

        public PrintOrderViewModel()
        {
            Tickets = new List<PrintTicketViewModel>();
        }

        public PrintOrderViewModel(Order order, List<PrintTicketViewModel> ticketsViewModel)
        {
            OrderId = order.OrderId;
            InvoiceId = order.InvoiceId;
            CreationDate = order.CreationDate;
            BuyerEmail = order.BuyerEmail;
            FlightId = order.Tickets.FirstOrDefault(ts => ts.OrderId == OrderId).Ticket.FlightId;
            Tickets = ticketsViewModel;
            isVip = order.Tickets.FirstOrDefault(ts => ts.OrderId == OrderId).Ticket.Flight.IsVIP;
            SetTotalPrice(Tickets);
        }

        public static PrintOrderViewModel CreateViewModel(Order order)
        {
            var ticketsViewModel = new List<PrintTicketViewModel>();
            var ticketsModel = order.Tickets.Select(ts => ts.Ticket).ToList();
            ticketsModel.ForEach(t => ticketsViewModel.Add(PrintTicketViewModel.CreateTicket(t)));

            return new PrintOrderViewModel(order, ticketsViewModel);
        }

        private void SetTotalPrice(ICollection<PrintTicketViewModel> tickets)
        {
            TotalPrice = 0;
            foreach (var ticket in tickets)
            {
                TotalPrice += ticket.Price;
            }
        }
    }
}