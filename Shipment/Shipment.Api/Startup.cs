using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Saga.Orchestration.Core.Extensions;
using Shipment.Infrastructure.Extensions;
using Shipment.Services.Extensions;

namespace Shipment.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddApiVersioning(options =>
            //{
            //    options.DefaultApiVersion = new ApiVersion(1, 0);
            //    options.AssumeDefaultVersionWhenUnspecified = true;
            //    options.ReportApiVersions = true;
            //});

            //services.AddVersionedApiExplorer(options =>
            //{
            //    // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service  
            //    // note: the specified format code will format the version as "'v'major[.minor][-status]"  
            //    options.GroupNameFormat = "'v'VVV";

            //    // note: this option is only necessary when versioning by url segment. the SubstitutionFormat  
            //    // can also be used to control the format of the API version in route templates  
            //    options.SubstituteApiVersionInUrl = true;
            //});

            services.AddCore(Configuration)
                                .AddSwagger(Configuration)
                                .AddJaeger(Configuration)
                                .AddShipmentInfra(Configuration)
                                .AddServices();

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger(Configuration);

            app.UseSwaggerUI(options =>
            {
                //foreach (var description in provider.ApiVersionDescriptions)
                //{
                //    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                //}
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
