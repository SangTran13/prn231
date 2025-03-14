﻿// <auto-generated />
using System;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BusinessObject.Migrations
{
    [DbContext(typeof(EBookStoreDBContext))]
    partial class EBookStoreDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BusinessObject.Models.Author", b =>
                {
                    b.Property<int>("author_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("author_id"));

                    b.Property<string>("address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("city")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("email_address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("first_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("last_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("state")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("zip")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("author_id");

                    b.ToTable("Author");

                    b.HasData(
                        new
                        {
                            author_id = 1,
                            address = "123 Main St",
                            city = "New York",
                            email_address = "johndoe@example.com",
                            first_name = "John",
                            last_name = "Doe",
                            phone = "123-456-7890",
                            state = "NY",
                            zip = "10001"
                        },
                        new
                        {
                            author_id = 2,
                            address = "456 Elm St",
                            city = "Los Angeles",
                            email_address = "janesmith@example.com",
                            first_name = "Jane",
                            last_name = "Smith",
                            phone = "234-567-8901",
                            state = "CA",
                            zip = "90001"
                        });
                });

            modelBuilder.Entity("BusinessObject.Models.Book", b =>
                {
                    b.Property<int>("book_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("book_id"));

                    b.Property<string>("advance")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("notes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("price")
                        .HasColumnType("float");

                    b.Property<int>("pub_id")
                        .HasColumnType("int");

                    b.Property<DateTime>("published_date")
                        .HasColumnType("datetime2");

                    b.Property<double>("royalty")
                        .HasColumnType("float");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ytd_sales")
                        .HasColumnType("int");

                    b.HasKey("book_id");

                    b.HasIndex("pub_id");

                    b.ToTable("Book");

                    b.HasData(
                        new
                        {
                            book_id = 1,
                            advance = "5000",
                            notes = "Bestseller",
                            price = 49.990000000000002,
                            pub_id = 1,
                            published_date = new DateTime(2023, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            royalty = 10.0,
                            title = "Learn C# in 30 Days",
                            type = "Programming",
                            ytd_sales = 1000
                        },
                        new
                        {
                            book_id = 2,
                            advance = "3000",
                            notes = "Highly rated",
                            price = 39.990000000000002,
                            pub_id = 2,
                            published_date = new DateTime(2022, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            royalty = 8.0,
                            title = "Entity Framework Core for Professionals",
                            type = "Database",
                            ytd_sales = 500
                        });
                });

            modelBuilder.Entity("BusinessObject.Models.BookAuthor", b =>
                {
                    b.Property<int>("author_id")
                        .HasColumnType("int");

                    b.Property<int>("book_id")
                        .HasColumnType("int");

                    b.Property<string>("author_order")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("royality_percentage")
                        .HasColumnType("float");

                    b.HasKey("author_id", "book_id");

                    b.HasIndex("book_id");

                    b.ToTable("BookAuthor");

                    b.HasData(
                        new
                        {
                            author_id = 1,
                            book_id = 1,
                            author_order = "1",
                            royality_percentage = 50.0
                        },
                        new
                        {
                            author_id = 2,
                            book_id = 2,
                            author_order = "1",
                            royality_percentage = 50.0
                        });
                });

            modelBuilder.Entity("BusinessObject.Models.Publisher", b =>
                {
                    b.Property<int>("pub_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("pub_id"));

                    b.Property<string>("city")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("publisher_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("state")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("pub_id");

                    b.ToTable("Publisher");

                    b.HasData(
                        new
                        {
                            pub_id = 1,
                            city = "New York",
                            country = "USA",
                            publisher_name = "Tech Books Publishing",
                            state = "NY"
                        },
                        new
                        {
                            pub_id = 2,
                            city = "Los Angeles",
                            country = "USA",
                            publisher_name = "Fiction World",
                            state = "CA"
                        });
                });

            modelBuilder.Entity("BusinessObject.Models.Role", b =>
                {
                    b.Property<int>("role_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("role_id"));

                    b.Property<string>("role_desc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("role_id");

                    b.ToTable("Role");

                    b.HasData(
                        new
                        {
                            role_id = 1,
                            role_desc = "Admin"
                        },
                        new
                        {
                            role_id = 2,
                            role_desc = "User"
                        });
                });

            modelBuilder.Entity("BusinessObject.Models.User", b =>
                {
                    b.Property<int>("user_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("user_id"));

                    b.Property<string>("email_address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("first_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("hire_date")
                        .HasColumnType("datetime2");

                    b.Property<string>("last_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("middle_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("pub_id")
                        .HasColumnType("int");

                    b.Property<int>("role_id")
                        .HasColumnType("int");

                    b.Property<string>("source")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("user_id");

                    b.HasIndex("pub_id");

                    b.HasIndex("role_id");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            user_id = 1,
                            email_address = "sangtn@fpt.com",
                            first_name = "Sang",
                            hire_date = new DateTime(2025, 2, 7, 21, 28, 35, 583, DateTimeKind.Local).AddTicks(944),
                            last_name = "Tran",
                            middle_name = "Ngoc",
                            password = "123",
                            pub_id = 1,
                            role_id = 1,
                            source = "Internal"
                        },
                        new
                        {
                            user_id = 2,
                            email_address = "quynx@fpt.com",
                            first_name = "Quy",
                            hire_date = new DateTime(2025, 2, 7, 21, 28, 35, 583, DateTimeKind.Local).AddTicks(958),
                            last_name = "Nguyen",
                            middle_name = "Xuan",
                            password = "123",
                            pub_id = 2,
                            role_id = 2,
                            source = "Internal"
                        });
                });

            modelBuilder.Entity("BusinessObject.Models.Book", b =>
                {
                    b.HasOne("BusinessObject.Models.Publisher", "Publisher")
                        .WithMany("Books")
                        .HasForeignKey("pub_id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Publisher");
                });

            modelBuilder.Entity("BusinessObject.Models.BookAuthor", b =>
                {
                    b.HasOne("BusinessObject.Models.Author", "Author")
                        .WithMany("BookAuthors")
                        .HasForeignKey("author_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessObject.Models.Book", "Book")
                        .WithMany("BookAuthors")
                        .HasForeignKey("book_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Book");
                });

            modelBuilder.Entity("BusinessObject.Models.User", b =>
                {
                    b.HasOne("BusinessObject.Models.Publisher", "Publisher")
                        .WithMany("Users")
                        .HasForeignKey("pub_id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("BusinessObject.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("role_id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Publisher");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("BusinessObject.Models.Author", b =>
                {
                    b.Navigation("BookAuthors");
                });

            modelBuilder.Entity("BusinessObject.Models.Book", b =>
                {
                    b.Navigation("BookAuthors");
                });

            modelBuilder.Entity("BusinessObject.Models.Publisher", b =>
                {
                    b.Navigation("Books");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("BusinessObject.Models.Role", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
