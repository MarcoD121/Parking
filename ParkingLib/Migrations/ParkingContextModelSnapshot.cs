﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ParkingLib.Models;

#nullable disable

namespace ParkingLib.Migrations
{
    [DbContext(typeof(ParkingContext))]
    partial class ParkingContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ParkingLib.Models.EndedParkedVehicle", b =>
                {
                    b.Property<int>("VehicleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Vehicle_Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VehicleId"));

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("EndedParkedId")
                        .HasColumnType("int")
                        .HasColumnName("EndedParked_Id");

                    b.Property<string>("LicensePlate")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Make")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("NumberOfWheels")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VehicleId")
                        .HasName("PK__EndedPar__CE6D7C95C597C5C4");

                    b.HasIndex("EndedParkedId");

                    b.ToTable("EndedParkedVehicles");
                });

            modelBuilder.Entity("ParkingLib.Models.ParkedVehicle", b =>
                {
                    b.Property<int>("VehicleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Vehicle_Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VehicleId"));

                    b.Property<int>("ActiveParkedId")
                        .HasColumnType("int")
                        .HasColumnName("ActiveParked_Id");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LicensePlate")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasAnnotation("Relational:JsonPropertyName", "registration_number");

                    b.Property<string>("Make")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("NumberOfWheels")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VehicleId")
                        .HasName("PK__ParkedVe__CE6D7C9556A6FD58");

                    b.HasIndex("ActiveParkedId");

                    b.ToTable("ParkedVehicles");
                });

            modelBuilder.Entity("ParkingLib.Models.TimeForEndedVehicle", b =>
                {
                    b.Property<int>("EndedParkedId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("EndedParked_Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EndedParkedId"));

                    b.Property<DateTime>("TimeEnded")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("TimeStarted")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("TotalTime")
                        .HasColumnType("datetime");

                    b.HasKey("EndedParkedId")
                        .HasName("PK__TimeForE__D2B9BC5ED11C9883");

                    b.ToTable("TimeForEndedVehicles");
                });

            modelBuilder.Entity("ParkingLib.Models.TimeForParkedVehicle", b =>
                {
                    b.Property<int>("ActiveParkedId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ActiveParked_Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ActiveParkedId"));

                    b.Property<DateTime>("TimeStarted")
                        .HasColumnType("datetime");

                    b.HasKey("ActiveParkedId")
                        .HasName("PK__TimeForP__9774E10531C9BE82");

                    b.ToTable("TimeForParkedVehicles");
                });

            modelBuilder.Entity("ParkingLib.Models.EndedParkedVehicle", b =>
                {
                    b.HasOne("ParkingLib.Models.TimeForEndedVehicle", "EndedParked")
                        .WithMany("EndedParkedVehicles")
                        .HasForeignKey("EndedParkedId")
                        .IsRequired()
                        .HasConstraintName("FK__EndedPark__Ended__2FCF1A8A");

                    b.Navigation("EndedParked");
                });

            modelBuilder.Entity("ParkingLib.Models.ParkedVehicle", b =>
                {
                    b.HasOne("ParkingLib.Models.TimeForParkedVehicle", "ActiveParked")
                        .WithMany("ParkedVehicles")
                        .HasForeignKey("ActiveParkedId")
                        .IsRequired()
                        .HasConstraintName("FK__ParkedVeh__Activ__2CF2ADDF");

                    b.Navigation("ActiveParked");
                });

            modelBuilder.Entity("ParkingLib.Models.TimeForEndedVehicle", b =>
                {
                    b.Navigation("EndedParkedVehicles");
                });

            modelBuilder.Entity("ParkingLib.Models.TimeForParkedVehicle", b =>
                {
                    b.Navigation("ParkedVehicles");
                });
#pragma warning restore 612, 618
        }
    }
}
