using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Saga.Orchestration.Core.Settings.Concrete;
using System;
using System.IO;
using System.Reflection;

namespace Saga.Orchestration.Core.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class SwaggerExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            var swaggerSettings = new SwaggerSettings();
            configuration.GetSection(nameof(SwaggerSettings)).Bind(swaggerSettings);
            services.Configure<SwaggerSettings>(configuration.GetSection(nameof(SwaggerSettings)));

            if (string.IsNullOrEmpty(swaggerSettings.Title))
                swaggerSettings.Title = AppDomain.CurrentDomain.FriendlyName.Trim().Trim('_');

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(swaggerSettings.Version, swaggerSettings);
                c.CustomSchemaIds(x => x.FullName);

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseSwagger(this IApplicationBuilder app, IConfiguration configuration)
        {
            var swaggerSettings = new SwaggerSettings();
            configuration.GetSection(nameof(SwaggerSettings)).Bind(swaggerSettings);
            if (string.IsNullOrEmpty(swaggerSettings.Title))
                swaggerSettings.Title = AppDomain.CurrentDomain.FriendlyName.Trim().Trim('_');

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{swaggerSettings.VersionName}/swagger.json", swaggerSettings.Title);
                c.RoutePrefix = swaggerSettings.RoutePrefix;
            });

            return app;
        }
    }
}
