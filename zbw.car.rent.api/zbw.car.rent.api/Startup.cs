using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using zbw.car.rent.api.Model;
using zbw.car.rent.api.Provider;
using zbw.car.rent.api.Provider.InMemory;

namespace zbw.car.rent.api
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
            services.AddMvc();
            services.AddSingleton<IDataProvider<Customer>, InMemoryProvider<Customer>>();
            services.AddSingleton<IDataProvider<Car>, InMemoryProvider<Car>>();
            services.AddSingleton<IDataProvider<CarBrand>, InMemoryProvider<CarBrand>>();
            services.AddSingleton<IDataProvider<CarType>, InMemoryProvider<CarType>>();
            services.AddSingleton<IDataProvider<Reservation>, InMemoryProvider<Reservation>>();
            services.AddSingleton<IDataProvider<RentalContract>, InMemoryProvider<RentalContract>>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
