﻿// <auto-generated />
using System;
using MDWebCoreAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MDWebCoreAPI.Migrations
{
    [DbContext(typeof(MyDbContext))]
    partial class MyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.32")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MDWebCoreAPI.Models.OrderDetail", b =>
                {
                    b.Property<int>("DetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("DetailId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderDetails");

                    b.HasData(
                        new
                        {
                            DetailId = 1,
                            OrderId = 1,
                            Price = 100m,
                            ProductId = 1,
                            Quantity = 1
                        },
                        new
                        {
                            DetailId = 2,
                            OrderId = 1,
                            Price = 200m,
                            ProductId = 2,
                            Quantity = 2
                        },
                        new
                        {
                            DetailId = 3,
                            OrderId = 2,
                            Price = 300m,
                            ProductId = 3,
                            Quantity = 3
                        });
                });

            modelBuilder.Entity("MDWebCoreAPI.Models.OrderMaster", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CustomerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsComplete")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("OrderDate")
                        .HasColumnType("datetime2");

                    b.HasKey("OrderId");

                    b.ToTable("OrderMasters");

                    b.HasData(
                        new
                        {
                            OrderId = 1,
                            CustomerName = "John Doe",
                            IsComplete = true,
                            OrderDate = new DateTime(2023, 8, 2, 14, 52, 17, 74, DateTimeKind.Local).AddTicks(5669)
                        },
                        new
                        {
                            OrderId = 2,
                            CustomerName = "Jane Smith",
                            IsComplete = false,
                            OrderDate = new DateTime(2023, 8, 1, 14, 52, 17, 75, DateTimeKind.Local).AddTicks(3900)
                        });
                });

            modelBuilder.Entity("MDWebCoreAPI.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            Name = "Product 1"
                        },
                        new
                        {
                            ProductId = 2,
                            Name = "Product 2"
                        },
                        new
                        {
                            ProductId = 3,
                            Name = "Product 3"
                        });
                });

            modelBuilder.Entity("MDWebCoreAPI.Models.OrderDetail", b =>
                {
                    b.HasOne("MDWebCoreAPI.Models.OrderMaster", "OrderMaster")
                        .WithMany("OrderDetail")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MDWebCoreAPI.Models.Product", "Product")
                        .WithMany("Details")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}