﻿// <auto-generated />
using System;
using FitBackend;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FitBackend.Migrations
{
    [DbContext(typeof(FitBackendContext))]
    [Migration("20250420102609_ReworkCharacteristicsExtendCompany")]
    partial class ReworkCharacteristicsExtendCompany
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FitBackend.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.PrimitiveCollection<string[]>("Comments")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<int>("FinancialReportingCurrency")
                        .HasColumnType("integer");

                    b.Property<int>("FinancialReportingInterval")
                        .HasColumnType("integer");

                    b.Property<int>("FinancialReportingMultiplier")
                        .HasColumnType("integer");

                    b.PrimitiveCollection<string[]>("FinancialReportingSourceUrls")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<string>("LogoUrl")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("StockExchange")
                        .HasColumnType("integer");

                    b.Property<string>("StockExchangeCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("StockExchangeCurrency")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("FitBackend.CompanyHistoricCurrencyCharacteristic", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<int>("Currency")
                        .HasColumnType("integer");

                    b.Property<Guid>("HistoricCurrencyCharacteristicId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("HistoricCurrencyCharacteristicId");

                    b.ToTable("CompanyHistoricCurrencyCharacteristics");
                });

            modelBuilder.Entity("FitBackend.CompanyHistoricNumberCharacteristic", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("HistoricNumberCharacteristicId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("HistoricNumberCharacteristicId");

                    b.ToTable("CompanyHistoricNumberCharacteristics");
                });

            modelBuilder.Entity("FitBackend.CompanyNumberCharacteristic", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("NumberCharacteristicId")
                        .HasColumnType("uuid");

                    b.Property<float>("Value")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("NumberCharacteristicId");

                    b.ToTable("CompanyNumberCharacteristics");
                });

            modelBuilder.Entity("FitBackend.CompanyTextCharacteristic", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TextCharacteristicId")
                        .HasColumnType("uuid");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("TextCharacteristicId");

                    b.ToTable("CompanyTextCharacteristics");
                });

            modelBuilder.Entity("FitBackend.HistoricCurrencyCharacteristic", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("HistoricCurrencyCharacteristics");
                });

            modelBuilder.Entity("FitBackend.HistoricNumberCharacteristic", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Unit")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("HistoricNumberCharacteristics");
                });

            modelBuilder.Entity("FitBackend.HistoricValue", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CompanyHistoricCurrencyCharacteristicId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CompanyHistoricNumberCharacteristicId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<float>("Value")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("CompanyHistoricCurrencyCharacteristicId");

                    b.HasIndex("CompanyHistoricNumberCharacteristicId");

                    b.ToTable("HistoricValues");
                });

            modelBuilder.Entity("FitBackend.NumberCharacteristic", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Unit")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("NumberCharacteristics");
                });

            modelBuilder.Entity("FitBackend.TextCharacteristic", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("TextCharacteristics");
                });

            modelBuilder.Entity("FitBackend.CompanyHistoricCurrencyCharacteristic", b =>
                {
                    b.HasOne("FitBackend.Company", "Company")
                        .WithMany("CompanyHistoricCurrencyCharacteristics")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FitBackend.HistoricCurrencyCharacteristic", "HistoricCurrencyCharacteristic")
                        .WithMany("CompanyHistoricCurrencyCharacteristics")
                        .HasForeignKey("HistoricCurrencyCharacteristicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("HistoricCurrencyCharacteristic");
                });

            modelBuilder.Entity("FitBackend.CompanyHistoricNumberCharacteristic", b =>
                {
                    b.HasOne("FitBackend.Company", "Company")
                        .WithMany("CompanyHistoricNumberCharacteristics")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FitBackend.HistoricNumberCharacteristic", "HistoricNumberCharacteristic")
                        .WithMany("CompanyHistoricNumberCharacteristics")
                        .HasForeignKey("HistoricNumberCharacteristicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("HistoricNumberCharacteristic");
                });

            modelBuilder.Entity("FitBackend.CompanyNumberCharacteristic", b =>
                {
                    b.HasOne("FitBackend.Company", "Company")
                        .WithMany("CompanyNumberCharacteristics")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FitBackend.NumberCharacteristic", "NumberCharacteristic")
                        .WithMany("CompanyNumberCharacteristics")
                        .HasForeignKey("NumberCharacteristicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("NumberCharacteristic");
                });

            modelBuilder.Entity("FitBackend.CompanyTextCharacteristic", b =>
                {
                    b.HasOne("FitBackend.Company", "Company")
                        .WithMany("CompanyTextCharacteristics")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FitBackend.TextCharacteristic", "TextCharacteristic")
                        .WithMany("CompanyTextCharacteristics")
                        .HasForeignKey("TextCharacteristicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("TextCharacteristic");
                });

            modelBuilder.Entity("FitBackend.HistoricValue", b =>
                {
                    b.HasOne("FitBackend.CompanyHistoricCurrencyCharacteristic", "CompanyHistoricCurrencyCharacteristic")
                        .WithMany("Values")
                        .HasForeignKey("CompanyHistoricCurrencyCharacteristicId");

                    b.HasOne("FitBackend.CompanyHistoricNumberCharacteristic", "CompanyHistoricNumberCharacteristic")
                        .WithMany("Values")
                        .HasForeignKey("CompanyHistoricNumberCharacteristicId");

                    b.Navigation("CompanyHistoricCurrencyCharacteristic");

                    b.Navigation("CompanyHistoricNumberCharacteristic");
                });

            modelBuilder.Entity("FitBackend.Company", b =>
                {
                    b.Navigation("CompanyHistoricCurrencyCharacteristics");

                    b.Navigation("CompanyHistoricNumberCharacteristics");

                    b.Navigation("CompanyNumberCharacteristics");

                    b.Navigation("CompanyTextCharacteristics");
                });

            modelBuilder.Entity("FitBackend.CompanyHistoricCurrencyCharacteristic", b =>
                {
                    b.Navigation("Values");
                });

            modelBuilder.Entity("FitBackend.CompanyHistoricNumberCharacteristic", b =>
                {
                    b.Navigation("Values");
                });

            modelBuilder.Entity("FitBackend.HistoricCurrencyCharacteristic", b =>
                {
                    b.Navigation("CompanyHistoricCurrencyCharacteristics");
                });

            modelBuilder.Entity("FitBackend.HistoricNumberCharacteristic", b =>
                {
                    b.Navigation("CompanyHistoricNumberCharacteristics");
                });

            modelBuilder.Entity("FitBackend.NumberCharacteristic", b =>
                {
                    b.Navigation("CompanyNumberCharacteristics");
                });

            modelBuilder.Entity("FitBackend.TextCharacteristic", b =>
                {
                    b.Navigation("CompanyTextCharacteristics");
                });
#pragma warning restore 612, 618
        }
    }
}
