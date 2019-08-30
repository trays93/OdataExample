﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductService;

namespace ProductsApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** ProductsApp kliens *****");

            Uri uri = new Uri("https://localhost:44387/");

            ListSuppliers(uri);
            ListProducts(uri);
            
            Console.WriteLine("Nyomj entert a folytatáshoz");
            Console.ReadLine();
        }

        private static void ListSuppliers(Uri uri)
        {
            var container = new Container(uri);
            var suppliers = container.Suppliers.Expand("Products").Execute();
            Console.WriteLine("\nBeszállítók:");
            foreach (var supplier in suppliers)
            {
                Console.WriteLine($"\t{supplier.Name}");
                if (supplier.Products != null)
                {
                    foreach (var product in supplier.Products)
                    {
                        Console.WriteLine($"\t\t{product.Name}");
                    }
                }
            }
            Console.WriteLine();
        }

        private static void ListProducts(Uri uri)
        {
            var container = new Container(uri);
            var products = container.Products.Expand("Supplier").Execute();
            Console.WriteLine("\nTermékek:");
            foreach (var product in products)
            {
                if (product.Supplier != null)
                {
                    Console.WriteLine($"\t{product.Name}, {product.Price}, {product.Supplier.Name}");
                }
                else
                {
                    Console.WriteLine($"\t{product.Name}, {product.Price}");
                }
            }
            Console.WriteLine();
        }
    }
}