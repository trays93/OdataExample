using Microsoft.AspNet.OData;
using ProductService.Models;
using System.Linq;

namespace ProductService.Controllers
{
    public class SuppliersController : ODataController
    {
        ProductsContext db = new ProductsContext();

        private bool SupplierExists(int key)
        {
            return db.Suppliers.Any(s => s.ID == key);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}