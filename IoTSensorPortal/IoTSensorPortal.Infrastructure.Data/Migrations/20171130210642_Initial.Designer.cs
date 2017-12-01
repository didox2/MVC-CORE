﻿// <auto-generated />
using IoTSensorPortal.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace IoTSensorPortal.Infrastructure.Data.Migrations
{
    [DbContext(typeof(IoTSensorPortalContext))]
    [Migration("20171130210642_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("IoTSensorPortal.Infrastructure.Data.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp");

                    b.Property<string>("Email");

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail");

                    b.Property<string>("NormalizedUserName");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("ApplicationUser");
                });

            modelBuilder.Entity("IoTSensorPortal.Infrastructure.Data.Models.History", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("SensorId");

                    b.Property<DateTime>("UpdateDate");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("SensorId");

                    b.ToTable("History");
                });

            modelBuilder.Entity("IoTSensorPortal.Infrastructure.Data.Models.Sensor", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CurrentValue");

                    b.Property<bool>("IsPublic");

                    b.Property<DateTime>("LastUpdated");

                    b.Property<int>("MaxValue");

                    b.Property<int>("MinValue");

                    b.Property<string>("Name");

                    b.Property<string>("OwnerId");

                    b.Property<int>("RefreshRate");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Sensors");
                });

            modelBuilder.Entity("IoTSensorPortal.Infrastructure.Data.Models.UserSensor", b =>
                {
                    b.Property<string>("ApplicationUserId");

                    b.Property<long>("SensorId");

                    b.HasKey("ApplicationUserId", "SensorId");

                    b.HasIndex("SensorId");

                    b.ToTable("UserSensor");
                });

            modelBuilder.Entity("IoTSensorPortal.Infrastructure.Data.Models.History", b =>
                {
                    b.HasOne("IoTSensorPortal.Infrastructure.Data.Models.Sensor", "Sensor")
                        .WithMany("History")
                        .HasForeignKey("SensorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("IoTSensorPortal.Infrastructure.Data.Models.Sensor", b =>
                {
                    b.HasOne("IoTSensorPortal.Infrastructure.Data.Models.ApplicationUser", "Owner")
                        .WithMany("MySensors")
                        .HasForeignKey("OwnerId");
                });

            modelBuilder.Entity("IoTSensorPortal.Infrastructure.Data.Models.UserSensor", b =>
                {
                    b.HasOne("IoTSensorPortal.Infrastructure.Data.Models.ApplicationUser", "ApplicationUser")
                        .WithMany("SharedSensors")
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("IoTSensorPortal.Infrastructure.Data.Models.Sensor", "Sensor")
                        .WithMany("SharedWithUsers")
                        .HasForeignKey("SensorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}