using Space101.ViewModels.PlanetViewModels;
using System.Collections.Generic;
using System.Linq;
using Space101.ViewModels.RaceViewModels;

namespace Space101.ViewModels.TicketViewModels
{
    public class TicketFormContainer
    {

        public IEnumerable<LightRaceViewModel> Races { get; set; }
        public IEnumerable<LightPlanetViewModel> Planets { get; set; }
        public List<TicketFormViewModel> TicketFormViewModels { get; set; }

        public TicketFormContainer()
        {
            Races = new List<LightRaceViewModel>();
            Planets = new List<LightPlanetViewModel>();
            TicketFormViewModels = new List<TicketFormViewModel>();
        }

        public TicketFormContainer(IEnumerable<LightRaceViewModel> races, IEnumerable<LightPlanetViewModel> planets)
        {
            Races = races;
            Planets = planets;
            TicketFormViewModels = new List<TicketFormViewModel>();
        }

        public TicketFormContainer(IEnumerable<TicketFormViewModel> tickets, IEnumerable<LightRaceViewModel> races, IEnumerable<LightPlanetViewModel> planets)
        {
            Races = races;
            Planets = planets;
            TicketFormViewModels = tickets.ToList();
        }

        public decimal GetTotalPrice()
        {
            decimal total = 0;
            foreach (var ticket in TicketFormViewModels)
            {
                total += ticket.Price;
            }
            return total;
        }
    }
}