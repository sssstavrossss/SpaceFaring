using Microsoft.AspNet.Identity;
using Space101.DAL;
using Space101.Dtos;
using Space101.Models;
using Space101.Persistence;
using Space101.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Space101.Controllers.api
{
    [Authorize]
    public class UserFavoritesController : ApiController
    {
        private ApplicationDbContext context;
        private readonly UserFavoriteRepository userFavoritesRepository;
        private readonly ApplicationUserRepository applicationUserRepository;
        private readonly FlightRepository flightRepository;
        private readonly UnitOfWork unitOfWork;

        public UserFavoritesController()
        {
            context = new ApplicationDbContext();
            userFavoritesRepository = new UserFavoriteRepository(context);
            applicationUserRepository = new ApplicationUserRepository(context);
            flightRepository = new FlightRepository(context);
            unitOfWork = new UnitOfWork(context);
        }

        protected override void Dispose(bool disposing)
        {
            context.Dispose();
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            string id = User.Identity.GetUserId();

            var favorites = userFavoritesRepository.GetSimpleUserFavorites(id);

            var favoritesDto = UserFavoritesManipulateDto.GetList(favorites, id);

            return Ok(favoritesDto);
        }

        [HttpPost]
        public IHttpActionResult Manipulate(UserFavoritesManipulateDto favorite)
        {

            UserFavorite favoriteDB;
            string id = User.Identity.GetUserId();

            var favorites = new List<UserFavorite>();
            favorites = userFavoritesRepository.GetSimpleUserFavorites(id);
            var flightIDs = new HashSet<int>();

            if (favorites != null)
            {
                favorites.ForEach(f => flightIDs.Add(f.FlightID));
            }

            if (flightIDs.Count > 0 && flightIDs.Contains(favorite.FlightID))
            {
                favoriteDB = favorites.SingleOrDefault(f => f.FlightID == favorite.FlightID);
                userFavoritesRepository.Remove(favoriteDB);
                unitOfWork.Complete();
                return Ok();
            }

            favoriteDB = UserFavorite.Create(id, favorite.FlightID);
            userFavoritesRepository.Add(favoriteDB);
            unitOfWork.Complete();

            return Ok();
        }

    }
}
