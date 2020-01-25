using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjectionConfigurationInClassAndIOptions.Classes;             
namespace ProjectionConfigurationInClassAndIOptions
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup(IConfiguration config)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("configPerson.json")
                .Build();
            AppConfiguration = builder;
        }
        public IConfiguration AppConfiguration { get; set; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<Person>(AppConfiguration);
            services.Configure<Person>(option =>
            {
                option.Name = "Jerry";
            });
            services.Configure<Company>(AppConfiguration);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMiddleware<PersonMiddleware>();
        }
    }
}
