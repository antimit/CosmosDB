﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TestApp2._0.Data;

#nullable disable

namespace TestApp2._0.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TestApp2._0.Models.Address", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AddressId"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<double?>("Latitude")
                        .HasColumnType("float");

                    b.Property<double?>("Longitude")
                        .HasColumnType("float");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("AddressId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("TestApp2._0.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerId"));

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerId");

                    b.HasIndex(new[] { "Email" }, "IX_Email_Unique")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("TestApp2._0.Models.Delivery", b =>
                {
                    b.Property<int>("DeliveryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DeliveryId"));

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int?>("StopId")
                        .HasColumnType("int");

                    b.HasKey("DeliveryId");

                    b.HasIndex("StopId");

                    b.ToTable("Deliveries");
                });

            modelBuilder.Entity("TestApp2._0.Models.DeliveryItem", b =>
                {
                    b.Property<int>("DeliveryItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DeliveryItemId"));

                    b.Property<int?>("CurrentDeliveryId")
                        .HasColumnType("int");

                    b.Property<int?>("DeliveredCount")
                        .HasColumnType("int");

                    b.Property<int>("ItemVolume")
                        .HasColumnType("int");

                    b.Property<int>("ItemWeight")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrderedCount")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<decimal>("SalesUnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalCost")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("DeliveryItemId");

                    b.HasIndex("CurrentDeliveryId");

                    b.HasIndex("ItemWeight")
                        .HasDatabaseName("IX_DeliveryItem_ItemWeight");

                    b.HasIndex("ProductId");

                    b.HasIndex("SalesUnitPrice")
                        .HasDatabaseName("IX_DeliveryItem_SalesUnitPrice");

                    b.ToTable("DeliveryItems");
                });

            modelBuilder.Entity("TestApp2._0.Models.Driver", b =>
                {
                    b.Property<int>("DriverId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DriverId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("DriverId");

                    b.ToTable("Drivers");
                });

            modelBuilder.Entity("TestApp2._0.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("SalesUnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Volume")
                        .HasColumnType("int");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.HasKey("ProductId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("TestApp2._0.Models.Stop", b =>
                {
                    b.Property<int>("StopId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StopId"));

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<double>("DistanceFromPreviousStop")
                        .HasColumnType("float");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("StopOrder")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TransportationId")
                        .HasColumnType("int");

                    b.HasKey("StopId");

                    b.HasIndex("AddressId");

                    b.HasIndex("CustomerId")
                        .HasDatabaseName("IX_Stops_CustomerId");

                    b.HasIndex("TransportationId");

                    b.ToTable("Stops");
                });

            modelBuilder.Entity("TestApp2._0.Models.Transportation", b =>
                {
                    b.Property<int>("TransportationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TransportationId"));

                    b.Property<DateTime?>("ActualArrivalTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("CompletedStopsCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DepartureTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("DriverId")
                        .HasColumnType("int");

                    b.Property<int?>("LastStopId")
                        .HasColumnType("int");

                    b.Property<int>("TransportationStatus")
                        .HasColumnType("int");

                    b.Property<int>("TruckId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("TransportationId");

                    b.HasIndex("DriverId")
                        .IsUnique();

                    b.ToTable("Transportations");
                });

            modelBuilder.Entity("TestApp2._0.Models.Vehicle", b =>
                {
                    b.Property<int>("VehicleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VehicleId"));

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<string>("LicensePlate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Make")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("VIN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VehicleId");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("TestApp2._0.Models.Delivery", b =>
                {
                    b.HasOne("TestApp2._0.Models.Stop", "Stop")
                        .WithMany("Deliveries")
                        .HasForeignKey("StopId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Stop");
                });

            modelBuilder.Entity("TestApp2._0.Models.DeliveryItem", b =>
                {
                    b.HasOne("TestApp2._0.Models.Delivery", "CurrentDelivery")
                        .WithMany("DeliveryItems")
                        .HasForeignKey("CurrentDeliveryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TestApp2._0.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("CurrentDelivery");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("TestApp2._0.Models.Stop", b =>
                {
                    b.HasOne("TestApp2._0.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TestApp2._0.Models.Customer", "Customer")
                        .WithOne("Stop")
                        .HasForeignKey("TestApp2._0.Models.Stop", "CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TestApp2._0.Models.Transportation", "CurrentTransportation")
                        .WithMany("Stops")
                        .HasForeignKey("TransportationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Address");

                    b.Navigation("CurrentTransportation");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("TestApp2._0.Models.Transportation", b =>
                {
                    b.HasOne("TestApp2._0.Models.Driver", "Driver")
                        .WithOne("Transportation")
                        .HasForeignKey("TestApp2._0.Models.Transportation", "DriverId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TestApp2._0.Models.Vehicle", "Truck")
                        .WithOne("Transportation")
                        .HasForeignKey("TestApp2._0.Models.Transportation", "DriverId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Driver");

                    b.Navigation("Truck");
                });

            modelBuilder.Entity("TestApp2._0.Models.Customer", b =>
                {
                    b.Navigation("Stop");
                });

            modelBuilder.Entity("TestApp2._0.Models.Delivery", b =>
                {
                    b.Navigation("DeliveryItems");
                });

            modelBuilder.Entity("TestApp2._0.Models.Driver", b =>
                {
                    b.Navigation("Transportation");
                });

            modelBuilder.Entity("TestApp2._0.Models.Stop", b =>
                {
                    b.Navigation("Deliveries");
                });

            modelBuilder.Entity("TestApp2._0.Models.Transportation", b =>
                {
                    b.Navigation("Stops");
                });

            modelBuilder.Entity("TestApp2._0.Models.Vehicle", b =>
                {
                    b.Navigation("Transportation")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
