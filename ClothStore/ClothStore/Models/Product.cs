namespace ClothStore.Models
{
    public class Product
    {
        public Product() { }

        public Product(string title, Category.Categ categ, string details, decimal price, string image,decimal shipping)
        {

            Title = title;
            Categ = categ;
            Details = details;
            Price = price;
            Image = image;
            Shipping = shipping;
        }

        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public Category.Categ Categ { get; set; } = Category.Categ.babies;
        public string Details { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public string Image { get; set; } = string.Empty;

        public int Amount { get; set; } = 1;

        public decimal Shipping { get; set; }

        public int Rating { get; set; } = 0;


        public Cart Cart { get; set; }

        public int? CartId { get; set; }

        public int? OrderId { get; set; }

        public Order? Order { get; set; }



       
    }
}
