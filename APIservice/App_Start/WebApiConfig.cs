using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Http.Cors;

namespace APIservice
{
    public static class WebApiConfig
    {
        //public static void Register(HttpConfiguration config)
        //{
        //    // Web API configuration and services

        //    // Web API routes
        //    config.MapHttpAttributeRoutes();

        //    config.Routes.MapHttpRoute(
        //        name: "DefaultApi",
        //        routeTemplate: "api/{controller}/{id}",
        //        defaults: new { id = RouteParameter.Optional }
        //    );
        //}

     
        public static void Register(HttpConfiguration config)
        {

            config.EnableCors(new EnableCorsAttribute("http://localhost:3000", headers: "*", methods: "*"));

            string isCORS = WebConfigurationManager.AppSettings["CORS_ENABLED"].ToString();
            if (isCORS != "false")
            {
                string origins = WebConfigurationManager.AppSettings["CORS_ORIGINS"].ToString();
                string headers = WebConfigurationManager.AppSettings["CORS_HEADERS"].ToString();
                string methods = WebConfigurationManager.AppSettings["CORS_METHODS"].ToString();
                string credentials = WebConfigurationManager.AppSettings["CORS_CREDENTIALS"].ToString();

                var cors = new EnableCorsAttribute(origins, headers, methods);
                cors.SupportsCredentials = (credentials != "false");
                config.EnableCors(cors);


               // Logger.LogMessage("CORS", "CORS ENABLED", "WEBAPI REGISTERATION");
            }
            else
            {

               // Logger.LogMessage("CORS", "CORS DISABLED", "WEBAPI REGISTERATION");
            }

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));

            // Controller Only
            // To handle routes like `/api/VTRouting`
            // Controller Only
            // To handle routes like `/api/VTRouting`
            config.Routes.MapHttpRoute(
                name: "ControllerOnly",
                routeTemplate: "api/{controller}"
            );

            // Controller with ID
            // To handle routes like `/api/VTRouting/1`
            config.Routes.MapHttpRoute(
                name: "ControllerAndId",
                routeTemplate: "api/{controller}/{id}",
                defaults: null,
                constraints: new { id = @"^\d+$" } // Only integers 
            );

            // Controllers with Actions
            // To handle routes like `/api/VTRouting/route`
            config.Routes.MapHttpRoute(
                name: "ControllerAndAction",
                routeTemplate: "api/{controller}/{action}"
            );


        }
    }
}
