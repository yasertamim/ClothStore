using ClothStore.Data;
using ClothStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Runtime.ConstrainedExecution;

namespace ClothStore.Controllers
{
    public class AdminController : Controller
    {

        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _env;

        public AdminController(ApplicationDbContext db, UserManager<ApplicationUser> userManager, IWebHostEnvironment env)
        {
            _db = db;
            _userManager = userManager;
            _env = env;
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Index(Category.Categ id)
        {



            if (id == Category.Categ.menn)
            {


                var find = _db.Products.Where(b => b.Categ.Equals(Category.Categ.menn));
                if (find != null)
                {



                    return View(await find.ToListAsync());
                }
            }
            if (id == Category.Categ.women)
            {

                var find = _db.Products.Where(b => b.Categ.Equals(Category.Categ.women));
                if (find != null)
                {
                    return View(await find.ToListAsync());
                }
            }

            if (id == Category.Categ.babies)
            {
                var find = _db.Products.Where(b => b.Categ.Equals(Category.Categ.babies));
                if (find != null)
                {
                    return View(await find.ToListAsync());
                }
            }

            if (id == Category.Categ.accessories)
            {
                var find = _db.Products.Where(b => b.Categ.Equals(Category.Categ.accessories));
                if (find != null)
                {
                    return View(await find.ToArrayAsync());
                }
            }


            return View(new List<Product>());
        }



        [HttpGet]
        [Authorize(Roles = "Admin")]
        //get the view to delete the selected post
        public async Task<IActionResult> DeleteItem(int? id)
        {
            if (id == null || _db.Products == null)
            {
                return NotFound();
            }

            var post = await _db.Products
                .FirstOrDefaultAsync(m => m.Id == id);


            return View(post);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        // delete the selected post
        public async Task<IActionResult> DeleteItem(int id)
        {
            if (id == null || _db.Products == null)
            {
                return NotFound();
            }

            var post = await _db.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }
            _db.Products.Remove(post);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }




        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditItem(int? id)
        {
            if (id == null || _db.Products == null)
            {
                return NotFound();
            }

            var post = await _db.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        // update the selected product
        public async Task<IActionResult> EditItem(Product product)
        {
            Product p = product;
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);



            


                _db.Update(product);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(product);
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        // create new post
        public async Task<IActionResult> Create(Product product,IFormFile file)
        {
            if (ModelState.IsValid)
            {
                
                    if (file != null)
                    {
                        var path = Path.Combine(_env.WebRootPath, "media", file.FileName);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                      //  product.Image = path;
                        product.Image = $"/media/{file.FileName }";
                    }

                


              
               

                _db.Add(product);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(product);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> UserOrders()
        {

            var Orders = await _db.Orders.Include(o => o.Products).ToListAsync();

            if (Orders != null)
            {
                return View(Orders);
            }

            return View(new List<Order>());
        }



        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> ManageOrders(int? id)
        {


            return View(new Order());
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> DeleteOrder(int id)
        {

            if (id == null)
            {
                return NotFound();
            }

            

      
            var order = await _db.Orders.Include(o => o.Products.Where(p => p.OrderId.Equals(id))).FirstOrDefaultAsync();

            foreach (var p in order.Products.ToList())
            {
                p.OrderId = null;
                _db.Update(p);
                await _db.SaveChangesAsync();
            }

            _db.Orders.Remove(order);
            await _db.SaveChangesAsync();

            return RedirectToAction("UserOrders");
        }



    }
}
