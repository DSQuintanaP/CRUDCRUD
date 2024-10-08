﻿// <auto-generated />
using System;
using CRUDCRUD.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CRUDCRUD.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240926162157_fourthGear")]
    partial class fourthGear
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CRUDCRUD.Models.Cliente", b =>
                {
                    b.Property<int>("IDCustomer")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IDCustomer"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<string>("customerName")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<int>("numberPhone")
                        .HasColumnType("int");

                    b.HasKey("IDCustomer");

                    b.ToTable("clientes");
                });

            modelBuilder.Entity("CRUDCRUD.Models.Compras", b =>
                {
                    b.Property<int>("IDOrder")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IDOrder"));

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<int>("IDCustomer")
                        .HasColumnType("int");

                    b.Property<DateOnly>("fecha")
                        .HasColumnType("date");

                    b.Property<decimal>("totalValue")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("IDOrder");

                    b.HasIndex("IDCustomer");

                    b.ToTable("compras");
                });

            modelBuilder.Entity("CRUDCRUD.Models.DetalleCompra", b =>
                {
                    b.Property<int>("prductID")
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<int>("IDOrder")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.HasKey("prductID", "IDOrder");

                    b.HasIndex("IDOrder");

                    b.ToTable("detalles");
                });

            modelBuilder.Entity("CRUDCRUD.Models.Producto", b =>
                {
                    b.Property<int>("prductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("prductID"));

                    b.Property<string>("productName")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<int>("productsInStock")
                        .HasColumnType("int");

                    b.HasKey("prductID");

                    b.ToTable("Productos");
                });

            modelBuilder.Entity("CRUDCRUD.Models.Compras", b =>
                {
                    b.HasOne("CRUDCRUD.Models.Cliente", "Cliente")
                        .WithMany("Compras")
                        .HasForeignKey("IDCustomer")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("CRUDCRUD.Models.DetalleCompra", b =>
                {
                    b.HasOne("CRUDCRUD.Models.Compras", "compras")
                        .WithMany("DetalleCompras")
                        .HasForeignKey("IDOrder")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CRUDCRUD.Models.Producto", "Producto")
                        .WithMany("DetalleCompras")
                        .HasForeignKey("prductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Producto");

                    b.Navigation("compras");
                });

            modelBuilder.Entity("CRUDCRUD.Models.Cliente", b =>
                {
                    b.Navigation("Compras");
                });

            modelBuilder.Entity("CRUDCRUD.Models.Compras", b =>
                {
                    b.Navigation("DetalleCompras");
                });

            modelBuilder.Entity("CRUDCRUD.Models.Producto", b =>
                {
                    b.Navigation("DetalleCompras");
                });
#pragma warning restore 612, 618
        }
    }
}
