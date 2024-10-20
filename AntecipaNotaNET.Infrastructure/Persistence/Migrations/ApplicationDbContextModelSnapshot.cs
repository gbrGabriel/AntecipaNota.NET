﻿// <auto-generated />
using System;
using AntecipaNotaNET.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AntecipaNotaNET.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AntecipaNotaNET.Domain.Entities.Carrinho", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("NotaFiscalId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Carrinhos");
                });

            modelBuilder.Entity("AntecipaNotaNET.Domain.Entities.Empresa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Cnpj")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<decimal>("Faturamento")
                        .HasPrecision(18, 5)
                        .HasColumnType("decimal(18,5)");

                    b.Property<decimal>("Limite")
                        .HasPrecision(18, 5)
                        .HasColumnType("decimal(18,5)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Ramo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Empresas");
                });

            modelBuilder.Entity("AntecipaNotaNET.Domain.Entities.NotaFiscal", b =>
                {
                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataVencimento")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Desagio")
                        .HasPrecision(18, 5)
                        .HasColumnType("decimal(18,5)");

                    b.Property<int>("EmpresaId")
                        .HasColumnType("int");

                    b.Property<decimal>("ValorBruto")
                        .HasPrecision(18, 5)
                        .HasColumnType("decimal(18,5)");

                    b.Property<decimal>("ValorLiquido")
                        .HasPrecision(18, 5)
                        .HasColumnType("decimal(18,5)");

                    b.HasKey("Numero");

                    b.ToTable("Notas");
                });
#pragma warning restore 612, 618
        }
    }
}
