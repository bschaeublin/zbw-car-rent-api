using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using zbw.car.rent.api.Model;
using zbw.car.rent.api.Repositories;
using zbw.car.rent.api.Repositories.Database;
using zbw.car.rent.api.Repositories.InMemory;

namespace zbw.car.rent.api
{
    public class Startup
    {
        public bool ProdMode = false;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllHeaders", builder => {
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
            services.AddMvc();

            if (ProdMode)
            {
                services.AddDbContext<CarRentDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

                #region Database Repositories
                services.AddScoped<IRepository<Customer>, DatabaseRepository<Customer>>();
                services.AddScoped<IRepository<Car>, DatabaseRepository<Car>>();
                services.AddScoped<IRepository<CarBrand>, DatabaseRepository<CarBrand>>();
                services.AddScoped<IRepository<CarType>, DatabaseRepository<CarType>>();
                services.AddScoped<IRepository<CarClass>, DatabaseRepository<CarClass>>();
                services.AddScoped<IRepository<Reservation>, DatabaseRepository<Reservation>>();
                services.AddScoped<IRepository<RentalContract>, DatabaseRepository<RentalContract>>();
                #endregion
            }
            else
            {
                #region In Memory Repositories
                services.AddSingleton<IRepository<Customer>, InMemoryRepository<Customer>>();
                services.AddSingleton<IRepository<Car>, InMemoryRepository<Car>>();
                services.AddSingleton<IRepository<CarBrand>, InMemoryRepository<CarBrand>>();
                services.AddSingleton<IRepository<CarType>, InMemoryRepository<CarType>>();
                services.AddSingleton<IRepository<CarClass>, InMemoryRepository<CarClass>>();
                services.AddSingleton<IRepository<Reservation>, InMemoryRepository<Reservation>>();
                services.AddSingleton<IRepository<RentalContract>, InMemoryRepository<RentalContract>>();
                #endregion
            }

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("AllowAllHeaders");
            app.UseMvc();

        }
    }
}
