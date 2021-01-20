using Space101.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Space101.ViewModels.UserFavoriteViewModels
{
    public class UserFavoriteViewModel
    {
        public int UserFavoriteID { get; set; }
        public int FlightID { get; set; }
        public string ApplicationUserID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:G}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal BasePrice { get; set; }
        public Planet Destination { get; set; }
        public Planet Departure { get; set; }
        public Starship Starship { get; set; }
        public bool IsVIP { get; set; }

        public UserFavoriteViewModel() { }

        public UserFavoriteViewModel(UserFavorite favorite) 
        {
            UserFavoriteID = favorite.UserFavoriteID;
            FlightID = favorite.FlightID;
            ApplicationUserID = favorite.ApplicationUserID;
            Date = favorite.Flight.Date;
            BasePrice = favorite.Flight.BasePrice;
            Destination = favorite.Flight.FlightPath.Destination;
            Departure = favorite.Flight.FlightPath.Departure;
            Starship = favorite.Flight.Starship;
            IsVIP = favorite.Flight.IsVIP;
        }

        public static UserFavoriteViewModel Create(UserFavorite favorite)
        {
            return new UserFavoriteViewModel(favorite);
        }
    }
}