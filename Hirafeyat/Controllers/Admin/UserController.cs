using Hirafeyat.AdminServices;
using Microsoft.AspNetCore.Mvc;

namespace Hirafeyat.Controllers.Admin
{
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        public async Task<IActionResult> Sellers(int page = 1, int pageSize = 10)
        {
            ViewData["PaginationAction"] = "Sellers";
            var sellers = await userService.GetSellersAsync(page, pageSize); 

            return View("Sellers", sellers);
        }
        public async Task<IActionResult> Customers(int page = 1, int pageSize = 10)
        {
            ViewData["PaginationAction"] = "Customers";
            var customers = await userService.GetCustomersAsync(page, pageSize); 
            return View("Customers", customers);
        }
    }
}
