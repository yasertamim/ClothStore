using Microsoft.AspNetCore.Mvc;

namespace ClothStore.Controllers
{
    public class AdminController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
