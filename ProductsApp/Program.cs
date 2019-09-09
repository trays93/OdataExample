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

            ConnectionHelper.Uri = new Uri("https://localhost:44387/");

            ListSuppliers();
            ListProducts();

            var product = AddProduct();

            ListProducts();

            product = UpdateProduct(product);

            ListProducts();

            DeleteProduct(product);

            ListProducts();

            Console.WriteLine("Nyomj entert a folytatáshoz");
            Console.ReadLine();
        }

        private static void ListSuppliers()
        {
            var container = ConnectionHelper.Container;
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

        private static void ListProducts()
        {
            var container = ConnectionHelper.Container;
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

        public static Product AddProduct()
        {
            var container = ConnectionHelper.Container;

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

        private static Product UpdateProduct(Product product)
        {
            var container = ConnectionHelper.Container;
            var updateProduct = container.Products.ByKey(product.ID).GetValue();
            updateProduct.Name = "Lorem ipsum";
            updateProduct.Category = "Kategória #2";

            container.UpdateObject(updateProduct);
            container.SaveChanges();
            Console.WriteLine($"Termék frissítve: {updateProduct.Name}");

            return updateProduct;
        }

        private static void DeleteProduct(Product product)
        {
            var container = ConnectionHelper.Container;
            var deleteProduct = container.Products.ByKey(product.ID).GetValue();

            container.DeleteObject(deleteProduct);
            container.SaveChanges();

            Console.WriteLine($"Termék törölve: {deleteProduct.Name}");
        }
    }

    public static class ConnectionHelper
    {
        public static Uri Uri { get; set; }

        public static Container Container
        {
            get
            {
                var container = new Container(Uri);
                container.SendingRequest2 += Container_SendingRequest2;
                return container;
            }
        }

        private static void Container_SendingRequest2(object sender, Microsoft.OData.Client.SendingRequest2EventArgs e)
        {
            string userName = "admin", password = "admin";
            string token = Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", userName, password)));
            e.RequestMessage.SetHeader("Authorization", $"Basic {token}");
        }
    }
}
