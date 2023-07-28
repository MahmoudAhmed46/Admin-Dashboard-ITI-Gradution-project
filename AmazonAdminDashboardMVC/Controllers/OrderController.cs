using AmazonAdmin.Application.Services;
using AmazonAdmin.DTO;
using Microsoft.AspNetCore.Mvc;

namespace AmazonAdminDashboardMVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        public OrderController(IOrderService orderService,IUserService userService)
        {
            _orderService = orderService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            List<OrderDTO> orders = await _orderService.GetAllOrders();
            foreach(var item in orders)
            {
                item.UserName =await _userService.UserName(item.UserId);
            }
            return View(orders);
        }
    }
}
