﻿// <auto-generated />
using System;
using Hackademy.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Hackademy.Infrastructure.Migrations
{
    [DbContext(typeof(HackademyContext))]
    [Migration("20230603123931_addCarrers")]
    partial class addCarrers
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Hackademy.Domain.Entity.Career", b =>
                {
                    b.Property<int>("CareerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CareerId"), 1L, 1);

                    b.Property<int>("CareerDescription")
                        .HasColumnType("int");

                    b.Property<int>("CareerStepNumber")
                        .HasColumnType("int");

                    b.Property<int>("CareerTitle")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDone")
                        .HasColumnType("bit");

                    b.Property<int>("TechLabId")
                        .HasColumnType("int");

                    b.HasKey("CareerId");

                    b.HasIndex("TechLabId");

                    b.ToTable("Careers");
                });

            modelBuilder.Entity("Hackademy.Domain.Entity.Event", b =>
                {
                    b.Property<int>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EventId"), 1L, 1);

                    b.Property<DateTime>("EndDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("EventCity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EventLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EventStreet")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EventTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EventTypeEnum")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("StartDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("TeamId")
                        .HasColumnType("int");

                    b.HasKey("EventId");

                    b.HasIndex("TeamId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("Hackademy.Domain.Entity.Team", b =>
                {
                    b.Property<int>("TeamId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TeamId"), 1L, 1);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TechLabId")
                        .HasColumnType("int");

                    b.HasKey("TeamId");

                    b.HasIndex("TechLabId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("Hackademy.Domain.Entity.TechLab", b =>
                {
                    b.Property<int>("TechLabId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TechLabId"), 1L, 1);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TechLabId");

                    b.ToTable("TechLabs");
                });

            modelBuilder.Entity("Hackademy.Domain.Entity.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<string>("Bio")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Birthdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("Image")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Point")
                        .HasColumnType("float");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TeamUser", b =>
                {
                    b.Property<int>("TeamsTeamId")
                        .HasColumnType("int");

                    b.Property<int>("UsersUserId")
                        .HasColumnType("int");

                    b.HasKey("TeamsTeamId", "UsersUserId");

                    b.HasIndex("UsersUserId");

                    b.ToTable("TeamUser");
                });

            modelBuilder.Entity("Hackademy.Domain.Entity.Career", b =>
                {
                    b.HasOne("Hackademy.Domain.Entity.TechLab", "TechLab")
                        .WithMany("Careers")
                        .HasForeignKey("TechLabId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TechLab");
                });

            modelBuilder.Entity("Hackademy.Domain.Entity.Event", b =>
                {
                    b.HasOne("Hackademy.Domain.Entity.Team", "Team")
                        .WithMany("Events")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Team");
                });

            modelBuilder.Entity("Hackademy.Domain.Entity.Team", b =>
                {
                    b.HasOne("Hackademy.Domain.Entity.TechLab", "TechLab")
                        .WithMany("Teams")
                        .HasForeignKey("TechLabId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TechLab");
                });

            modelBuilder.Entity("TeamUser", b =>
                {
                    b.HasOne("Hackademy.Domain.Entity.Team", null)
                        .WithMany()
                        .HasForeignKey("TeamsTeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Hackademy.Domain.Entity.User", null)
                        .WithMany()
                        .HasForeignKey("UsersUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Hackademy.Domain.Entity.Team", b =>
                {
                    b.Navigation("Events");
                });

            modelBuilder.Entity("Hackademy.Domain.Entity.TechLab", b =>
                {
                    b.Navigation("Careers");

                    b.Navigation("Teams");
                });
#pragma warning restore 612, 618
        }
    }
}
