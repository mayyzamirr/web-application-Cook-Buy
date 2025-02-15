﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication.Data;

namespace WebApplication.Migrations
{
    [DbContext(typeof(WebApplicationContext))]
    [Migration("20210523095213_namecard")]
    partial class namecard
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebProject.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("WebProject.Models.CreditCard", b =>
                {
                    b.Property<string>("NumberCard")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("NumberOrder")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NumberCard");

                    b.HasIndex("NumberOrder");

                    b.ToTable("CreditCard");
                });

            modelBuilder.Entity("WebProject.Models.OrderDetails", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreditCardOrderNumberCard")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumberOrder")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PriceOrder")
                        .HasColumnType("int");

                    b.Property<string>("addressOrder")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OrderId");

                    b.HasIndex("CreditCardOrderNumberCard");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("WebProject.Models.Orders", b =>
                {
                    b.Property<int>("NumberOrder")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DateOrder")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId1")
                        .HasColumnType("int");

                    b.HasKey("NumberOrder");

                    b.HasIndex("UserId1");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("WebProject.Models.Recipe", b =>
                {
                    b.Property<int>("RecipeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Body")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Howtomake")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OrderDetailsOrderId")
                        .HasColumnType("int");

                    b.Property<int?>("OrdersNumberOrder")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<string>("Video")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RecipeId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("OrderDetailsOrderId");

                    b.HasIndex("OrdersNumberOrder");

                    b.ToTable("Recipe");
                });

            modelBuilder.Entity("WebProject.Models.Users", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Password")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WebProject.Models.CreditCard", b =>
                {
                    b.HasOne("WebProject.Models.Orders", "OrderId")
                        .WithMany()
                        .HasForeignKey("NumberOrder");

                    b.Navigation("OrderId");
                });

            modelBuilder.Entity("WebProject.Models.OrderDetails", b =>
                {
                    b.HasOne("WebProject.Models.CreditCard", "CreditCardOrder")
                        .WithMany()
                        .HasForeignKey("CreditCardOrderNumberCard");

                    b.Navigation("CreditCardOrder");
                });

            modelBuilder.Entity("WebProject.Models.Orders", b =>
                {
                    b.HasOne("WebProject.Models.Users", "UserId")
                        .WithMany("Orders")
                        .HasForeignKey("UserId1");

                    b.Navigation("UserId");
                });

            modelBuilder.Entity("WebProject.Models.Recipe", b =>
                {
                    b.HasOne("WebProject.Models.Category", "Category")
                        .WithMany("Recipes")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebProject.Models.OrderDetails", null)
                        .WithMany("Recipes")
                        .HasForeignKey("OrderDetailsOrderId");

                    b.HasOne("WebProject.Models.Orders", null)
                        .WithMany("Recipes")
                        .HasForeignKey("OrdersNumberOrder");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("WebProject.Models.Category", b =>
                {
                    b.Navigation("Recipes");
                });

            modelBuilder.Entity("WebProject.Models.OrderDetails", b =>
                {
                    b.Navigation("Recipes");
                });

            modelBuilder.Entity("WebProject.Models.Orders", b =>
                {
                    b.Navigation("Recipes");
                });

            modelBuilder.Entity("WebProject.Models.Users", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
