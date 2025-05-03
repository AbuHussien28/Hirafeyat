using Hirafeyat.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Hirafeyat.ViewModel.Admin
{
    public class EditProduct
    {
        public int Id { get; set; }

        [Required, MaxLength(100), MinLength(10)]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required, DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Display(Name = "Product Image")]
        public string ImageUrl { get; set; }

        public IFormFile ImageFile { get; set; }
        public bool IsApproved { get; set; }

        public DateTime CreatedAt { get; set; }

        [Display(Name = "Category")]
        public string CategoryName { get; set; }
        [Display(Name = "Seller Name")]
        public string SellerName { get; set; }
        public int CategoryId { get; set; }

        public string SellerId { get; set; }

        [Display(Name = "Status")]
        public productStatus Status { get; set; }

        public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Statuses { get; set; } = new List<SelectListItem>();
    }
}
