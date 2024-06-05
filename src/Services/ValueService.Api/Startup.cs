using Microsoft.OpenApi.Models;
using Service.Core;
namespace ValueService.Api
{
    public class Startup
    {
        private const string SERVICE_NAME = "ValueService.Api";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddConsul(Configuration.GetServiceConfig());
            services.AddHttpContextAccessor();
            services.AddControllers();
            services.AddCors();
            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ValueService.Api", Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "Yiğit ÇEVİK",
                        Email = "yigit.cevik@isimplatform.io",
                    }
                });
            });
            // services.ConfigureConsul(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ValueService.Api v1"));
            }

            app.UseRouting();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseRouting();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("", async context =>
                {
                    await context.Response.WriteAsync(SERVICE_NAME);
                });
            });
        }
    }

}
