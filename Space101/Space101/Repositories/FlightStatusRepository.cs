using Space101.DAL;
using Space101.Models;
using System.Collections.Generic;
using System.Linq;

namespace Space101.Repositories
{
    public class FlightStatusRepository
    {
        private ApplicationDbContext context;

        public FlightStatusRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<FlightStatus> GetFlightStatuses()
        {
            return context.FlightStatuses
                .ToList();
        }
    }
}