﻿// <auto-generated />
using System;
using DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DB.Migrations
{
    [DbContext(typeof(DI_MFSD_J_GarciaContext))]
    [Migration("20230613211901_addColumnDestinationExchangeRate")]
    partial class addColumnDestinationExchangeRate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DB.Models.ExchangeRate", b =>
                {
                    b.Property<int>("ExchangeRateID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ExchangeRateID"));

                    b.Property<string>("CurrencyBase")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CurrencyDestination")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Rate")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ExchangeRateID");

                    b.ToTable("ExchangeRate", (string)null);
                });

            modelBuilder.Entity("DB.Models.Rol", b =>
                {
                    b.Property<int>("RolID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RolID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RolID");

                    b.ToTable("Rol", (string)null);

                    b.HasData(
                        new
                        {
                            RolID = 1,
                            Name = "Admin"
                        },
                        new
                        {
                            RolID = 2,
                            Name = "General"
                        });
                });

            modelBuilder.Entity("DB.Models.Transaction", b =>
                {
                    b.Property<int>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TransactionId"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DestinationCurrency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("DestinationExchangeRate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ExchangeRate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("SourceCurrency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserRefID")
                        .HasColumnType("int");

                    b.HasKey("TransactionId");

                    b.HasIndex("UserRefID");

                    b.ToTable("Transaction", (string)null);
                });

            modelBuilder.Entity("DB.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RolRefID")
                        .HasColumnType("int");

                    b.HasKey("UserID");

                    b.HasIndex("RolRefID");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("DB.Models.Transaction", b =>
                {
                    b.HasOne("DB.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserRefID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DB.Models.User", b =>
                {
                    b.HasOne("DB.Models.Rol", "Rol")
                        .WithMany("Users")
                        .HasForeignKey("RolRefID");

                    b.Navigation("Rol");
                });

            modelBuilder.Entity("DB.Models.Rol", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
