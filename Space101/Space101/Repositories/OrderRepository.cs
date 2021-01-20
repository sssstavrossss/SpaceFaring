using Space101.DAL;
using Space101.Models;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace Space101.Repositories
{
    public class OrderRepository
    {
        private readonly ApplicationDbContext context;

        public OrderRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<Order> GetOrders()
        {
            return context.Orders
                .Include(o => o.Tickets.Select(t => t.Ticket))
                .ToList();
        }

        public List<Order> GetOrdersByUserId(string id)
        {
            return context.Orders
                .Where(o => o.UserId == id)
                .ToList();
        }

        public List<TicketOrder> GetTicketsOfOrder(long id)
        {
            return context.TicketOrders
                .Include(to => to.Ticket.Flight.FlightPath.Departure)
                .Include(to => to.Ticket.Flight.FlightPath.Destination)
                .Include(to => to.Ticket.PassengerRace)
                .Include(to => to.Ticket.PassengerPlanet)
                .Include(to => to.Ticket.Seat.TravelClass)
                .Where(to => to.OrderId == id)
                .ToList();
        }

        public Order GetFullOrderById(long Id)
        {
            return context.Orders
                .Include(o => o.Tickets.Select(t => t.Ticket).Select(tk => tk.Flight).Select(f => f.FlightPath).Select(fp => fp.Departure))
                .Include(o => o.Tickets.Select(t => t.Ticket).Select(tk => tk.Flight).Select(f => f.FlightPath).Select(fp => fp.Destination))
                .Include(o => o.Tickets.Select(t => t.Ticket).Select(tk => tk.Flight).Select(f => f.Starship))
                .Include(o => o.Tickets.Select(t => t.Ticket).Select(tk => tk.Seat))
                .SingleOrDefault(o => o.OrderId == Id);
        }

        public HashSet<long> GetOrderIDHasSet()
        {
            return context.Orders.Select(o => o.OrderId).ToHashSet();
        }

        public void RemoveMultipleOrders(IEnumerable<Order> orders)
        {
            foreach (var order in orders)
            {
                context.Entry(order).State = EntityState.Deleted;
            }
        }

        public long GetOrdersCount()
        {
            return context.Orders.Count();
        }

        public decimal GetTotalOrderRevenue()
        {
            var orders = context.Orders;
            decimal result = 0;
            if (orders.Count() > 0)
                result = orders.Sum(o => o.TotalPrice);

            return result;
        }

        public void Add(Order order)
        {
            context.Orders.Add(order);
        }

        public void Remove(Order order)
        {
            context.Orders.Remove(order);
        }
    }
}