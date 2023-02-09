using ClothStore.Models;
using Microsoft.AspNetCore.Identity;

namespace ClothStore.Data
{
	public class ApplicationDbInitializer
    {
		public static void Initializer(ApplicationDbContext db, UserManager<ApplicationUser> um, RoleManager<IdentityRole> rm)
		{
			db.Database.EnsureDeleted();
			db.Database.EnsureCreated();



            var admin = new IdentityRole("Admin");

            rm.CreateAsync(admin).Wait();

            var adminUser = new ApplicationUser { Email = "yaser@uia.no", UserName = "yaser@uia.no", EmailConfirmed = true };
            um.CreateAsync(adminUser, "Password.1").Wait();

            var user = new ApplicationUser { Email = "yaser@gmail.com", UserName = "yaser@gmail.com", EmailConfirmed = true };
            um.CreateAsync(user, "Password.1").Wait();


            var products = new[]
           {
                new Product("Shirt", Category.Categ.menn,"this shirt is awsome", 40, "/media/shirt.jpg", 0),
                new Product("Shirt", Category.Categ.menn,"this shirt is awsome", 40,"/media/shirt.jpg",0),
                new Product("Shirt", Category.Categ.menn,"this shirt is awsome", 40,"/media/shirt.jpg", 0),
                new Product("Shirt", Category.Categ.menn,"this shirt is awsome", 40,"/media/shirt.jpg", 30),
                new Product("Shirt", Category.Categ.menn,"this shirt is awsome", 40,"/media/shirt.jpg",50),
                new Product("Shirt", Category.Categ.menn,"this shirt is awsome", 40,"/media/shirt.jpg", 100),
            };

            foreach (var p in products)
            {
                p.Rating = 3;
            }
            
            db.Products.AddRange(products);
            db.SaveChanges();

            var products2 = new[]
    {
                new Product("Shirt", Category.Categ.women,"this shirt is awsome", 70, "/media/womenpants.jpg",0),
                new Product("Shirt", Category.Categ.women,"this shirt is awsome", 70,"/media/womenpants.jpg", 0 ),
                new Product("Shirt", Category.Categ.women,"this shirt is awsome", 70,"/media/swomenpants.jpg", 100),
                new Product("Shirt", Category.Categ.women,"this shirt is awsome", 70,"/media/womenpants.jpg", 20),
                new Product("Shirt", Category.Categ.women,"this shirt is awsome", 70,"/media/womenpants.jpg",120),
                new Product("Shirt", Category.Categ.women,"this shirt is awsome", 70,"/media/womenpants.jpg",40),
            };

            db.Products.AddRange(products2);
            db.SaveChanges();

            var products3 = new[]
{
                new Product("Shirt", Category.Categ.babies,"this shirt is awsome", 120, "/media/baby.png",0),
                new Product("Shirt", Category.Categ.babies,"this shirt is awsome", 120,"/media/baby.png",0),
                new Product("Shirt", Category.Categ.babies,"this shirt is awsome", 120,"/media/baby.png",80),
                new Product("Shirt", Category.Categ.babies,"this shirt is awsome", 120,"/media/baby.png",0),
                new Product("Shirt", Category.Categ.babies,"this shirt is awsome", 120,"/media/baby.png",90),
                new Product("Shirt", Category.Categ.babies,"this shirt is awsome", 120,"/media/baby.png",100),
            };

            db.Products.AddRange(products3);
            db.SaveChanges();


            var products4 = new[]
{
                new Product("Shirt", Category.Categ.accessories,"this shirt is awsome", 130, "/media/access.jpg",0),
                new Product("Shirt", Category.Categ.accessories,"this shirt is awsome", 130,"/media/access.jpg",0),
                new Product("Shirt", Category.Categ.accessories,"this shirt is awsome", 130,"/media/access.jpg",80),
                new Product("Shirt", Category.Categ.accessories,"this shirt is awsome", 130,"/media/access.jpg",70),
                new Product("Shirt", Category.Categ.accessories,"this shirt is awsome", 130,"/media/access.jpg",60),
                new Product("Shirt", Category.Categ.accessories,"this shirt is awsome", 130,"/media/access.jpg",100),
            };

            db.Products.AddRange(products4);
            db.SaveChanges();

        }
    }
}
