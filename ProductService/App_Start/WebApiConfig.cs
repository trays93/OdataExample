using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using ProductService.Models;
using System.Web.Http;

namespace ProductService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            ODataModelBuilder builder = new ODataConventionModelBuilder();

            builder.Namespace = "ProductService";

            builder.EntitySet<Product>("Products");
            builder.EntitySet<Supplier>("Suppliers");

            builder.EntityType<Product>()
                .Action("Rate")
                .Parameter<int>("Rating");

            builder.EntityType<Product>()
                .Collection
                .Function("MostExpensive")
                .Returns<double>();

            builder.Function("GetSalesTaxRate")
                .Returns<double>()
                .Parameter<int>("PostalCode");

            config.MapODataServiceRoute(
                routeName: "ODataRoute",
                routePrefix: null,
                model: builder.GetEdmModel());
        }
    }
}
