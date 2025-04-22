using Hirafeyat.SellerServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Hirafeyat.Models;
using Hirafeyat.Services;

namespace Hirafeyat.Controllers
{
    public class SellerController : Controller
    {
        private readonly IOrderService orderService;
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> user;

        public SellerController(IOrderService orderService,UserManager<ApplicationUser> user)
        {
            this.orderService = orderService;
            this.user = user;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Orders() 
        {
            var sellerid = user.GetUserId(User);
            Console.WriteLine($"Seller ID: {sellerid}");
            var orders = orderService.GetAllOrdersBySellerId(sellerid);

            if (orders == null)
            {
                return NotFound();
            }
            return View(orders);
        }
        //STATIC ID
        //var orders = orderService.GetAllOrdersBySellerId("6D793D4A-A25C-46B8-A48F-A5183E7A0683");

        public IActionResult OrderDetail(int id)
        {
            var order = orderService.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order); // هنبعت تفاصيل الطلب للـ View
        }
        public IActionResult UpdateOrderStatus(int orderId, OrderStatus newStatus) 
        {
        
            orderService.UpdateOrderStatus(orderId, newStatus);
            orderService.Save();
         return   RedirectToAction("orders");

        
        }
    }
}
