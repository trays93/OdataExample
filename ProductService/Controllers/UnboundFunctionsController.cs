using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using System.Web.Http;

namespace ProductService.Controllers
{
    public class UnboundFunctionsController : ODataController
    {
        [HttpGet]
        [ODataRoute("GetSalesTaxRate(PostalCode={postalCode})")]
        public IHttpActionResult GetSalesTaxRate([FromODataUri] int postalCode)
        {
            double rate = 5.6; // Use a fake number for the sample.
            return Ok(rate);
        }
    }
}