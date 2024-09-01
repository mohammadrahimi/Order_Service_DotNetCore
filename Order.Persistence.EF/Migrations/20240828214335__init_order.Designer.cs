﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Order.Persistence.EF.Context;

#nullable disable

namespace Order.Persistence.EF.Migrations
{
    [DbContext(typeof(OrderDbContext))]
    [Migration("20240828214335__init_order")]
    partial class _init_order
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Order.Domain.Order.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CustomerID")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("CustomerID");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<DateTime>("OrderDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Orders", (string)null);
                });

            modelBuilder.Entity("Order.Domain.Order.Order", b =>
                {
                    b.OwnsMany("Order.Domain.Order.Entites.OrderItem", "OrderItems", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("OrderItemId");

                            b1.Property<Guid>("OrderId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("ProductId")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("ProductId");

                            b1.Property<int>("Quantity")
                                .HasColumnType("int");

                            b1.HasKey("Id");

                            b1.HasIndex("OrderId");

                            b1.ToTable("OrderItems", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("OrderId");

                            b1.OwnsOne("Order.Domain.Order.ValueObjects.Price", "Price", b2 =>
                                {
                                    b2.Property<Guid>("OrderItemId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<float>("Amount")
                                        .HasColumnType("real");

                                    b2.Property<string>("Currency")
                                        .IsRequired()
                                        .HasMaxLength(10)
                                        .HasColumnType("nvarchar(10)");

                                    b2.HasKey("OrderItemId");

                                    b2.ToTable("OrderItems");

                                    b2.WithOwner()
                                        .HasForeignKey("OrderItemId");
                                });

                            b1.Navigation("Price")
                                .IsRequired();
                        });

                    b.OwnsOne("Order.Domain.Order.ValueObjects.Price", "TotalPrice", b1 =>
                        {
                            b1.Property<Guid>("OrderId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<float>("Amount")
                                .HasColumnType("real");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)");

                            b1.HasKey("OrderId");

                            b1.ToTable("Orders");

                            b1.WithOwner()
                                .HasForeignKey("OrderId");
                        });

                    b.Navigation("OrderItems");

                    b.Navigation("TotalPrice")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}