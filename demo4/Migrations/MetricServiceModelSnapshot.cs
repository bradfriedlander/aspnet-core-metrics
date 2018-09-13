﻿// <auto-generated />
using System;
using MagenicMetrics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace demo4.Migrations
{
    [DbContext(typeof(MetricService))]
    partial class MetricServiceModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
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

                    b.Property<int>("ElpasedTime");

                    b.Property<string>("ExceptionMessage");

                    b.Property<string>("Query")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<string>("RequestPath")
                        .IsRequired()
                        .HasMaxLength(128);

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
