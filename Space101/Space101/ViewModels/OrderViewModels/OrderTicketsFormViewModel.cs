using System.ComponentModel.DataAnnotations;

namespace Space101.ViewModels.OrderViewModels
{
    public class OrderTicketsFormViewModel
    {
        public long OrderId { get; private set; }

        [Required(ErrorMessage = "The Email is required")]
        [Display(Name = "Email")]
        [EmailAddress]
        public string BuyerEmail { get; set; }
        public string UserId { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal TotalPrice { get; set; }

        public OrderTicketsFormViewModel()
        { }

        public OrderTicketsFormViewModel(string buyerEmail, string userId, decimal totalPrice)
        {
            BuyerEmail = buyerEmail ?? "";
            UserId = userId ?? "";
            TotalPrice = totalPrice;
        }
    }
}