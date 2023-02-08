using Microsoft.AspNetCore.Mvc;

namespace ClothStore.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
