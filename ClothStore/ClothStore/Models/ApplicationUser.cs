using Microsoft.AspNetCore.Identity;

namespace ClothStore.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<Order>? Orders { get; set; }

        public Cart? Cart { get; set; }
    }
}
