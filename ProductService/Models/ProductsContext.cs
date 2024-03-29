﻿using System.Data.Entity;

namespace ProductService.Models
{
    public class ProductsContext : DbContext
    {
        public ProductsContext() : base("name=ProductsContext") { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<ProductRating> Ratings { get; set; }
    }
}