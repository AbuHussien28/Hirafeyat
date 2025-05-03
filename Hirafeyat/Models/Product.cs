﻿using System.ComponentModel.DataAnnotations;

namespace Hirafeyat.Models
{
    public enum productStatus
    {
        Approved = 1, Pending = 2 , Rejected = 3
    }
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }
        [Display(Name = "Product Image")]

        public string ImageUrl { get; set; }
        [Required]
        public int Quentity { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsApproved { get; set; }  
        // Foreign Keys
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        //was int now string
        public string SellerId { get; set; }
        public ApplicationUser Seller { get; set; }

        // Navigation
        public ICollection<Order> Orders { get; set; }

    }
}
