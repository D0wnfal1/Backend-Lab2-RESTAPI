﻿// <auto-generated />
using System;
using Backend_Lab2_RESTAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Backend_Lab2_RESTAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241121125908_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Backend_Lab2_RESTAPI.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryName = "Food"
                        },
                        new
                        {
                            Id = 2,
                            CategoryName = "Entertainment"
                        });
                });

            modelBuilder.Entity("Backend_Lab2_RESTAPI.Models.Currency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Currencies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "USD"
                        },
                        new
                        {
                            Id = 2,
                            Name = "EUR"
                        },
                        new
                        {
                            Id = 3,
                            Name = "UAH"
                        });
                });

            modelBuilder.Entity("Backend_Lab2_RESTAPI.Models.Record", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("CurrencyId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("UserId");

                    b.ToTable("Records");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amount = 50.75m,
                            CategoryId = 1,
                            CreateTime = new DateTime(2024, 11, 21, 12, 59, 4, 663, DateTimeKind.Utc).AddTicks(6105),
                            CurrencyId = 2,
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            Amount = 20.00m,
                            CategoryId = 2,
                            CreateTime = new DateTime(2024, 11, 21, 12, 59, 4, 663, DateTimeKind.Utc).AddTicks(6125),
                            UserId = 2
                        });
                });

            modelBuilder.Entity("Backend_Lab2_RESTAPI.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("DefaultCurrencyId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DefaultCurrencyId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DefaultCurrencyId = 1,
                            Name = "John Doe"
                        },
                        new
                        {
                            Id = 2,
                            DefaultCurrencyId = 3,
                            Name = "Jane Smith"
                        });
                });

            modelBuilder.Entity("Backend_Lab2_RESTAPI.Models.Record", b =>
                {
                    b.HasOne("Backend_Lab2_RESTAPI.Models.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Backend_Lab2_RESTAPI.Models.Currency", "Currency")
                        .WithMany()
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Backend_Lab2_RESTAPI.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Currency");
                });

            modelBuilder.Entity("Backend_Lab2_RESTAPI.Models.User", b =>
                {
                    b.HasOne("Backend_Lab2_RESTAPI.Models.Currency", "DefaultCurrency")
                        .WithMany()
                        .HasForeignKey("DefaultCurrencyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("DefaultCurrency");
                });
#pragma warning restore 612, 618
        }
    }
}
