using CvLocate.Website.Bl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace CvLocate.Website
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            //// Initial log4net configuration
            log4net.Config.XmlConfigurator.Configure();

            var config = GlobalConfiguration.Configuration;
            // Authenticaion handler
            // config.MessageHandlers.Add(new AuthenticationHandler());

            RouteTable.Routes.MapHttpRoute(
                     name: "actionApi",
                     routeTemplate: "api/{controller}/{action}/{id}",
                     defaults: new { id = System.Web.Http.RouteParameter.Optional }
                    
                      );
            RouteTable.Routes.MapHttpRoute(
                            name: "DefaultApi",
                            routeTemplate: "api/{controller}/{id}",
                            defaults: new { id = System.Web.Http.RouteParameter.Optional }
                            );


            //disable XML formatting and return JSON all the time
            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize;
            json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;

            Bootstrapper.Instance.Initialize();

            //var serializerSettings = GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings;
            //var contractResolver = (DefaultContractResolver)json.SerializerSettings.ContractResolver;
            //contractResolver.IgnoreSerializableAttribute = true;

            //// Initial Cache Manager
            //CacheManager.Instance.Initilize();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}