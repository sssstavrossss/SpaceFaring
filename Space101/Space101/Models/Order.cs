using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Space101.Models
{
    public class Order
    {
        public long OrderId { get; private set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string BuyerEmail { get; private set; }

        public string UserId { get; private set; }
        public ApplicationUser User { get; private set; }

        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal TotalPrice { get; private set; }

        [Display(Name = "Invoice Id")]
        public string InvoiceId { get; private set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:G}", ApplyFormatInEditMode = true)]
        public DateTime CreationDate { get; set; }

        public ICollection<TicketOrder> Tickets { get; private set; }

        protected Order()
        {
            Tickets = new List<TicketOrder>();
            CreationDate = DateTime.Now;
        }

        public Order(string buyerEmail, string userId, ICollection<TicketOrder> tickets)
        {
            if(tickets == null)
                throw new ArgumentNullException("Tickets");
            BuyerEmail = buyerEmail;
            UserId = userId;
            SetTotalPrice(tickets);
            Tickets = tickets;
            CreationDate = DateTime.Now;
        }

        private void SetTotalPrice(ICollection<TicketOrder> ticketOrders)
        {
            TotalPrice = 0;
            foreach (var ticketOrder in ticketOrders)
            {
                TotalPrice += ticketOrder.Ticket.Price;
            }
        }

        public void SetInvoiceId(string invoiceId)
        {
            InvoiceId = invoiceId;
        }
    }
}