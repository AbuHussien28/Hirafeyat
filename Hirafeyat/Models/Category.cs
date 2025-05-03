using System.ComponentModel.DataAnnotations;

namespace Hirafeyat.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        public string Name { get; set; }
        public string? Description { get; set; }
        public ICollection<Product> Products { get; set; }

    }
}
