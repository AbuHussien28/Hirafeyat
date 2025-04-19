namespace Hirafeyat.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int CustomerId { get; set; }
        public ApplicationUser Customer { get; set; }

        public int Quantity { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public string Status { get; set; } 

        public string Address { get; set; }

    }
}
