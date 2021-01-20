using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Space101.Dtos
{
    public class UserFavoriteDto
    {
        public int UserFavoriteID { get; set; }
        public int FlightID { get; set; }
        public string ApplicationUserID { get; set; }
    }
}