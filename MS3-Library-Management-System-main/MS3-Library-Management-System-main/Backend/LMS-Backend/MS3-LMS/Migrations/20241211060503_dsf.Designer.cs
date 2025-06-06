﻿// <auto-generated />
using System;
using MS3_LMS.LMSDbcontext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MS3_LMS.Migrations
{
    [DbContext(typeof(LMSContext))]
    [Migration("20241211060503_dsf")]
    partial class dsf
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MS3_LMS.Enity.Book.Author", b =>
                {
                    b.Property<Guid>("AuthorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Bio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AuthorId");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("MS3_LMS.Enity.Book.Book", b =>
                {
                    b.Property<Guid>("Bookid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("BookType")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("GenreId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<Guid>("LanguageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PageCount")
                        .HasColumnType("int");

                    b.Property<Guid>("PublisherId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Bookid");

                    b.HasIndex("AuthorId");

                    b.HasIndex("GenreId");

                    b.HasIndex("LanguageId");

                    b.HasIndex("PublisherId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("MS3_LMS.Enity.Book.Genre", b =>
                {
                    b.Property<Guid>("GenreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BookGenre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GenreId");

                    b.ToTable("Genres");

                    b.HasData(
                        new
                        {
                            GenreId = new Guid("41bd7cb6-c51f-4c2a-bf62-742ef4cb87ed"),
                            BookGenre = "Fiction",
                            Description = "Fictional stories"
                        },
                        new
                        {
                            GenreId = new Guid("40a09432-0380-4ccf-9a52-cf193350f50c"),
                            BookGenre = "Non Fiction",
                            Description = "Non-fictional content"
                        },
                        new
                        {
                            GenreId = new Guid("b00e5605-e4e0-4b78-9012-20ef3660cb4c"),
                            BookGenre = "Science Fiction",
                            Description = "Sci-fi stories"
                        },
                        new
                        {
                            GenreId = new Guid("e5149ca3-136a-4ac3-9743-cbac7185111f"),
                            BookGenre = "Mystery",
                            Description = "Mystery novels"
                        },
                        new
                        {
                            GenreId = new Guid("6f403ff5-4aa5-4a9f-8297-b07aaa331401"),
                            BookGenre = "Thriller",
                            Description = "Thrilling stories"
                        },
                        new
                        {
                            GenreId = new Guid("05a8049d-2754-4cc2-9633-c650ece1e3fa"),
                            BookGenre = "Romance Novel",
                            Description = "Romantic tales"
                        },
                        new
                        {
                            GenreId = new Guid("86d0a4e9-c794-47dc-8acd-57b0d2499d42"),
                            BookGenre = "Biography",
                            Description = "Life stories of individuals"
                        },
                        new
                        {
                            GenreId = new Guid("8baf8281-ccdd-4811-9f09-06e2408ff164"),
                            BookGenre = "Humor",
                            Description = "Funny and comedic stories"
                        },
                        new
                        {
                            GenreId = new Guid("4f114fe6-770e-48c9-afa5-030ce826b7f1"),
                            BookGenre = "Fairy Tale",
                            Description = "Fairy tales and folklore"
                        },
                        new
                        {
                            GenreId = new Guid("bd0f8b82-664a-4825-b440-495f47c6a694"),
                            BookGenre = "Graphic Novel",
                            Description = "Stories told through illustrations"
                        },
                        new
                        {
                            GenreId = new Guid("d2873638-cfc9-4e51-a6d3-772e4e1f2d25"),
                            BookGenre = "True Crime",
                            Description = "Real-life crime stories"
                        },
                        new
                        {
                            GenreId = new Guid("60b0d928-cd5e-47b0-b0e0-3c96abfcd2fc"),
                            BookGenre = "Magical Realism",
                            Description = "Stories blending magic and realism"
                        });
                });

            modelBuilder.Entity("MS3_LMS.Enity.Book.Image", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Bookid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Image1Path")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image2Path")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("Bookid")
                        .IsUnique();

                    b.ToTable("Images");
                });

            modelBuilder.Entity("MS3_LMS.Enity.Book.Language", b =>
                {
                    b.Property<Guid>("LanguageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("TypeOfLanguage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LanguageId");

                    b.ToTable("Languages");

                    b.HasData(
                        new
                        {
                            LanguageId = new Guid("714da737-2b5f-4da4-a1b7-da69585040ee"),
                            TypeOfLanguage = "English"
                        },
                        new
                        {
                            LanguageId = new Guid("f6fb870d-9527-462c-89a8-d9f8a62b2057"),
                            TypeOfLanguage = "Tamil"
                        },
                        new
                        {
                            LanguageId = new Guid("c0aa0b76-6144-4c71-8402-fb8ab6dd4219"),
                            TypeOfLanguage = "Singala"
                        });
                });

            modelBuilder.Entity("MS3_LMS.Enity.Book.Publisher", b =>
                {
                    b.Property<Guid>("PublisherId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Details")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PublishDate")
                        .HasColumnType("datetime2");

                    b.HasKey("PublisherId");

                    b.ToTable("Publishers");
                });

            modelBuilder.Entity("MS3_LMS.Enity.Book.Rating", b =>
                {
                    b.Property<Guid>("RatingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Bookid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FeedBack")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("MemebID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("StarCount")
                        .HasColumnType("int");

                    b.HasKey("RatingId");

                    b.HasIndex("Bookid");

                    b.HasIndex("MemebID");

                    b.ToTable("Rating");
                });

            modelBuilder.Entity("MS3_LMS.Enity.Core.BookLend", b =>
                {
                    b.Property<Guid>("LendId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ApprovedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("Bookid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CollectDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("LendDays")
                        .HasColumnType("int");

                    b.Property<Guid>("MemebID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ReturnDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("LendId");

                    b.HasIndex("Bookid");

                    b.HasIndex("MemebID");

                    b.ToTable("BookLends");
                });

            modelBuilder.Entity("MS3_LMS.Enity.Core.Payment", b =>
                {
                    b.Property<Guid>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("SubId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PaymentId");

                    b.HasIndex("SubId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("MS3_LMS.Enity.Core.Restriction", b =>
                {
                    b.Property<Guid>("ResId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool?>("IsRestricted")
                        .HasColumnType("bit");

                    b.Property<Guid>("MemebID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Reason")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ResId");

                    b.HasIndex("MemebID")
                        .IsUnique();

                    b.ToTable("Restrictions");
                });

            modelBuilder.Entity("MS3_LMS.Enity.Core.Subscription", b =>
                {
                    b.Property<Guid>("SubId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsCancel")
                        .HasColumnType("bit");

                    b.Property<Guid>("MemebID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("SubType")
                        .HasColumnType("int");

                    b.HasKey("SubId");

                    b.HasIndex("MemebID");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("MS3_LMS.Enity.Notification.Notification", b =>
                {
                    b.Property<Guid>("NotificationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Body")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ReceiveId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("ViewStatus")
                        .HasColumnType("bit");

                    b.HasKey("NotificationId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("MS3_LMS.Enity.Notification.OTP", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ExpiryTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("OTPCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("OTPs");
                });

            modelBuilder.Entity("MS3_LMS.Enity.User.Member", b =>
                {
                    b.Property<Guid>("MemebID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsVerify")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nic")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserGender")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("MemebID");

                    b.HasIndex("Nic")
                        .IsUnique();

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Members");
                });

            modelBuilder.Entity("MS3_LMS.Enity.User.Role", b =>
                {
                    b.Property<Guid>("RoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserAType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleID");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            RoleID = new Guid("1751a225-8149-4aa7-9d53-971bd4e73b2c"),
                            UserAType = "Member"
                        },
                        new
                        {
                            RoleID = new Guid("49186f2c-d1dc-4e0d-be79-8d6589340ec6"),
                            UserAType = "Admin"
                        });
                });

            modelBuilder.Entity("MS3_LMS.Enity.User.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsConfirmEmail")
                        .HasColumnType("bit");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MS3_LMS.Enity.User.UserRole", b =>
                {
                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("RoleID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleID");

                    b.HasIndex("RoleID");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("MS3_LMS.Enity.Book.Book", b =>
                {
                    b.HasOne("MS3_LMS.Enity.Book.Author", "Author")
                        .WithMany("Books")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MS3_LMS.Enity.Book.Genre", "Genre")
                        .WithMany("Books")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MS3_LMS.Enity.Book.Language", "Language")
                        .WithMany("Books")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MS3_LMS.Enity.Book.Publisher", "Publisher")
                        .WithMany("Books")
                        .HasForeignKey("PublisherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Genre");

                    b.Navigation("Language");

                    b.Navigation("Publisher");
                });

            modelBuilder.Entity("MS3_LMS.Enity.Book.Image", b =>
                {
                    b.HasOne("MS3_LMS.Enity.Book.Book", "Book")
                        .WithOne("Image")
                        .HasForeignKey("MS3_LMS.Enity.Book.Image", "Bookid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");
                });

            modelBuilder.Entity("MS3_LMS.Enity.Book.Rating", b =>
                {
                    b.HasOne("MS3_LMS.Enity.Book.Book", "Book")
                        .WithMany("Ratings")
                        .HasForeignKey("Bookid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MS3_LMS.Enity.User.Member", "Member")
                        .WithMany("Ratings")
                        .HasForeignKey("MemebID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Member");
                });

            modelBuilder.Entity("MS3_LMS.Enity.Core.BookLend", b =>
                {
                    b.HasOne("MS3_LMS.Enity.Book.Book", "Book")
                        .WithMany()
                        .HasForeignKey("Bookid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MS3_LMS.Enity.User.Member", "Member")
                        .WithMany("BookLends")
                        .HasForeignKey("MemebID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Member");
                });

            modelBuilder.Entity("MS3_LMS.Enity.Core.Payment", b =>
                {
                    b.HasOne("MS3_LMS.Enity.Core.Subscription", "Subscription")
                        .WithMany("Payment")
                        .HasForeignKey("SubId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Subscription");
                });

            modelBuilder.Entity("MS3_LMS.Enity.Core.Restriction", b =>
                {
                    b.HasOne("MS3_LMS.Enity.User.Member", "Member")
                        .WithOne("Restriction")
                        .HasForeignKey("MS3_LMS.Enity.Core.Restriction", "MemebID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Member");
                });

            modelBuilder.Entity("MS3_LMS.Enity.Core.Subscription", b =>
                {
                    b.HasOne("MS3_LMS.Enity.User.Member", "Member")
                        .WithMany("Subscriptions")
                        .HasForeignKey("MemebID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Member");
                });

            modelBuilder.Entity("MS3_LMS.Enity.Notification.OTP", b =>
                {
                    b.HasOne("MS3_LMS.Enity.User.User", "User")
                        .WithMany("OTPs")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("MS3_LMS.Enity.User.Member", b =>
                {
                    b.HasOne("MS3_LMS.Enity.User.User", "User")
                        .WithOne("Member")
                        .HasForeignKey("MS3_LMS.Enity.User.Member", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("MS3_LMS.Enity.User.UserRole", b =>
                {
                    b.HasOne("MS3_LMS.Enity.User.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MS3_LMS.Enity.User.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MS3_LMS.Enity.Book.Author", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("MS3_LMS.Enity.Book.Book", b =>
                {
                    b.Navigation("Image");

                    b.Navigation("Ratings");
                });

            modelBuilder.Entity("MS3_LMS.Enity.Book.Genre", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("MS3_LMS.Enity.Book.Language", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("MS3_LMS.Enity.Book.Publisher", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("MS3_LMS.Enity.Core.Subscription", b =>
                {
                    b.Navigation("Payment");
                });

            modelBuilder.Entity("MS3_LMS.Enity.User.Member", b =>
                {
                    b.Navigation("BookLends");

                    b.Navigation("Ratings");

                    b.Navigation("Restriction");

                    b.Navigation("Subscriptions");
                });

            modelBuilder.Entity("MS3_LMS.Enity.User.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("MS3_LMS.Enity.User.User", b =>
                {
                    b.Navigation("Member");

                    b.Navigation("OTPs");

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
