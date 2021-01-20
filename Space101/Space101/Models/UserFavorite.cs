using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Space101.Models
{
    public class UserFavorite
    {
        public int UserFavoriteID { get; private set; }

        [Required]
        public int FlightID { get; private set; }

        [Required]
        public string ApplicationUserID { get; private set; }

        public ApplicationUser ApplicationUser { get; private set; }
        public Flight Flight { get; private set; }

        protected UserFavorite() { }

        private UserFavorite(string userId, int flightId)
        {
            FlightID = flightId;
            ApplicationUserID = userId;
        }

        public static UserFavorite Create(string userId, int flightId)
        {
            return new UserFavorite(userId, flightId);
        }

    }
}