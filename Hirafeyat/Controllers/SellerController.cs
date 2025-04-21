using Microsoft.AspNetCore.Mvc;

namespace Hirafeyat.Controllers
{
    public class SellerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
