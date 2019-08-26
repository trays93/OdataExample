using Microsoft.AspNet.OData;
using ProductService.Models;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace ProductService.Controllers
{
    public class ProductsController : ODataController
    {
        private ProductsContext db = new ProductsContext();

        private bool ProductExists(int key)
        {
            return db.Products.Any(p => p.ID == key);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}