using System.Web.Http;

namespace DLHK_API
{
	public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "ApiWithAction",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new 
                { 
                    action = RouteParameter.Optional,
                    id = RouteParameter.Optional 
                }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new
                {
                    action = RouteParameter.Optional,
                    id = RouteParameter.Optional
                }
            );

            config.Routes.MapHttpRoute(
                name: "FilterDate",
                routeTemplate: "api/{controller}/filter/{start}/{end}",
                defaults: new
                {
                       note = RouteParameter.Optional,
                       start = RouteParameter.Optional,
                       end = RouteParameter.Optional
                }
            );

            config.Routes.MapHttpRoute(
                name: "ApiComplex",
                routeTemplate: "api/{controller}/{zone}/{region}/{role}/{shift}",
                defaults: new
                {
                    zone = RouteParameter.Optional,
                    region = RouteParameter.Optional,
                    role = RouteParameter.Optional,
                    shift = RouteParameter.Optional,
                }
            );
        }
    }
}
