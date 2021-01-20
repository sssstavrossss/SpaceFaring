using AutoMapper;
using Space101.DAL;
using Space101.Dtos;
using Space101.Models;
using Space101.Persistence;
using Space101.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Space101.Controllers.api
{
    public class UsersFlightsController : ApiController
    {
        private ApplicationDbContext context;
        private FlightRepository flightRepository;
        private FlightStatusRepository flightStatusRepository;

        public UsersFlightsController()
        {
            context = new ApplicationDbContext();
            flightRepository = new FlightRepository(context);
            flightStatusRepository = new FlightStatusRepository(context);
        }
        protected override void Dispose(bool disposing)
        {
            context.Dispose();
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetAll()
        {
            var flights = flightRepository.GetOnScheduleFlights();
            var flightStatusesDto = flightStatusRepository.GetFlightStatuses()
                .Select(Mapper.Map<FlightStatus, FlightStatusDto>).ToList();

            var flightsDto = flights
                .Where(f => f.Date >=  DateTime.Now)
                .Select(Mapper.Map<Flight, FlightDto>).ToList();

            flightsDto.ForEach(dto => dto.ConvertDate());
            flightsDto.ForEach(dto => dto.FlightStatuses = flightStatusesDto);

            return Ok(flightsDto);
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetChangedFlight(int? id)
        {
            if (id == null || id == 0)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var flight = flightRepository.GetSingleFlight(id.Value);

            if (flight == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            if (flight.Date < DateTime.Now)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var flightDto = Mapper.Map<Flight, FlightDto>(flight);

            flightDto.ConvertDate();

            return Ok(flightDto);
        }

        [HttpPost]
        [AllowAnonymous]
        public IHttpActionResult GetFiltered(FlightSearchDto apiObject)
        {
            List<Flight> flights = new List<Flight>();

            apiObject.FixDates();

            if (apiObject.NoVipBool && !apiObject.VipBool)
                flights = getNoVipFlights(apiObject);
            else if (!apiObject.NoVipBool && apiObject.VipBool)
                flights = getVipFlights(apiObject);
            else
                flights = getBothVipFlights(apiObject);

            var flightsDto = flights.Select(Mapper.Map<Flight, FlightDto>).ToList();

            flightsDto.ForEach(dto => dto.ConvertDate());

            return Ok(flightsDto);
        }

        private List<Flight> getNoVipFlights(FlightSearchDto apiObject)
        {
            return flightRepository.GetOnScheduleFlights()
                .Where(
                    f => f.IsVIP == false &&
                    f.BasePrice >= apiObject.MinPrice &&
                    f.BasePrice <= apiObject.MaxPrice &&
                    f.Date <= apiObject.DateTo &&
                    f.Date >= apiObject.DateFrom &&
                    f.FlightPath.Destination.Name.Contains(apiObject.DestinationPlanet) &&
                    f.FlightPath.Departure.Name.Contains(apiObject.DeparturePlanet)).ToList();
        }

        private List<Flight> getVipFlights(FlightSearchDto apiObject)
        {
            return flightRepository.GetOnScheduleFlights()
                .Where(
                    f => f.IsVIP == true &&
                    f.BasePrice >= apiObject.MinPrice &&
                    f.BasePrice <= apiObject.MaxPrice &&
                    f.Date <= apiObject.DateTo &&
                    f.Date >= apiObject.DateFrom &&
                    f.FlightPath.Destination.Name.Contains(apiObject.DestinationPlanet) &&
                    f.FlightPath.Departure.Name.Contains(apiObject.DeparturePlanet)).ToList();
        }

        private List<Flight> getBothVipFlights(FlightSearchDto apiObject)
        {
            return flightRepository.GetOnScheduleFlights()
                .Where(
                    f => f.BasePrice >= apiObject.MinPrice &&
                    f.BasePrice <= apiObject.MaxPrice &&
                    f.Date <= apiObject.DateTo &&
                    f.Date >= apiObject.DateFrom &&
                    f.FlightPath.Destination.Name.Contains(apiObject.DestinationPlanet) &&
                    f.FlightPath.Departure.Name.Contains(apiObject.DeparturePlanet)).ToList();
        }

    }
}
