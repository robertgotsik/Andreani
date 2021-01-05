using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AndreaniCodificador.RabbitMQ.Bus;
using AndreaniCodificador.RabbitMQ.Interfaces;
using AndreaniCodificador.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AndreaniCodificador
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddTransient<IRabbitEventBus, RabbitEventBus>();

            services.AddHttpClient("OpenStreetMap", config =>
            {
                config.BaseAddress = new Uri(Configuration.GetSection("Services:OpenStreetMapAPI").Value);
            });

            services.AddTransient<OpenSteetMapService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var eventBus = app.ApplicationServices.GetRequiredService<IRabbitEventBus>();
            eventBus.Consume();
        }
    }
}
