﻿// <auto-generated />
using System;
using MagenicMetrics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace demoWebApp.Migrations
{
    [DbContext(typeof(MetricService))]
    [Migration("20181105201317_EnlaargeRequest")]
    partial class EnlaargeRequest
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MagenicMetrics.Metric", b =>
                {
                    b.Property<int>("MetricId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Application")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<string>("Details");

                    b.Property<int>("ElapsedTime");

                    b.Property<string>("ExceptionMessage");

                    b.Property<string>("Query")
                        .IsRequired();

                    b.Property<string>("RequestMethod")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<string>("RequestPath")
                        .IsRequired();

                    b.Property<int>("ResultCode");

                    b.Property<int>("ResultCount");

                    b.Property<string>("ServerName")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<DateTime>("StartTime");

                    b.Property<string>("TraceId")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.HasKey("MetricId");

                    b.ToTable("Metrics");
                });
#pragma warning restore 612, 618
        }
    }
}
