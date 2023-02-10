namespace ClothStore.Models
{
    public class Order
    {
        public Order() { }

        public Order(DateTime ordertime)
        {

            OrderTime = ordertime;
        }



        public int Id { get; set; }
        public DateTime OrderTime { get; set; }
        public decimal Total
        {
            get
            {
                decimal total = 0;

                foreach (var line in Products)
                {
                    if (line != null)
                    {
                        var amountPrice = line.Amount * line.Price;
                        total += amountPrice;
                    }

                }
                return total;
            }
   

        }



        public bool IsCompleted { get; set; } = false;
        public decimal ShippingTotal {
            get
            {
                decimal shippingTotal = 0;

                foreach (var line in Products)
                {
                    if (line != null)
                    {
                        var amountShipping = line.Amount * line.Shipping;
                        shippingTotal += amountShipping;
                    }

                }
                return shippingTotal;
            }
        }


        public decimal Topay { get
            {
                return Total + ShippingTotal;
                     
            }
                }

        public ApplicationUser? User { get; set; }
        public string? UserId { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();


    }
}

