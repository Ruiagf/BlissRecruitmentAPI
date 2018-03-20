namespace BlissRecruitment
{
    using System.Web.Http;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            FluentValidation.WebApi.FluentValidationModelValidatorProvider.Configure(config);

            var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;

            json.SerializerSettings = new Newtonsoft.Json.JsonSerializerSettings
            {
                ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver { NamingStrategy = new Infrastructure.Serialization.LowercaseNamingStrategy() },
            };
        }
    }
}
