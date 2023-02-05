﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShopBridgeAPI.Models;

#nullable disable

namespace ShopBridgeAPI.Migrations
{
    [DbContext(typeof(ShopBridgeContext))]
    [Migration("20230204182851_AddCategoryNameIndex")]
    partial class AddCategoryNameIndex
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ShopBridgeAPI.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("DiscountPercentage")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Category", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 501,
                            DiscountPercentage = 2,
                            Name = "Books"
                        },
                        new
                        {
                            Id = 502,
                            Name = "Furniture"
                        },
                        new
                        {
                            Id = 503,
                            Name = "Home Appliances"
                        },
                        new
                        {
                            Id = 504,
                            DiscountPercentage = 10,
                            Name = "Mobile Accessories"
                        });
                });

            modelBuilder.Entity("ShopBridgeAPI.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Product", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 501,
                            Description = "A Brief History of Human Kind",
                            Name = "Sapiens - By Yuval Noah Harari",
                            Price = 460.0
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 501,
                            Description = "An Easy and Proven way to create good habits",
                            Name = "Atomic Habits - By James Clear",
                            Price = 350.0
                        },
                        new
                        {
                            Id = 3,
                            CategoryId = 501,
                            Description = "Timeless lessons on wealth, greed and Happiness",
                            Name = "The Psychology of Money - By Morgan Housel",
                            Price = 420.0
                        },
                        new
                        {
                            Id = 4,
                            CategoryId = 501,
                            Description = "The two ways we make decisions",
                            Name = "Thinking fast and slow - By Daniel Kahneman",
                            Price = 460.0
                        },
                        new
                        {
                            Id = 5,
                            CategoryId = 502,
                            Description = "Decorative set of shelves",
                            Name = "Wooden Wall Shelf",
                            Price = 2000.0
                        },
                        new
                        {
                            Id = 6,
                            CategoryId = 502,
                            Description = "Perfect TV Unit for your home",
                            Name = "Wooden TV Unit",
                            Price = 18000.0
                        },
                        new
                        {
                            Id = 7,
                            CategoryId = 502,
                            Description = "Seating Capacity - 8",
                            Name = "Homeify Sofa set",
                            Price = 40000.0
                        },
                        new
                        {
                            Id = 8,
                            CategoryId = 502,
                            Description = "Wooden Shoe rack with multiple racks",
                            Name = "Shoe rack",
                            Price = 3500.0
                        },
                        new
                        {
                            Id = 9,
                            CategoryId = 503,
                            Description = "Best company for your home maker",
                            Name = "XYZ Wet Grinder",
                            Price = 6000.0
                        },
                        new
                        {
                            Id = 10,
                            CategoryId = 503,
                            Description = "Environment friendly Double door refrigerator",
                            Name = "Godrej Refrigerator",
                            Price = 18000.0
                        },
                        new
                        {
                            Id = 11,
                            CategoryId = 504,
                            Description = "Type-C 18W charger",
                            Name = "Samsung Mobile charger",
                            Price = 1349.0
                        },
                        new
                        {
                            Id = 12,
                            CategoryId = 504,
                            Description = "Best in class music",
                            Name = "JBL Wireless HeadPhones",
                            Price = 1499.0
                        });
                });

            modelBuilder.Entity("ShopBridgeAPI.Models.Product", b =>
                {
                    b.HasOne("ShopBridgeAPI.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("ShopBridgeAPI.Models.Category", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
