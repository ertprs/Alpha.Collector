using System.Net.Http.Formatting;
using System.Web.Http;

namespace Alpha.Collector.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //默认返回 json  
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.MediaTypeMappings.Add(
                new QueryStringMapping("datatype", "json", "application/json"));

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
