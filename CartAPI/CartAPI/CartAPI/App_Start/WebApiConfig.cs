using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CartAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            string credential_path = @"C:\Users\kumar\OneDrive\Documents\Carted-0295b328550a.json";
            System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credential_path);
            // Web API configuration and services

            config.EnableCors();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
