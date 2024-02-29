﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ScoreSphere.Models;

#nullable disable

namespace ScoreSphere.Migrations
{
    [DbContext(typeof(ScoreSphereDbContext))]
    [Migration("20240229121316_ChangedIDtolowercase")]
    partial class ChangedIDtolowercase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ScoreSphere.Models.Match", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("Team1Goals")
                        .HasColumnType("integer");

                    b.Property<int?>("Team1Id")
                        .HasColumnType("integer");

                    b.Property<string>("Team1Logo")
                        .HasColumnType("text");

                    b.Property<string>("Team1Name")
                        .HasColumnType("text");

                    b.Property<int?>("Team2Goals")
                        .HasColumnType("integer");

                    b.Property<int?>("Team2Id")
                        .HasColumnType("integer");

                    b.Property<string>("Team2Logo")
                        .HasColumnType("text");

                    b.Property<string>("Team2Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("ScoreSphere.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Logo")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Teams");
                });
#pragma warning restore 612, 618
        }
    }
}
