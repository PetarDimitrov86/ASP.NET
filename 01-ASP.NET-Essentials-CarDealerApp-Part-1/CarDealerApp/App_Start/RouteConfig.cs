namespace CarDealerApp
{
    using System.Web.Mvc;
    using System.Web.Routing;
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                  name: "Customers All",
                  url: "Customers/All/{sortType}",
                  defaults: new
                  {
                      controller = "Customers",
                      action = "Index",
                      sortType = "all"
                  }
              );

            routes.MapRoute(
                name: "Suppliers",
                url: "Suppliers/{type}",
                defaults: new
                {
                    controller = "Suppliers",
                    action = "Index",
                }
            );

            routes.MapRoute(
                 name: "Cars",
                 url: "Cars/{make}",
                 defaults: new
                 {
                     controller = "Cars",
                     action = "Index",
                     make = ""
                 }
             );

            routes.MapRoute(
                 name: "Car Parts",
                 url: "Cars/{id}/Parts",
                 defaults: new
                 {
                     controller = "Cars",
                     action = "Parts",
                     id = 1
                 }
             );

            routes.MapRoute(
                 name: "Customer Sales Details",
                 url: "Customers/{id}",
                 defaults: new
                 {
                     controller = "Customers",
                     action = "Sales",
                     id = 1
                 }
             );

            routes.MapRoute(
                 name: "Sales Discounts All",
                 url: "Sales",
                 defaults: new
                 {
                     controller = "Sales",
                     action = "Index",
                 }
             );

            routes.MapRoute(
                name: "Discounted Sales All",
                url: "Sales/Discounted/{percent}",
                defaults: new
                {
                    controller = "Sales",
                    action = "Discounted",
                    percent = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                 name: "Sales Details",
                 url: "Sales/{id}",
                 defaults: new
                 {
                     controller = "Sales",
                     action = "Details",
                     id = UrlParameter.Optional
                 },
                 constraints: new { id=@"\d+" }
             );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );          
        }
    }
}