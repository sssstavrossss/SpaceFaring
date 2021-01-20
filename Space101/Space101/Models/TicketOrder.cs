using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Space101.Models
{
    public class TicketOrder
    {
        [Key]
        [ForeignKey("Order")]
        [Column(Order = 0)]
        public long OrderId { get; private set; }
        public Order Order { get; private set; }

        [Key]
        [ForeignKey("Ticket")]
        [Column(Order = 1)]
        public long TicketId { get; private set; }
        public Ticket Ticket { get; private set; }

        public byte Quantity { get; private set; }

        protected TicketOrder()
        { }

        public TicketOrder(Ticket ticket, long orderId)
        {
            Quantity = 1;
            TicketId = ticket.TicketId;
            OrderId = orderId;
            Ticket = ticket;
        }
    }
}