﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repo_EF;

#nullable disable

namespace Repo_EF.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230329005053_PlanUserRelation")]
    partial class PlanUserRelation
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Repo_Core.Identity_Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Repo_Core.Identity_Models.Feedback", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("comment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("feedbacktime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Feedbacks");
                });

            modelBuilder.Entity("Repo_Core.Identity_Models.Posts", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("postContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("postDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("postImages")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("postTitle")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("UserId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Repo_Core.Models.Acknowledge", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AckDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AckNum")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Acknowledges");
                });

            modelBuilder.Entity("Repo_Core.Models.Command", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("SubSystemId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SensorName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id", "SubSystemId");

                    b.HasIndex("SubSystemId");

                    b.ToTable("Commands");
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

                    b.ToTable("CommandParams");
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

                    b.ToTable("ParamTypes");
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
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Device")
                        .HasColumnType("int");

                    b.HasKey("Id", "SubSystemID", "CommandID", "CommandParamID");

                    b.HasIndex("CommandParamID", "CommandID", "SubSystemID");

                    b.ToTable("ParamValues");
                });

            modelBuilder.Entity("Repo_Core.Models.Plan", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("SequenceNumber")
                        .HasColumnType("int");

                    b.Property<int?>("AcknowledgeId")
                        .HasColumnType("int");

                    b.Property<string>("ApplicationUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("CommandId")
                        .HasColumnType("int");

                    b.Property<string>("Delay")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Divces")
                        .HasColumnType("int");

                    b.Property<bool?>("FlagWatting")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Repeat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SubSystemId")
                        .HasColumnType("int");

                    b.Property<DateTime>("dateTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("inputParamter")
                        .HasColumnType("int");

                    b.HasKey("Id", "SequenceNumber");

                    b.HasIndex("AcknowledgeId");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("FlagWatting")
                        .HasFilter("[FlagWatting] IS False");

                    b.HasIndex("CommandId", "SubSystemId");

                    b.ToTable("Plans");
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
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.HasKey("Id", "PlanId", "PlanSequenceNumber");

                    b.HasIndex("PlanId", "PlanSequenceNumber");

                    b.ToTable("PlanResults");
                });

            modelBuilder.Entity("Repo_Core.Models.Satellite", b =>
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

                    b.ToTable("Satellites");
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

                    b.ToTable("Stations");
                });

            modelBuilder.Entity("Repo_Core.Models.SubSystem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("SatelliteId")
                        .HasColumnType("int");

                    b.Property<string>("SubSystemName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubSystemType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SatelliteId");

                    b.ToTable("Subsystems");
                });

            modelBuilder.Entity("SatelliteStation", b =>
                {
                    b.Property<int>("SatellitesId")
                        .HasColumnType("int");

                    b.Property<int>("StationsId")
                        .HasColumnType("int");

                    b.HasKey("SatellitesId", "StationsId");

                    b.HasIndex("StationsId");

                    b.ToTable("SatelliteStation");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Repo_Core.Identity_Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Repo_Core.Identity_Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Repo_Core.Identity_Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Repo_Core.Identity_Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Repo_Core.Identity_Models.Feedback", b =>
                {
                    b.HasOne("Repo_Core.Identity_Models.Posts", "Post")
                        .WithMany("feedback")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Repo_Core.Identity_Models.ApplicationUser", "User")
                        .WithMany("Feedbacks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Repo_Core.Identity_Models.Posts", b =>
                {
                    b.HasOne("Repo_Core.Identity_Models.ApplicationUser", "User")
                        .WithMany("Posts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Repo_Core.Models.Command", b =>
                {
                    b.HasOne("Repo_Core.Models.SubSystem", "SubSystem")
                        .WithMany("Commands")
                        .HasForeignKey("SubSystemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SubSystem");
                });

            modelBuilder.Entity("Repo_Core.Models.CommandParam", b =>
                {
                    b.HasOne("Repo_Core.Models.ParamType", "ParamType")
                        .WithMany()
                        .HasForeignKey("ParamTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Repo_Core.Models.Command", "Command")
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
                        .HasForeignKey("AcknowledgeId");

                    b.HasOne("Repo_Core.Identity_Models.ApplicationUser", "ApplicationUser")
                        .WithMany("Plans")
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Repo_Core.Models.Command", "Command")
                        .WithMany("Plans")
                        .HasForeignKey("CommandId", "SubSystemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Acknowledge");

                    b.Navigation("ApplicationUser");

                    b.Navigation("Command");
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

            modelBuilder.Entity("Repo_Core.Models.SubSystem", b =>
                {
                    b.HasOne("Repo_Core.Models.Satellite", "Satellite")
                        .WithMany("Subsystems")
                        .HasForeignKey("SatelliteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Satellite");
                });

            modelBuilder.Entity("SatelliteStation", b =>
                {
                    b.HasOne("Repo_Core.Models.Satellite", null)
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

            modelBuilder.Entity("Repo_Core.Identity_Models.ApplicationUser", b =>
                {
                    b.Navigation("Feedbacks");

                    b.Navigation("Plans");

                    b.Navigation("Posts");
                });

            modelBuilder.Entity("Repo_Core.Identity_Models.Posts", b =>
                {
                    b.Navigation("feedback");
                });

            modelBuilder.Entity("Repo_Core.Models.Command", b =>
                {
                    b.Navigation("CommandParams");

                    b.Navigation("Plans");
                });

            modelBuilder.Entity("Repo_Core.Models.CommandParam", b =>
                {
                    b.Navigation("ParamValues");
                });

            modelBuilder.Entity("Repo_Core.Models.Satellite", b =>
                {
                    b.Navigation("Subsystems");
                });

            modelBuilder.Entity("Repo_Core.Models.SubSystem", b =>
                {
                    b.Navigation("Commands");
                });
#pragma warning restore 612, 618
        }
    }
}
