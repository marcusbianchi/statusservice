﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using statusservice.Data;
using System;

namespace statusservice.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20171221124334_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("statusservice.Model.Status", b =>
                {
                    b.Property<int>("statusId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("thingId");

                    b.HasKey("statusId");

                    b.ToTable("Status");
                });

            modelBuilder.Entity("statusservice.Model.StatusDescription", b =>
                {
                    b.Property<int>("statusDescriptionId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("context")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("description")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int?>("statusId");

                    b.Property<string>("statusName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<long>("timestampTicks");

                    b.Property<string>("value")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("statusDescriptionId");

                    b.HasIndex("statusId");

                    b.ToTable("StatusDescriptions");
                });

            modelBuilder.Entity("statusservice.Model.StatusDescription", b =>
                {
                    b.HasOne("statusservice.Model.Status")
                        .WithMany("statusDescrptions")
                        .HasForeignKey("statusId");
                });
#pragma warning restore 612, 618
        }
    }
}
