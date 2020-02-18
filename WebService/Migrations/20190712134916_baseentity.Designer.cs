﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebService.Models;

namespace WebService.Migrations
{
    [DbContext(typeof(UserContext))]
    [Migration("20190712134916_baseentity")]
    partial class baseentity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0-preview6.19304.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PersonelWebApp.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("UserInfoForeignKey");

                    b.Property<string>("UserName")
                        .IsRequired();

                    b.Property<string>("UserPassword")
                        .IsRequired();

                    b.HasKey("UserId");

                    b.HasIndex("UserInfoForeignKey")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PersonelWebApp.Models.UserInfo", b =>
                {
                    b.Property<int>("UserInfoId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PersonelAd")
                        .IsRequired();

                    b.Property<string>("PersonelSoyad")
                        .IsRequired();

                    b.Property<int>("PersonelTcNo");

                    b.HasKey("UserInfoId");

                    b.ToTable("UserInfos");
                });

            modelBuilder.Entity("PersonelWebApp.Models.User", b =>
                {
                    b.HasOne("PersonelWebApp.Models.UserInfo", "UserInfo")
                        .WithOne("User")
                        .HasForeignKey("PersonelWebApp.Models.User", "UserInfoForeignKey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
