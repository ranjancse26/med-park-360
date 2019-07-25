﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using MedPark.Catalog.Domain;
using MedPark.Catalog.Dto;
using MedPark.Catalog.Handlers.Catalog;
using MedPark.Catalog.Queries;
using MedPark.Common;
using MedPark.Common.Handlers;
using MedPark.Common.RabbitMq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MedPark.Catalog
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IContainer Container { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper();

            //Add DBContext
            services.AddDbContext<CatalogDBContext>(options => options.UseSqlServer(Configuration["Database:ConnectionString"]));

            services.AddScoped(typeof(IQueryHandler<ProductQueries, ProductDetailDto>), typeof(ProductQueriesHandler));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //SeedData.EnsureSeedData(services.BuildServiceProvider());

            var builder = new ContainerBuilder();

            builder.RegisterType<CatalogDBContext>().As<DbContext>().InstancePerLifetimeScope();

            builder.Populate(services);
            builder.AddDispatchers();
            //builder.AddRabbitMq();
            builder.AddRepository<Product>();
            builder.AddRepository<Category>();
            builder.AddRepository<ProductCatalog>();

            Container = builder.Build();
            return new AutofacServiceProvider(Container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            //app.UseRabbitMq();

            app.UseMvcWithDefaultRoute();
        }
    }
}
