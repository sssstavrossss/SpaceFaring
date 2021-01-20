using Microsoft.AspNet.Identity;
using Space101.DAL;
using Space101.Helper_Models;
using Space101.Persistence;
using Space101.Repositories;
using Space101.ViewModels.ApplicationUserViewModels;
using Space101.ViewModels.UserFavoriteViewModels;
using Space101.ViewModels.UserOrderViewModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Space101.Controllers
{
    public class UserController : Controller
    {
        private ApplicationDbContext context;
        private readonly ApplicationUserRepository applicationUserRepository;
        private readonly OrderRepository orderRepository;
        private readonly UnitOfWork unitOfWork;
        private readonly UserFavoriteRepository userFavoriteRepository;

        public UserController()
        {
            context = new ApplicationDbContext();
            applicationUserRepository = new ApplicationUserRepository(context);
            orderRepository = new OrderRepository(context);
            unitOfWork = new UnitOfWork(context);
            userFavoriteRepository = new UserFavoriteRepository(context);
        }

        protected override void Dispose(bool disposing)
        {
            context.Dispose();
        }

        // GET: User
        [Authorize(Roles = AvailableRoles.Admin + "," + AvailableRoles.DatabaseManager)]
        public ActionResult Index()
        {
            var users = applicationUserRepository.GetApplicationUsers();

            var viewModel = new List<ApplicationUserContainerViewModel>();

            users.ForEach(u => viewModel.Add(ApplicationUserContainerViewModel.CreateNew(u)));

            return View(viewModel);
        }

        [Authorize]
        public ActionResult UserFavorites()
        {
            var favorites = userFavoriteRepository.GetFullUserFavorites(User.Identity.GetUserId());
            var viewModel = new List<UserFavoriteViewModel>();

            if (favorites == null)
                return View(viewModel);

            favorites.ForEach(f => viewModel.Add(UserFavoriteViewModel.Create(f)));

            return View(viewModel);
        }

        [Authorize]
        public ActionResult UserOrders(int page=1)
        {
            var orders = orderRepository.GetOrdersByUserId(User.Identity.GetUserId());
            var viewModel = UserOrderPagingViewModel.CreateViewModelEmpty();

            if (orders != null)
                viewModel = UserOrderPagingViewModel.CreateViewModel(orders, page);

            return View(viewModel);
        }
    }
}