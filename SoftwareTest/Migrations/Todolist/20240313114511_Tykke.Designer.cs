﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SoftwareTest.Models;

#nullable disable

namespace SoftwareTest.Migrations.Todolist
{
    [DbContext(typeof(TodolistContext))]
    [Migration("20240313114511_Tykke")]
    partial class Tykke
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SoftwareTest.Models.Cpr", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Cprnr")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("CPRnr");

                    b.Property<string>("User")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id")
                        .HasName("PK__CPR__3213E83F7E677FE0");

                    b.ToTable("CPR", (string)null);
                });

            modelBuilder.Entity("SoftwareTest.Models.TodolostTb", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Items")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("Userid")
                        .HasColumnType("int")
                        .HasColumnName("userid");

                    b.HasKey("Id")
                        .HasName("PK_TodolostTb_Id");

                    b.HasIndex("Userid");

                    b.ToTable("TodolostTB", (string)null);
                });

            modelBuilder.Entity("SoftwareTest.Models.TodolostTb", b =>
                {
                    b.HasOne("SoftwareTest.Models.Cpr", "User")
                        .WithMany()
                        .HasForeignKey("Userid")
                        .HasConstraintName("FK_CPR_Todo");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
