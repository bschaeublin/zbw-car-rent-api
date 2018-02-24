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
using zbw.car.rent.api.Repositories;
using zbw.car.rent.api.Repositories.InMemory;

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

            #region In Memory Repositories
            services.AddSingleton<IRepository<Customer>, InMemoryRepository<Customer>>();
            services.AddSingleton<IRepository<Car>, InMemoryRepository<Car>>();
            services.AddSingleton<IRepository<CarBrand>, InMemoryRepository<CarBrand>>();
            services.AddSingleton<IRepository<CarType>, InMemoryRepository<CarType>>();
            services.AddSingleton<IRepository<Reservation>, InMemoryRepository<Reservation>>();
            services.AddSingleton<IRepository<RentalContract>, InMemoryRepository<RentalContract>>();
            #endregion
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
