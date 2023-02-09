﻿using ClothStore.Data;
using ClothStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ClothStore.Controllers
{
    public class CustomerController : Controller
    {

        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
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


            return View(new List<Product>());
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
   
        public async Task<IActionResult> Cart(int id)
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

                _db.Products.Update(product);
                await _db.SaveChangesAsync();



                // return RedirectToAction("Index");
                //  var cart1 = await _db.Carts.FirstOrDefaultAsync(p => p.UserId.Equals(user.Id));
                return RedirectToAction("Index");
            
            }
            if (checkCart.Count == 1) { 
            
                var cart = await _db.Carts.FirstOrDefaultAsync(p => p.UserId.Equals(user.Id));

                product.CartId = cart.Id;

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


            return View(new Order());
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

            foreach (var p in cart.Products)
            {
                p.OrderId = orderDb.Id;
                _db.Products.Update(p);
                await _db.SaveChangesAsync();
            }




            return View(orderDb);
        }





    }
}
