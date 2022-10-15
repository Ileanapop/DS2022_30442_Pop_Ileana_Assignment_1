﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using energy_utility_platform_api.Entities.DataPersistence;

#nullable disable

namespace energy_utility_platform_api.Migrations
{
    [DbContext(typeof(UtilityPlatformContext))]
    [Migration("20221015121559_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("energy_utility_platform_api.Entities.EnergyConsumption", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("Consumption")
                        .HasColumnType("real");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserDeviceEnergyDeviceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserDeviceUserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserDeviceUserId", "UserDeviceEnergyDeviceId");

                    b.ToTable("EnergyConsumptions");
                });

            modelBuilder.Entity("energy_utility_platform_api.Entities.EnergyDevice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasDefaultValue("Device description");

                    b.Property<float>("MaxHourlyEnergy")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("real")
                        .HasDefaultValue(0f);

                    b.HasKey("Id");

                    b.ToTable("EnergyDevices");
                });

            modelBuilder.Entity("energy_utility_platform_api.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasDefaultValue("client");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("energy_utility_platform_api.Entities.UserDevice", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EnergyDeviceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("UserId", "EnergyDeviceId");

                    b.HasIndex("EnergyDeviceId");

                    b.ToTable("UserDevices", (string)null);
                });

            modelBuilder.Entity("energy_utility_platform_api.Entities.EnergyConsumption", b =>
                {
                    b.HasOne("energy_utility_platform_api.Entities.UserDevice", "UserDevice")
                        .WithMany("EnergyConsumptions")
                        .HasForeignKey("UserDeviceUserId", "UserDeviceEnergyDeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserDevice");
                });

            modelBuilder.Entity("energy_utility_platform_api.Entities.UserDevice", b =>
                {
                    b.HasOne("energy_utility_platform_api.Entities.EnergyDevice", "EnergyDevice")
                        .WithMany("UserDevices")
                        .HasForeignKey("EnergyDeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("energy_utility_platform_api.Entities.User", "User")
                        .WithMany("UserDevices")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EnergyDevice");

                    b.Navigation("User");
                });

            modelBuilder.Entity("energy_utility_platform_api.Entities.EnergyDevice", b =>
                {
                    b.Navigation("UserDevices");
                });

            modelBuilder.Entity("energy_utility_platform_api.Entities.User", b =>
                {
                    b.Navigation("UserDevices");
                });

            modelBuilder.Entity("energy_utility_platform_api.Entities.UserDevice", b =>
                {
                    b.Navigation("EnergyConsumptions");
                });
#pragma warning restore 612, 618
        }
    }
}