using Space101.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Space101.Dtos
{
    public class UserFavoritesManipulateDto
    {
        public int UserFavoriteID { get; set; }
        public string ApplicationUserID { get; set; }
        public int FlightID { get; set; }

        public UserFavoritesManipulateDto() { }

        public UserFavoritesManipulateDto(UserFavorite favorites)
        {

        }

        public static List<UserFavoritesManipulateDto> GetList(List<UserFavorite> favorites, string id)
        {
            var DtoList = new List<UserFavoritesManipulateDto>();

            favorites.ForEach(f => DtoList.Add(new UserFavoritesManipulateDto
            {
                ApplicationUserID = id,
                FlightID = f.FlightID,
                UserFavoriteID = f.UserFavoriteID
            }));

            return DtoList;
        }

    }
}