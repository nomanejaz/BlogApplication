﻿// <auto-generated />
using System;
using CorApplication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CorApplication.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20190316174550_AddingTitleImageToUserBlogTable")]
    partial class AddingTitleImageToUserBlogTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Blog.Entity.Models.BlogComment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comment");

                    b.Property<DateTime>("CommentPostDate");

                    b.Property<string>("PostById");

                    b.Property<int>("UserBlogId");

                    b.HasKey("Id");

                    b.HasIndex("PostById");

                    b.HasIndex("UserBlogId");

                    b.ToTable("BlogComments");
                });

            modelBuilder.Entity("Blog.Entity.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RoleName");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Blog.Entity.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("Password");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Blog.Entity.Models.UserBlog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AuthorId");

                    b.Property<string>("Content");

                    b.Property<DateTime>("PublishDate");

                    b.Property<string>("Title");

                    b.Property<string>("TitleImage");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("UserBlogs");
                });

            modelBuilder.Entity("Blog.Entity.Models.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("RoleId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Blog.Entity.Models.BlogComment", b =>
                {
                    b.HasOne("Blog.Entity.Models.User", "PostBy")
                        .WithMany()
                        .HasForeignKey("PostById");

                    b.HasOne("Blog.Entity.Models.UserBlog", "UserBlog")
                        .WithMany("BlogComments")
                        .HasForeignKey("UserBlogId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Blog.Entity.Models.UserBlog", b =>
                {
                    b.HasOne("Blog.Entity.Models.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId");
                });

            modelBuilder.Entity("Blog.Entity.Models.UserRole", b =>
                {
                    b.HasOne("Blog.Entity.Models.Role", "Role")
                        .WithMany("UserRole")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Blog.Entity.Models.User", "User")
                        .WithMany("UserRole")
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
