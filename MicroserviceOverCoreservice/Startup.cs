using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace MicroserviceOverCoreservice
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            HttpConfiguration config = new HttpConfiguration();
            config.EnableCors();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}/{tcmid}",
                defaults: new { id = RouteParameter.Optional, tcmid = RouteParameter.Optional }
                );
            config.Routes.MapHttpRoute(
                name: "default",
                routeTemplate: "{controller}/{action}/{tcmuri}",
                defaults: new { tcmuri = RouteParameter.Optional }
                );
            
            appBuilder.UseWebApi(config);
        }

    }
}
