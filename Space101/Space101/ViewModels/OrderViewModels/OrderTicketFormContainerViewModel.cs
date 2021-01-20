using Space101.Models;
using Space101.ViewModels.TicketViewModels;

namespace Space101.ViewModels.OrderViewModels
{
    public class OrderTicketFormContainerViewModel
    {
        public OrderTicketsFormViewModel OrderTicketsFormViewModel { get; set; }
        public TicketFormContainer TicketContainer { get; set; }
        public Flight Flight { get; set; }

        public OrderTicketFormContainerViewModel()
        { }

        public OrderTicketFormContainerViewModel(OrderTicketsFormViewModel orderTicketsFormViewModel, TicketFormContainer ticketContainer, Flight flight)
        {
            OrderTicketsFormViewModel = orderTicketsFormViewModel;
            TicketContainer = ticketContainer;
            Flight = flight;
        }
    }
}