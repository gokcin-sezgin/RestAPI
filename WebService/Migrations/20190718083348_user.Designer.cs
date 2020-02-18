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
    [Migration("20190718083348_user")]
    partial class user
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0-preview6.19304.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebService.Models.UserInfo", b =>
                {
                    b.Property<int>("UserInfoId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PersonelAd");

                    b.Property<string>("PersonelSoyad");

                    b.Property<int>("PersonelTcNo");

                    b.Property<string>("UserName");

                    b.Property<string>("UserPassword");

                    b.HasKey("UserInfoId");

                    b.ToTable("UserInfos");
                });
#pragma warning restore 612, 618
        }
    }
}