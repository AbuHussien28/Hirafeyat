using Microsoft.AspNetCore.Authorization;
using X.PagedList;
using Microsoft.AspNetCore.Mvc;
using Hirafeyat.AdminServices;
using Hirafeyat.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList.EntityFramework;
using System.Threading.Tasks;
using System.Data.Entity;
using Hirafeyat.ViewModel.Admin;
using AutoMapper;
namespace Hirafeyat.Controllers.Admin
{
    //[Authorize(Roles = "Admin")]

    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;

        public ProductController(
            IProductService productService,
            UserManager<ApplicationUser> userManager,
            IMapper mapper,
            ICategoryService categoryService)
        {
            _productService = productService;
            _userManager = userManager;
            _mapper = mapper;
            _categoryService = categoryService;
        }

        [Route("/Admin/Product/Index")]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10, string? sellerId = null)
        {
            var products = await _productService.GetProductsAsync(pageNumber, pageSize, sellerId);
            var totalProducts = await _productService.GetTotalProductsCountAsync(sellerId);

            if (products == null || !products.Any())
            {
                ViewBag.Message = "No products available.";
            }

            ViewBag.TotalProducts = totalProducts;
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.SellerId = sellerId;

            return View("/Views/AdminProducts/Index.cshtml", products);
        }

        [Route("/Admin/Product/Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View("/Views/AdminProducts/Details.cshtml", product);
        }
        [Route("/Admin/Product/Edit")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null) return NotFound();

            var vm = _mapper.Map<EditProduct>(product);

            // Fill SelectLists
            var categories = _categoryService.GetAll();
            vm.Categories = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();

            vm.Statuses = Enum.GetValues(typeof(productStatus))
                              .Cast<productStatus>()
                              .Select(s => new SelectListItem
                              {
                                  Value = ((int)s).ToString(),
                                  Text = s.ToString()
                              }).ToList();

            return View("/Views/AdminProducts/Edit.cshtml", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveEdit(int Id,EditProduct vm)
        {
            if (!ModelState.IsValid)
            {
                // Refill dropdowns if validation fails
                var categories = _categoryService.GetAll();
                vm.Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();

                vm.Statuses = Enum.GetValues(typeof(productStatus))
                                  .Cast<productStatus>()
                                  .Select(s => new SelectListItem
                                  {
                                      Value = ((int)s).ToString(),
                                      Text = s.ToString()
                                  }).ToList();
                return View("/Views/AdminProducts/Edit.cshtml", vm);

            }
            await _productService.UpdateProductAsync(vm);
            return RedirectToAction("Index");
        }

       
        
        [Route("/Admin/Product/DeleteAsync/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View("/Views/AdminProducts/Delete.cshtml", product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            await _productService.DeleteProductAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
