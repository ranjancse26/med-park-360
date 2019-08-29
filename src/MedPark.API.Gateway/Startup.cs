﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using MedPark.API.Gateway.Controllers;
using MedPark.API.Gateway.Services;
using MedPark.Common.RabbitMq;
using MedPark.Common.RestEase;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MedPark.API.Gateway
{
    public class Startup
    {
        private static readonly string[] Headers = new[] { "X-Operation", "X-Resource", "X-Total-Count" };
        public IConfiguration Configuration { get; }
        public IContainer Container { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;

                //Add Conventions
                options.Conventions.Controller<CustomersController>().HasApiVersion(new ApiVersion(1, 0));
                options.Conventions.Controller<AuthenticationController>().HasApiVersion(new ApiVersion(1, 0));
                options.Conventions.Controller<MedicalPracticeController>().HasApiVersion(new ApiVersion(1, 0));
                options.Conventions.Controller<BookingsController>().HasApiVersion(new ApiVersion(1, 0));
                options.Conventions.Controller<BasketController>().HasApiVersion(new ApiVersion(1, 0));
                options.Conventions.Controller<CatalogController>().HasApiVersion(new ApiVersion(1, 0));
                options.Conventions.Controller<OrderController>().HasApiVersion(new ApiVersion(1, 0));
                options.Conventions.Controller<PaymentController>().HasApiVersion(new ApiVersion(1, 0));
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", cors =>
                        cors.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials()
                            .WithExposedHeaders(Headers));
            });

            services.AddDefaultEndpoint<ICustomerService>("customer-service");
            services.AddDefaultEndpoint<IMedicalPracticeService>("med-practice-service");
            services.AddDefaultEndpoint<IBookingService>("booking-service");
            services.AddDefaultEndpoint<ICatalogService>("catalog-service");
            services.AddDefaultEndpoint<IBasketService>("basket-service");
            services.AddDefaultEndpoint<IOrderService>("order-service");
            services.AddDefaultEndpoint<IPaymentService>("payment-service");

            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                .AsImplementedInterfaces();
            builder.AddRabbitMq();

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

            app.UseCors("CorsPolicy");
            app.UseRabbitMq();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
