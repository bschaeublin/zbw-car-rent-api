﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using zbw.car.rent.api.Model;
using zbw.car.rent.api.Repositories.Database;

namespace zbw.car.rent.api.Migrations
{
    [DbContext(typeof(CarRentDbContext))]
    [Migration("20180224193219_Reset")]
    partial class Reset
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("zbw.car.rent.api.Model.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BrandId");

                    b.Property<int?>("CarBrandId");

                    b.Property<int?>("CarClassId");

                    b.Property<int?>("CarTypeId");

                    b.Property<int>("ClassId");

                    b.Property<int>("HorsePower");

                    b.Property<long>("Kilometers");

                    b.Property<int>("RegistrationYear");

                    b.Property<int>("TypeId");

                    b.HasKey("Id");

                    b.HasIndex("CarBrandId");

                    b.HasIndex("CarClassId");

                    b.HasIndex("CarTypeId");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("zbw.car.rent.api.Model.CarBrand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("CarBrands");
                });

            modelBuilder.Entity("zbw.car.rent.api.Model.CarClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Cost");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("CarClasses");
                });

            modelBuilder.Entity("zbw.car.rent.api.Model.CarType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("CarTypes");
                });

            modelBuilder.Entity("zbw.car.rent.api.Model.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("zbw.car.rent.api.Model.RentalContract", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CarId");

                    b.Property<int?>("CarId1");

                    b.Property<int>("CustomerId");

                    b.Property<int?>("CustomerId1");

                    b.Property<int>("Days");

                    b.Property<DateTime>("RentalDate");

                    b.Property<int>("ReservationId");

                    b.Property<decimal>("TotalCosts");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.HasIndex("CarId1");

                    b.HasIndex("CustomerId");

                    b.HasIndex("CustomerId1");

                    b.HasIndex("ReservationId")
                        .IsUnique();

                    b.ToTable("RentalContracts");
                });

            modelBuilder.Entity("zbw.car.rent.api.Model.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CarId");

                    b.Property<int?>("CarId1");

                    b.Property<int>("CustomerId");

                    b.Property<int?>("CustomerId1");

                    b.Property<int>("Days");

                    b.Property<DateTime>("RentalDate");

                    b.Property<DateTime>("ReservationDate");

                    b.Property<int>("State");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.HasIndex("CarId1");

                    b.HasIndex("CustomerId");

                    b.HasIndex("CustomerId1");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("zbw.car.rent.api.Model.Car", b =>
                {
                    b.HasOne("zbw.car.rent.api.Model.CarBrand")
                        .WithMany()
                        .HasForeignKey("CarBrandId");

                    b.HasOne("zbw.car.rent.api.Model.CarClass")
                        .WithMany()
                        .HasForeignKey("CarClassId");

                    b.HasOne("zbw.car.rent.api.Model.CarType")
                        .WithMany()
                        .HasForeignKey("CarTypeId");
                });

            modelBuilder.Entity("zbw.car.rent.api.Model.RentalContract", b =>
                {
                    b.HasOne("zbw.car.rent.api.Model.Car")
                        .WithMany()
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("zbw.car.rent.api.Model.Car")
                        .WithMany()
                        .HasForeignKey("CarId1")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("zbw.car.rent.api.Model.Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("zbw.car.rent.api.Model.Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId1")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("zbw.car.rent.api.Model.Reservation")
                        .WithOne()
                        .HasForeignKey("zbw.car.rent.api.Model.RentalContract", "ReservationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("zbw.car.rent.api.Model.Reservation", b =>
                {
                    b.HasOne("zbw.car.rent.api.Model.Car")
                        .WithMany()
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("zbw.car.rent.api.Model.Car")
                        .WithMany()
                        .HasForeignKey("CarId1")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("zbw.car.rent.api.Model.Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("zbw.car.rent.api.Model.Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId1")
                        .OnDelete(DeleteBehavior.SetNull);
                });
#pragma warning restore 612, 618
        }
    }
}
