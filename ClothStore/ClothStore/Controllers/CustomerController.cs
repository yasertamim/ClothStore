using ClothStore.Data;
using ClothStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Stripe;

namespace ClothStore.Controllers
{
    public class CustomerController : Controller
    {

        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<IdentityRole> _signInManager;
        public CustomerController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        
        }



        [HttpGet]
        // Get/Index/1
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


            return View(new List<Models.Product>());
        }



        [Authorize]
        [HttpGet]


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }



            var product = await _db.Products.FirstOrDefaultAsync(p => p.Id.Equals(id));
            return View(product);
        }


        [Authorize]
        [HttpPost]


        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var user = await _userManager.GetUserAsync(User);
            var product = await _db.Products.FirstOrDefaultAsync(p => p.Id.Equals(id));

            var checkCart = await _db.Carts.Where(p => p.UserId.Equals(user.Id)).ToListAsync();

            if (checkCart == null || checkCart.Count == 0)
            {
                var cart = new Cart();
                cart.UserId = user.Id;

                cart.Products.Add(product);

                await _db.Carts.AddAsync(cart);
                _db.SaveChangesAsync().Wait();
            }
            if (checkCart.Count == 1)
            {
                var cart = await _db.Carts.FirstOrDefaultAsync(p => p.UserId.Equals(user.Id));
                cart.Products.Add(product);
                _db.Update(cart.Products);
                _db.Update(cart);
                await _db.SaveChangesAsync();

            }

            return View(product.Id);
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ShoppingCart()
        {
            var user = await _userManager.GetUserAsync(User);
            var cartInit = await _db.Carts.FirstOrDefaultAsync(p => p.UserId.Equals(user.Id));

            var cart = await _db.Carts.Include(c => c.Products.Where(p => p.Cart.UserId.Equals(user.Id))).ToListAsync();
        
    

            if (cart != null)
            {
                return View(cart);
            }


            //if (cartInit != null)
            //{
            //    return View(cartInit);
            //}

            return View(new List<Cart>());

        }

        //[HttpGet]
        //public async Task<IActionResult> Cart()
        //{
        //    var user = await _userManager.GetUserAsync(User);

        //    var cartInit = await _db.Carts.FirstOrDefaultAsync(p => p.UserId.Equals(user.Id) );
        //    return View(cartInit);
        //}




        [Authorize]
        [HttpPost]
   
        public async Task<IActionResult> Cart(int id, int amount)
        {
            var user = await _userManager.GetUserAsync(User);


            if (id == 0)
            {
                var cartInit = await _db.Carts.FirstOrDefaultAsync(p => p.UserId.Equals(user.Id) && p.Id.Equals(id));
                return RedirectToAction(nameof(Index));
            }

        

          
            var product = await _db.Products.FirstOrDefaultAsync(p => p.Id.Equals(id));

            var checkCart = await _db.Carts.Where(p => p.UserId.Equals(user.Id)).ToListAsync();

            if (checkCart == null || checkCart.Count == 0)
            {
                var cart = new Cart();
                cart.UserId = user.Id;
        
                await _db.AddAsync(cart);
                await _db.SaveChangesAsync();


                var cartInit = await _db.Carts.FirstOrDefaultAsync(p => p.UserId.Equals(user.Id));
         
                product.CartId = cartInit.Id;
                product.Amount = amount;

                _db.Products.Update(product);
                await _db.SaveChangesAsync();
                ViewData["count"] = cartInit.Products.Count;


                // return RedirectToAction("Index");
                //  var cart1 = await _db.Carts.FirstOrDefaultAsync(p => p.UserId.Equals(user.Id));
                return RedirectToAction("Index");
            
            }
            if (checkCart.Count == 1) { 
            
                var cart = await _db.Carts.FirstOrDefaultAsync(p => p.UserId.Equals(user.Id));

                product.CartId = cart.Id;
                product.Amount = amount;

                _db.Products.Update(product);
                await _db.SaveChangesAsync();
                //cart.Products.Add(product); 
                //_db.Update(cart);

                //_db.Update(cart);
                //await _db.SaveChangesAsync();
                // return RedirectToAction("Index");
                //   var cart2 = await _db.Carts.FirstOrDefaultAsync(p => p.UserId.Equals(user.Id));
                return RedirectToAction("Index");

            }



            return RedirectToAction("Index");
        }



        [Authorize]
        [HttpGet]
        public async Task<IActionResult> PlaceOrder(Order order)

        {
            var user = await _userManager.GetUserAsync(User);
            var order1 = await _db.Orders.FirstOrDefaultAsync(p => p.UserId.Equals(user.Id) && p.Id.Equals(order.Id));

            //var cart = await _db.Orders.Include(c => c.Products.Where(p => p.CartId.Equals(1))).ToListAsync();



            //if (cart != null)
            //{
            //    return View(cart);
            //}

            if (order != null)
            {
                return View(order1);
            }


            return View(order);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PlaceOrder(int id)

        {
            var user = await _userManager.GetUserAsync(User);
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _db.Carts.Include(c => c.Products.Where(p => p.CartId.Equals(id))).FirstOrDefaultAsync();
            var order = new Order(DateTime.Now);

            order.User = user;

            await _db.AddAsync(order);
            await _db.SaveChangesAsync();


            var orderDb = await _db.Orders.FirstOrDefaultAsync(p => p.UserId.Equals(user.Id));
            var shoppingCart = await _db.Carts.FirstOrDefaultAsync(c => c.UserId.Equals(user.Id));

            foreach (var p in cart.Products)
            {
                p.OrderId = orderDb.Id;
                _db.Products.Update(p);
                await _db.SaveChangesAsync();
            }


            _db.Carts.Remove(shoppingCart);
            await _db.SaveChangesAsync();


            return View(orderDb);
        }



        [HttpGet]
        [Authorize]
        //get the view to delete the selected post
        public async Task<IActionResult> DeleteItem(int? id)
        {
            if (id == 0 )
            {
                return NotFound();
            }

            var product = await _db.Products.FirstOrDefaultAsync(p => p.Id.Equals(id));

            if (product != null)
            {
                return View(product);
            }
     

            return View(new Models.Product());
        }



        [HttpPost]
        [Authorize]
        //get the view to delete the selected post
        public async Task<IActionResult> DeleteItem(int id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            var product = await _db.Products.FirstOrDefaultAsync(p => p.Id.Equals(id));

            if (product != null)
            {

                product.CartId = null;
                _db.Products.Update(product);
                await _db.SaveChangesAsync();
                return RedirectToAction("ShoppingCart");
            }


            return View();
        }




        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Orders()
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;

            //   var Orders = await _db.Orders.Where(p => p.UserId == userId).ToListAsync();
            var Orders = await _db.Orders.Include(o => o.Products.Where(P => P.Order.UserId == userId)).ToListAsync();

            if (Orders != null)
            {
                return View(Orders);
            }

            return View(new List<Order>());
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> DeleteOrder(int id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;

         //   var order = await _db.Orders.FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);
            var order = await _db.Orders.Include(o => o.Products.Where(P => P.Order.UserId == userId && P.OrderId == id)).FirstOrDefaultAsync();

            foreach (var p in order.Products.ToList())
            {
               p.OrderId = null;
                _db.Update(p);
                await _db.SaveChangesAsync();
            }

            _db.Orders.Remove(order);
            await _db.SaveChangesAsync();

            return RedirectToAction("Orders");
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Payment()
        {

            return View();

        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Payment(int id, string stripeToken)
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;

            var order = await _db.Orders.Include(o => o.Products.Where(P => P.Order.UserId == userId && P.OrderId == id)).FirstOrDefaultAsync();
            if (order != null)
            {
                order.IsCompleted = true;
                _db.Update(order);
                await _db.SaveChangesAsync();
                return RedirectToAction("Orders");
            }

            //if (order != null)
            //{
            //    var chargeOptions = new ChargeCreateOptions()
            //    {
            //        Amount = (long)(Convert.ToDouble(order.Topay) * 100),
            //        Currency = "NOK",
            //        Source = stripeToken,
            //        Metadata = new Dictionary<string, string>()
            //    {
            //        {"OrderId", order.Id.ToString() },

            //        {"Customer", order.User.Email }
            //    }
            //    };

            //    var service = new ChargeService();
            //    Charge charge = service.Create(chargeOptions);

            //    if (charge.Status == "succeeded")
            //    {
            //        order.IsCompleted = true;
            //        _db.Orders.Update(order);
            //        await _db.SaveChangesAsync();
            //        return RedirectToAction("MyOrders");
            //    }

            //}

            return RedirectToAction("Orders");

        }



        [Authorize]
        [HttpGet]
        public async Task<IActionResult> CompleteOrder(Order order)

        {
            var user = await _userManager.GetUserAsync(User);
          //  var order1 = await _db.Orders.FirstOrDefaultAsync(p => p.UserId.Equals(user.Id) && p.Id.Equals(order.Id));

            var order1 = await _db.Orders.Include(c => c.Products.Where(p => p.OrderId.Equals(order.Id))).FirstOrDefaultAsync();



            //if (cart != null)
            //{
            //    return View(cart);
            //}

            if (order != null)
            {
                return View(order1);
            }


            return View(new Order());
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CompleteOrder(int id)

        {
            var user = await _userManager.GetUserAsync(User);
            if (id == null)
            {
                return NotFound();
            }

           var order =  await _db.Orders.Include(o => o.Products.Where(P => P.Order.UserId == user.Id && P.OrderId == id)).FirstOrDefaultAsync();

            //await _db.AddAsync(order);
            //await _db.SaveChangesAsync();


            //var orderDb = await _db.Orders.FirstOrDefaultAsync(p => p.UserId.Equals(user.Id));
            //var shoppingCart = await _db.Carts.FirstOrDefaultAsync(c => c.UserId.Equals(user.Id));

            if(order == null)
            {
                return NotFound();
            }


            return View(order);
        }

    }
}
