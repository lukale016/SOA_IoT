using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataMicroservice.Hubs;
using DataMicroservice.Models;
using DataMicroservice.Repository;
using DataMicroservice.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace DataMicroservice
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DataMicroservice", Version = "v1" });
            });

            services.AddCors(options =>{
                options.AddPolicy("SOAPolicy", option =>{
                    option.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                });
            });

            services.AddSingleton<DataHub>(new DataHub());
            
            services.AddSignalR();

            Hivemq mqtt = new Hivemq();
            services.AddSingleton(mqtt);
            services.AddScoped<ISensorRepository, SensorRepository>();
            services.AddSingleton(new DataService(mqtt));
            services.AddScoped<DataContext, DataContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DataMicroservice v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseWebSockets();

            app.UseCors("SOAPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<DataHub>("hub/Data");
            });
        }
    }
}
