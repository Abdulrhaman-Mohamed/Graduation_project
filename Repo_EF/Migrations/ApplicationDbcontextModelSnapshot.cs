﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repo_EF;

#nullable disable

namespace RepoEF.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("FlightControlCenter.Model1.Command", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("SubSystemId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PlanId")
                        .HasColumnType("int");

                    b.Property<int>("PlanSequenceNumber")
                        .HasColumnType("int");

                    b.Property<string>("SensorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id", "SubSystemId");

                    b.HasIndex("SubSystemId");

                    b.HasIndex("PlanId", "PlanSequenceNumber");

                    b.ToTable("Commands", (string)null);
                });

            modelBuilder.Entity("FlightControlCenter.Model1.Satellite", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("Date")
                        .HasColumnType("date");

                    b.Property<decimal?>("Mass")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OrbitType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SatelliteType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Satellites", (string)null);
                });

            modelBuilder.Entity("FlightControlCenter.Model1.SubSystem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("SatelliteId")
                        .HasColumnType("int");

                    b.Property<string>("SubSystemName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SubSystemType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SatelliteId");

                    b.ToTable("Subsystems", (string)null);
                });

            modelBuilder.Entity("Repo_Core.Models.Acknowledge", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AckDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AckNum")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Acknowledges", (string)null);
                });

            modelBuilder.Entity("Repo_Core.Models.CommandParam", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("CommandId")
                        .HasColumnType("int");

                    b.Property<int>("SubSystemId")
                        .HasColumnType("int");

                    b.Property<int>("ParamTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id", "CommandId", "SubSystemId");

                    b.HasIndex("ParamTypeId");

                    b.HasIndex("CommandId", "SubSystemId");

                    b.ToTable("CommandParams", (string)null);
                });

            modelBuilder.Entity("Repo_Core.Models.ParamType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ParamTypes", (string)null);
                });

            modelBuilder.Entity("Repo_Core.Models.ParamValue", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("SubSystemID")
                        .HasColumnType("int");

                    b.Property<int>("CommandID")
                        .HasColumnType("int");

                    b.Property<int>("CommandParamID")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Device")
                        .HasColumnType("int");

                    b.Property<int>("MyProperty")
                        .HasColumnType("int");

                    b.HasKey("Id", "SubSystemID", "CommandID", "CommandParamID");

                    b.HasIndex("CommandParamID", "CommandID", "SubSystemID");

                    b.ToTable("ParamValues", (string)null);
                });

            modelBuilder.Entity("Repo_Core.Models.Plan", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("SequenceNumber")
                        .HasColumnType("int");

                    b.Property<string>("AckId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AcknowledgeId")
                        .HasColumnType("int");

                    b.Property<string>("Delay")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Repeat")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id", "SequenceNumber");

                    b.HasIndex("AcknowledgeId");

                    b.ToTable("Plans", (string)null);
                });

            modelBuilder.Entity("Repo_Core.Models.PlanResult", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("PlanId")
                        .HasColumnType("int");

                    b.Property<int>("PlanSequenceNumber")
                        .HasColumnType("int");

                    b.Property<string>("Result")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Time")
                        .HasColumnType("int");

                    b.HasKey("Id", "PlanId", "PlanSequenceNumber");

                    b.HasIndex("PlanId", "PlanSequenceNumber");

                    b.ToTable("PlanResults", (string)null);
                });

            modelBuilder.Entity("Repo_Core.Models.Station", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("Latitude")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Longitude")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("StationName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StationType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Stations", (string)null);
                });

            modelBuilder.Entity("SatelliteStation", b =>
                {
                    b.Property<int>("SatellitesId")
                        .HasColumnType("int");

                    b.Property<int>("StationsId")
                        .HasColumnType("int");

                    b.HasKey("SatellitesId", "StationsId");

                    b.HasIndex("StationsId");

                    b.ToTable("SatelliteStation", (string)null);
                });

            modelBuilder.Entity("FlightControlCenter.Model1.Command", b =>
                {
                    b.HasOne("FlightControlCenter.Model1.SubSystem", "SubSystem")
                        .WithMany("Commands")
                        .HasForeignKey("SubSystemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Repo_Core.Models.Plan", "Plan")
                        .WithMany("Commands")
                        .HasForeignKey("PlanId", "PlanSequenceNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Plan");

                    b.Navigation("SubSystem");
                });

            modelBuilder.Entity("FlightControlCenter.Model1.SubSystem", b =>
                {
                    b.HasOne("FlightControlCenter.Model1.Satellite", "Satellite")
                        .WithMany("Subsystems")
                        .HasForeignKey("SatelliteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Satellite");
                });

            modelBuilder.Entity("Repo_Core.Models.CommandParam", b =>
                {
                    b.HasOne("Repo_Core.Models.ParamType", "ParamType")
                        .WithMany()
                        .HasForeignKey("ParamTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlightControlCenter.Model1.Command", "Command")
                        .WithMany("CommandParams")
                        .HasForeignKey("CommandId", "SubSystemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Command");

                    b.Navigation("ParamType");
                });

            modelBuilder.Entity("Repo_Core.Models.ParamValue", b =>
                {
                    b.HasOne("Repo_Core.Models.CommandParam", "CommandParam")
                        .WithMany("ParamValues")
                        .HasForeignKey("CommandParamID", "CommandID", "SubSystemID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CommandParam");
                });

            modelBuilder.Entity("Repo_Core.Models.Plan", b =>
                {
                    b.HasOne("Repo_Core.Models.Acknowledge", "Acknowledge")
                        .WithMany()
                        .HasForeignKey("AcknowledgeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Acknowledge");
                });

            modelBuilder.Entity("Repo_Core.Models.PlanResult", b =>
                {
                    b.HasOne("Repo_Core.Models.Plan", "Plan")
                        .WithMany()
                        .HasForeignKey("PlanId", "PlanSequenceNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Plan");
                });

            modelBuilder.Entity("SatelliteStation", b =>
                {
                    b.HasOne("FlightControlCenter.Model1.Satellite", null)
                        .WithMany()
                        .HasForeignKey("SatellitesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Repo_Core.Models.Station", null)
                        .WithMany()
                        .HasForeignKey("StationsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FlightControlCenter.Model1.Command", b =>
                {
                    b.Navigation("CommandParams");
                });

            modelBuilder.Entity("FlightControlCenter.Model1.Satellite", b =>
                {
                    b.Navigation("Subsystems");
                });

            modelBuilder.Entity("FlightControlCenter.Model1.SubSystem", b =>
                {
                    b.Navigation("Commands");
                });

            modelBuilder.Entity("Repo_Core.Models.CommandParam", b =>
                {
                    b.Navigation("ParamValues");
                });

            modelBuilder.Entity("Repo_Core.Models.Plan", b =>
                {
                    b.Navigation("Commands");
                });
#pragma warning restore 612, 618
        }
    }
}
