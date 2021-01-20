using Microsoft.AspNet.SignalR;
using Space101.DAL;
using Space101.Dtos;
using Space101.Hubs;
using Space101.Models;
using Space101.ViewModels.FlightViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Space101.Hub_Services
{
    public class FlightHubService
    {
        public FlightHubService()
        { }

        public void FlightCreated(FlightDto flight)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<FlightHub>();
            if (hubContext != null)
            {
                hubContext.Clients.All.flightCreated(flight);
            }
        }

        public void FlightUpdated(FlightDto flight)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<FlightHub>();
            if (hubContext != null)
            {
                hubContext.Clients.All.flightUpdated(flight);
            }
        }

        public void FlightDeleted(int id)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<FlightHub>();
            if (hubContext != null)
            {
                hubContext.Clients.All.flightDeleted(id);
            }
        }

        public void FligthStatusChanged(int flightId, int statusId)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<FlightHub>();
            if (hubContext != null)
            {
                hubContext.Clients.All.flightStatusChanged(flightId, statusId);
            }
        }

        public void FlightSeatsClosed(long[] seatIds)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<FlightHub>();
            if (hubContext != null)
            {
                hubContext.Clients.All.flightSeatsClosed(seatIds);
            }
        }

        public void FlightSeatsOpened(long[] seatIds)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<FlightHub>();
            if (hubContext != null)
            {
                hubContext.Clients.All.flightSeatsOpened(seatIds);
            }
        }
    }
}