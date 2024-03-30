﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tranee_Web_App;

#nullable disable

namespace Tranee_Web_App.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20240328154118_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.2");

            modelBuilder.Entity("Tranee_Web_App.Models.ToDoTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Selected")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TaskDescription")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("ToDoTasks");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Selected = false,
                            TaskDescription = "Test",
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            Selected = false,
                            TaskDescription = "Test2",
                            UserId = 2
                        });
                });

            modelBuilder.Entity("Tranee_Web_App.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 2,
                            Name = "Sergey",
                            Password = "123"
                        },
                        new
                        {
                            Id = 1,
                            Name = "Admin",
                            Password = "123"
                        });
                });

            modelBuilder.Entity("Tranee_Web_App.Models.ToDoTask", b =>
                {
                    b.HasOne("Tranee_Web_App.Models.User", "User")
                        .WithMany("ToDoTasks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Tranee_Web_App.Models.User", b =>
                {
                    b.Navigation("ToDoTasks");
                });
#pragma warning restore 612, 618
        }
    }
}