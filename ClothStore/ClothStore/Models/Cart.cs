namespace ClothStore.Models
{
    public class Cart
    {
        public Cart() { 

            Products = new List<Product>();
        }

    
   
        public int Id { get; set; }
        public List<Product> Products { get; set; } 

        public ApplicationUser? User { get; set; }

        public string? UserId { get; set; }
    }
}
