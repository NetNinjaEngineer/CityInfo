﻿// <auto-generated />
using System;
using CityInfo.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CityInfo.API.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240127211017_AddIdentity")]
    partial class AddIdentity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CityInfo.API.Models.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("CityInfo.API.Models.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("VARCHAR");

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("VARCHAR");

                    b.Property<int>("Population")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Cities", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Country = "USA",
                            Latitude = 40.712800000000001,
                            Longitude = -74.006,
                            Name = "New York",
                            Population = 8398748
                        },
                        new
                        {
                            Id = 2,
                            Country = "UK",
                            Latitude = 51.509900000000002,
                            Longitude = -0.11799999999999999,
                            Name = "London",
                            Population = 8982000
                        },
                        new
                        {
                            Id = 3,
                            Country = "Japan",
                            Latitude = 35.689500000000002,
                            Longitude = 139.6917,
                            Name = "Tokyo",
                            Population = 13929286
                        },
                        new
                        {
                            Id = 4,
                            Country = "China",
                            Latitude = 39.904200000000003,
                            Longitude = 116.4074,
                            Name = "Beijing",
                            Population = 21516000
                        },
                        new
                        {
                            Id = 5,
                            Country = "France",
                            Latitude = 48.8566,
                            Longitude = 2.3521999999999998,
                            Name = "Paris",
                            Population = 2140526
                        },
                        new
                        {
                            Id = 6,
                            Country = "India",
                            Latitude = 28.613900000000001,
                            Longitude = 77.209000000000003,
                            Name = "Delhi",
                            Population = 30290936
                        },
                        new
                        {
                            Id = 7,
                            Country = "Brazil",
                            Latitude = -23.5505,
                            Longitude = -46.633299999999998,
                            Name = "Sao Paulo",
                            Population = 2200281
                        },
                        new
                        {
                            Id = 8,
                            Country = "Turkey",
                            Latitude = 41.008200000000002,
                            Longitude = 28.978400000000001,
                            Name = "Istanbul",
                            Population = 15462435
                        },
                        new
                        {
                            Id = 9,
                            Country = "Nigeria",
                            Latitude = 6.5244,
                            Longitude = 3.3792,
                            Name = "Lagos",
                            Population = 14497000
                        },
                        new
                        {
                            Id = 10,
                            Country = "Russia",
                            Latitude = 55.755800000000001,
                            Longitude = 37.617600000000003,
                            Name = "Moscow",
                            Population = 12615882
                        },
                        new
                        {
                            Id = 11,
                            Country = "India",
                            Latitude = 19.076000000000001,
                            Longitude = 72.877700000000004,
                            Name = "Mumbai",
                            Population = 12691836
                        },
                        new
                        {
                            Id = 12,
                            Country = "Egypt",
                            Latitude = 30.0444,
                            Longitude = 31.235700000000001,
                            Name = "Cairo",
                            Population = 20484965
                        },
                        new
                        {
                            Id = 13,
                            Country = "Mexico",
                            Latitude = 19.432600000000001,
                            Longitude = -99.133200000000002,
                            Name = "Mexico City",
                            Population = 9209944
                        },
                        new
                        {
                            Id = 14,
                            Country = "Thailand",
                            Latitude = 13.7563,
                            Longitude = 100.5018,
                            Name = "Bangkok",
                            Population = 8280925
                        },
                        new
                        {
                            Id = 15,
                            Country = "South Korea",
                            Latitude = 37.566499999999998,
                            Longitude = 126.97799999999999,
                            Name = "Seoul",
                            Population = 9720846
                        });
                });

            modelBuilder.Entity("CityInfo.API.Models.PointOfInterest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("VARCHAR");

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1500)
                        .HasColumnType("VARCHAR");

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("VARCHAR");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("PointOfInterests", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Category = "Park",
                            CityId = 1,
                            Description = "A large urban park in Manhattan.",
                            Latitude = 40.7851,
                            Longitude = -73.968299999999999,
                            Name = "Central Park"
                        },
                        new
                        {
                            Id = 2,
                            Category = "Landmark",
                            CityId = 1,
                            Description = "Iconic skyscraper in Midtown Manhattan.",
                            Latitude = 40.748399999999997,
                            Longitude = -73.985699999999994,
                            Name = "Empire State Building"
                        },
                        new
                        {
                            Id = 3,
                            Category = "Park",
                            CityId = 2,
                            Description = "One of the largest parks in London.",
                            Latitude = 51.507399999999997,
                            Longitude = -0.16569999999999999,
                            Name = "Hyde Park"
                        },
                        new
                        {
                            Id = 4,
                            Category = "Museum",
                            CityId = 2,
                            Description = "World-famous museum of art and antiquities.",
                            Latitude = 51.519399999999997,
                            Longitude = -0.127,
                            Name = "British Museum"
                        },
                        new
                        {
                            Id = 5,
                            Category = "Park",
                            CityId = 3,
                            Description = "Famous public park in central Tokyo.",
                            Latitude = 35.714599999999997,
                            Longitude = 139.7732,
                            Name = "Ueno Park"
                        },
                        new
                        {
                            Id = 6,
                            Category = "Landmark",
                            CityId = 3,
                            Description = "Iconic communications and observation tower.",
                            Latitude = 35.6586,
                            Longitude = 139.74539999999999,
                            Name = "Tokyo Tower"
                        },
                        new
                        {
                            Id = 7,
                            Category = "Park",
                            CityId = 4,
                            Description = "A large urban park in Manhattan.",
                            Latitude = 40.7851,
                            Longitude = -73.968299999999999,
                            Name = "Central Park"
                        },
                        new
                        {
                            Id = 8,
                            Category = "Landmark",
                            CityId = 4,
                            Description = "Iconic skyscraper in Midtown Manhattan.",
                            Latitude = 40.748399999999997,
                            Longitude = -73.985699999999994,
                            Name = "Empire State Building"
                        },
                        new
                        {
                            Id = 9,
                            Category = "Park",
                            CityId = 5,
                            Description = "One of the largest parks in London.",
                            Latitude = 51.507399999999997,
                            Longitude = -0.16569999999999999,
                            Name = "Hyde Park"
                        },
                        new
                        {
                            Id = 10,
                            Category = "Museum",
                            CityId = 5,
                            Description = "World-famous museum of art and antiquities.",
                            Latitude = 51.519399999999997,
                            Longitude = -0.127,
                            Name = "British Museum"
                        },
                        new
                        {
                            Id = 11,
                            Category = "Park",
                            CityId = 6,
                            Description = "Famous public park in central Tokyo.",
                            Latitude = 35.714599999999997,
                            Longitude = 139.7732,
                            Name = "Ueno Park"
                        },
                        new
                        {
                            Id = 12,
                            Category = "Landmark",
                            CityId = 5,
                            Description = "Iconic communications and observation tower.",
                            Latitude = 35.6586,
                            Longitude = 139.74539999999999,
                            Name = "Tokyo Tower"
                        },
                        new
                        {
                            Id = 13,
                            Category = "Park",
                            CityId = 7,
                            Description = "A large urban park in Manhattan.",
                            Latitude = 40.7851,
                            Longitude = -73.968299999999999,
                            Name = "Central Park"
                        },
                        new
                        {
                            Id = 14,
                            Category = "Landmark",
                            CityId = 8,
                            Description = "Iconic skyscraper in Midtown Manhattan.",
                            Latitude = 40.748399999999997,
                            Longitude = -73.985699999999994,
                            Name = "Empire State Building"
                        },
                        new
                        {
                            Id = 15,
                            Category = "Park",
                            CityId = 9,
                            Description = "One of the largest parks in London.",
                            Latitude = 51.507399999999997,
                            Longitude = -0.16569999999999999,
                            Name = "Hyde Park"
                        },
                        new
                        {
                            Id = 16,
                            Category = "Museum",
                            CityId = 10,
                            Description = "World-famous museum of art and antiquities.",
                            Latitude = 51.519399999999997,
                            Longitude = -0.127,
                            Name = "British Museum"
                        },
                        new
                        {
                            Id = 17,
                            Category = "Park",
                            CityId = 11,
                            Description = "Famous public park in central Tokyo.",
                            Latitude = 35.714599999999997,
                            Longitude = 139.7732,
                            Name = "Ueno Park"
                        },
                        new
                        {
                            Id = 18,
                            Category = "Landmark",
                            CityId = 12,
                            Description = "Iconic communications and observation tower.",
                            Latitude = 35.6586,
                            Longitude = 139.74539999999999,
                            Name = "Tokyo Tower"
                        },
                        new
                        {
                            Id = 19,
                            Category = "Park",
                            CityId = 13,
                            Description = "A large urban park in Manhattan.",
                            Latitude = 40.7851,
                            Longitude = -73.968299999999999,
                            Name = "Central Park"
                        },
                        new
                        {
                            Id = 20,
                            Category = "Landmark",
                            CityId = 15,
                            Description = "Iconic skyscraper in Midtown Manhattan.",
                            Latitude = 40.748399999999997,
                            Longitude = -73.985699999999994,
                            Name = "Empire State Building"
                        },
                        new
                        {
                            Id = 21,
                            Category = "Park",
                            CityId = 13,
                            Description = "One of the largest parks in London.",
                            Latitude = 51.507399999999997,
                            Longitude = -0.16569999999999999,
                            Name = "Hyde Park"
                        },
                        new
                        {
                            Id = 22,
                            Category = "Museum",
                            CityId = 13,
                            Description = "World-famous museum of art and antiquities.",
                            Latitude = 51.519399999999997,
                            Longitude = -0.127,
                            Name = "British Museum"
                        },
                        new
                        {
                            Id = 23,
                            Category = "Park",
                            CityId = 10,
                            Description = "Famous public park in central Tokyo.",
                            Latitude = 35.714599999999997,
                            Longitude = 139.7732,
                            Name = "Ueno Park"
                        },
                        new
                        {
                            Id = 24,
                            Category = "Landmark",
                            CityId = 14,
                            Description = "Iconic communications and observation tower.",
                            Latitude = 35.6586,
                            Longitude = 139.74539999999999,
                            Name = "Tokyo Tower"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("CityInfo.API.Models.PointOfInterest", b =>
                {
                    b.HasOne("CityInfo.API.Models.City", "City")
                        .WithMany("PointOfInterests")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("CityInfo.API.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("CityInfo.API.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CityInfo.API.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("CityInfo.API.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CityInfo.API.Models.City", b =>
                {
                    b.Navigation("PointOfInterests");
                });
#pragma warning restore 612, 618
        }
    }
}
