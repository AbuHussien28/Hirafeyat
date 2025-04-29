﻿using Microsoft.AspNetCore.Authorization;
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
namespace Hirafeyat.Controllers.Admin
{
    //[Authorize(Roles = "Admin")]

    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProductController(
            IProductService productService,
            UserManager<ApplicationUser> userManager)
        {
            _productService = productService;
            _userManager = userManager;
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
            if (product == null)
            {
                return NotFound();
            }

            return View("/Views/AdminProducts/Edit.cshtml", product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _productService.UpdateProductAsync(product);
                return RedirectToAction(nameof(Index));
            }

            return View("/Views/AdminProducts/Edit.cshtml", product);
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
