using System;
using System.ComponentModel.DataAnnotations;

namespace Space101.ViewModels.OrderViewModels
{
    public class DisplayOrderViewModel
    {
        [Display(Name = "Order Id")]
        public long OrderId { get; private set; }

        [Display(Name = "Email")]
        public string BuyerEmail { get; private set; }

        [Display(Name = "User Id")]
        public string UserId { get; private set; }

        [Display(Name = "Total Price")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal TotalPrice { get; private set; }

        [Display(Name = "Invoice Id")]
        public string InvoiceId { get; private set; }

        [Display(Name = "Creation Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:G}")]
        public DateTime CreationDate { get; set; }

        protected DisplayOrderViewModel()
        { }

        public DisplayOrderViewModel(Models.Order order)
        {
            OrderId = order.OrderId;
            BuyerEmail = order.BuyerEmail;
            UserId = order.UserId;
            TotalPrice = order.TotalPrice;
            InvoiceId = order.InvoiceId;
            CreationDate = order.CreationDate;
        }
    }
}