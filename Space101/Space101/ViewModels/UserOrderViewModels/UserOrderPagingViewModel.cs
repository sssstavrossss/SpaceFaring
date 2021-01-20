using Space101.Models;
using Space101.ViewModels.OrderViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Space101.ViewModels.UserOrderViewModels
{
    public class UserOrderPagingViewModel
    {
        public IEnumerable<DisplayOrderViewModel> Orders { get; set; }
        public int OrderPerPage { get; set; } = 8;
        public int CurrentPage { get; set; }

        public UserOrderPagingViewModel() 
        {
            Orders = new List<DisplayOrderViewModel>();
        }

        public UserOrderPagingViewModel(List<DisplayOrderViewModel> ordersViewMoled, int page)
        {
            Orders = ordersViewMoled;
            CurrentPage = page;
        }

        public static UserOrderPagingViewModel CreateViewModel(List<Order> orders, int page)
        {
            var ordersViewModel = new List<DisplayOrderViewModel>();
            orders.ForEach(o => ordersViewModel.Add(new DisplayOrderViewModel(o)));
            return new UserOrderPagingViewModel(ordersViewModel, page);
        }

        public static UserOrderPagingViewModel CreateViewModelEmpty()
        {
            return new UserOrderPagingViewModel();
        }

        public void SetPage(int page)
        {
            CurrentPage = page;
        }

        public int PageCount()
        {
            return Convert.ToInt32(Math.Ceiling(Orders.Count() / (double)OrderPerPage));
        }

        public IEnumerable<DisplayOrderViewModel> PaginatedOrders()
        {
            int start = (CurrentPage - 1) * OrderPerPage;
            return Orders.OrderBy(o => o.OrderId).Skip(start).Take(OrderPerPage);
        }
    }
}