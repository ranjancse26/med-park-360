﻿// <auto-generated />
using System;
using MedPark.OrderService;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MedPark.OrderService.Migrations
{
    [DbContext(typeof(OrderingDbContext))]
    [Migration("20190731130817_addedPickupLocationIndicatorUpdate")]
    partial class addedPickupLocationIndicatorUpdate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MedPark.OrderService.Domain.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Mobile");

                    b.Property<DateTime>("Modified");

                    b.HasKey("Id");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("MedPark.OrderService.Domain.CustomerAddress", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AddressLine1");

                    b.Property<string>("AddressLine2");

                    b.Property<string>("AddressLine3");

                    b.Property<int>("AddressType");

                    b.Property<DateTime>("Created");

                    b.Property<Guid>("CustomerId");

                    b.Property<bool?>("IsPickUpLocation");

                    b.Property<DateTime>("Modified");

                    b.Property<string>("PostalCode");

                    b.HasKey("Id");

                    b.ToTable("CustomerAddress");
                });

            modelBuilder.Entity("MedPark.OrderService.Domain.LineItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<int>("Markup");

                    b.Property<DateTime>("Modified");

                    b.Property<string>("NappiCode");

                    b.Property<Guid>("OrderId");

                    b.Property<decimal>("Price");

                    b.Property<string>("ProductCode");

                    b.Property<string>("ProductDescription");

                    b.Property<string>("ProductName");

                    b.Property<int>("Quantity");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("LineItem");
                });

            modelBuilder.Entity("MedPark.OrderService.Domain.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comments");

                    b.Property<DateTime>("Created");

                    b.Property<Guid>("CustomerId");

                    b.Property<DateTime?>("DatePaid");

                    b.Property<DateTime>("DatePlaced");

                    b.Property<DateTime>("Modified");

                    b.Property<string>("OrderNo");

                    b.Property<int>("OrderStatus");

                    b.Property<decimal>("OrderTotal");

                    b.Property<Guid?>("ShippingAddress");

                    b.Property<int?>("ShippingType");

                    b.Property<decimal>("TotalVat");

                    b.HasKey("Id");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("MedPark.OrderService.Domain.LineItem", b =>
                {
                    b.HasOne("MedPark.OrderService.Domain.Order")
                        .WithMany("LineItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
