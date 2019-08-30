namespace ProductService.Migrations
{
    using ProductService.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ProductService.Models.ProductsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ProductService.Models.ProductsContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.


            // Add suppliers.
            Supplier[] suppliers = new Supplier[]
            {
                new Supplier()
                {
                    Name = "Beszállító #1"
                },
                new Supplier()
                {
                    Name = "Beszállító #2"
                }
            };
            context.Suppliers.AddOrUpdate(x => x.ID, suppliers);
            context.SaveChanges();

            // Add products.
            Product[] products = new Product[]
            {
                new Product()
                {
                    Name = "Termék #1",
                    Price = 5.95M,
                    Category = "Kategória #1",
                    Supplier = context.Suppliers.
                        SingleOrDefault(s => s.Name == "Beszállító #1")
                },
                new Product()
                {
                    Name = "Termék #2",
                    Price = 6.73M,
                    Category = "Kategória #2",
                    Supplier = context.Suppliers.
                        SingleOrDefault(s => s.Name == "Beszállító #1")
                },
                new Product()
                {
                    Name = "Termék #3",
                    Price = 4.72M,
                    Category = "Kategória #1",
                    Supplier = context.Suppliers.
                        SingleOrDefault(s => s.Name == "Beszállító #2")
                }
            };
            context.Products.AddOrUpdate(x => x.ID, products);
            context.SaveChanges();
        }
    }
}
