using System.ComponentModel;
using Microsoft.OpenApi.Models;

namespace Checkout
{
    public static class StartupExtensions
    {

        public static IServiceCollection AddServiceClient<T>(this IServiceCollection services, Func<HttpClient, T> factory) where T : class
        {
            services.AddHttpContextAccessor();
            services.AddHttpClient();

            return services.AddTransient(f =>
            {
                var httpClientFactory = f.GetRequiredService<IHttpClientFactory>();
                var httpClient = httpClientFactory.CreateClient();

                // Forward authorization header bearer if any
                var httpContextAccessor = f.GetRequiredService<IHttpContextAccessor>();
                var bearerToken = httpContextAccessor?.HttpContext?.Request?
                    .Headers["Authorization"]
                    .FirstOrDefault(h => h.StartsWith("bearer ", StringComparison.OrdinalIgnoreCase));

                if (bearerToken != null)
                    httpClient.DefaultRequestHeaders.Add("Authorization", bearerToken);

                return factory(httpClient);
            });
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services, string swaggerDocName, string swaggerDocVersion)
        {
            return services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(swaggerDocVersion, new OpenApiInfo { Title = swaggerDocName, Version = swaggerDocVersion });
                c.UseAllOfToExtendReferenceSchemas();

                // Allow us to override the name of the generated JSON object
                // with a DisplayName attribute
                // Usage:
                //    [DisplayName("MyCustomName")]
                //    class MyClass { ... }
                c.CustomSchemaIds(x =>
                {
                    var attribute = x.GetCustomAttributes(typeof(DisplayNameAttribute), true).Cast<DisplayNameAttribute>().SingleOrDefault();
                    return attribute?.DisplayName ?? x.Name;
                });

                // Enable access to file returned by a controller tagged with FileResultContentTypeAttribute
                c.OperationFilter<FileResultContentTypeOperationFilter>();
            })
                .AddSwaggerGenNewtonsoftSupport();
        }

        public static IApplicationBuilder UseSwaggerEndpointAndWebUI(this IApplicationBuilder app, string routePrefix, string swaggerDocName)
        {
            // routePrefix expected format is 'api/workspace'
            // no check on slashes for now

            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger(c =>
            {
                c.RouteTemplate =  "/swagger/{documentName}/swagger.json";
            });

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.) specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Checkout");
            });

            return app;
        }

    }
}
