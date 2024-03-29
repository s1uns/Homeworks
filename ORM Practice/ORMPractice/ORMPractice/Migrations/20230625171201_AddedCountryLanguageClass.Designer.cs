﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ORMPractice;

#nullable disable

namespace ORMPractice.Migrations
{
    [DbContext(typeof(AppContext))]
    [Migration("20230625171201_AddedCountryLanguageClass")]
    partial class AddedCountryLanguageClass
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ORMPractice.Models.Continent", b =>
                {
                    b.Property<long>("ContinentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ContinentId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ContinentId");

                    b.ToTable("Continents");
                });

            modelBuilder.Entity("ORMPractice.Models.Country", b =>
                {
                    b.Property<long>("CountryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("CountryId"));

                    b.Property<decimal>("Area")
                        .HasColumnType("decimal(18,2)");

                    b.Property<long>("CountryCode2")
                        .HasColumnType("bigint");

                    b.Property<long>("CountryCode3")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("NationalDay")
                        .HasColumnType("datetime2");

                    b.Property<long>("RegionId")
                        .HasColumnType("bigint");

                    b.HasKey("CountryId");

                    b.HasIndex("RegionId");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("ORMPractice.Models.Country_Language", b =>
                {
                    b.Property<long>("CountryLanguageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("CountryLanguageId"));

                    b.Property<long>("CountryId")
                        .HasColumnType("bigint");

                    b.Property<long>("LanguageId")
                        .HasColumnType("bigint");

                    b.Property<bool>("Official")
                        .HasColumnType("bit");

                    b.HasKey("CountryLanguageId");

                    b.HasIndex("CountryId");

                    b.HasIndex("LanguageId");

                    b.ToTable("Country_Languages");
                });

            modelBuilder.Entity("ORMPractice.Models.Country_Stat", b =>
                {
                    b.Property<long>("CountryYearId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("CountryYearId"));

                    b.Property<long>("CountryId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("GDP")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Population")
                        .HasColumnType("int");

                    b.Property<long>("Year")
                        .HasColumnType("bigint");

                    b.HasKey("CountryYearId");

                    b.HasIndex("CountryId");

                    b.ToTable("Country_Stats");
                });

            modelBuilder.Entity("ORMPractice.Models.Language", b =>
                {
                    b.Property<long>("LanguageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("LanguageId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LanguageId");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("ORMPractice.Models.Region", b =>
                {
                    b.Property<long>("RegionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("RegionId"));

                    b.Property<long>("ContinentId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RegionId");

                    b.HasIndex("ContinentId");

                    b.ToTable("Regions");
                });

            modelBuilder.Entity("ORMPractice.Models.Country", b =>
                {
                    b.HasOne("ORMPractice.Models.Region", "Region")
                        .WithMany()
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Region");
                });

            modelBuilder.Entity("ORMPractice.Models.Country_Language", b =>
                {
                    b.HasOne("ORMPractice.Models.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ORMPractice.Models.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");

                    b.Navigation("Language");
                });

            modelBuilder.Entity("ORMPractice.Models.Country_Stat", b =>
                {
                    b.HasOne("ORMPractice.Models.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("ORMPractice.Models.Region", b =>
                {
                    b.HasOne("ORMPractice.Models.Continent", "Continent")
                        .WithMany()
                        .HasForeignKey("ContinentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Continent");
                });
#pragma warning restore 612, 618
        }
    }
}
