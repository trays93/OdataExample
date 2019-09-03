using System;
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

            var product = AddProduct(uri);

            ListProducts(uri);

            product = UpdateProduct(uri, product);

            ListProducts(uri);

            DeleteProduct(uri, product);

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

        public static Product AddProduct(Uri uri)
        {
            var container = new Container(uri);

            var product = new Product()
            {
                Name = "Próba termék",
                Price = 7.23M,
                Category = "Próba kategória",
                SupplierID = 1
            };

            container.AddToProducts(product);
            container.SaveChanges();
            Console.WriteLine($"Termék hozzáadva: {product.Name}");

            return product;
        }

        private static Product UpdateProduct(Uri uri, Product product)
        {
            var container = new Container(uri);
            var updateProduct = container.Products.ByKey(product.ID).GetValue();
            updateProduct.Name = "Lorem ipsum";
            updateProduct.Category = "Kategória #2";

            container.UpdateObject(updateProduct);
            container.SaveChanges();
            Console.WriteLine($"Termék frissítve: {updateProduct.Name}");

            return updateProduct;
        }

        private static void DeleteProduct(Uri uri, Product product)
        {
            var container = new Container(uri);
            var deleteProduct = container.Products.ByKey(product.ID).GetValue();

            container.DeleteObject(deleteProduct);
            container.SaveChanges();

            Console.WriteLine($"Termék törölve: {deleteProduct.Name}");
        }
    }
}
