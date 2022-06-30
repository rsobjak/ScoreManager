﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ScoreManager.Data;

#nullable disable

namespace ScoreManager.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ScoreManagerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.6");

            modelBuilder.Entity("ScoreManager.Entities.Candidate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Cellphone")
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .HasColumnType("TEXT");

                    b.Property<string>("Document")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Document")
                        .IsUnique();

                    b.ToTable("Candidate");
                });

            modelBuilder.Entity("ScoreManager.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<int>("Order")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Category");
                });

            modelBuilder.Entity("ScoreManager.Entities.Perform", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PrimaryCandidateId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal?>("Score")
                        .HasColumnType("TEXT");

                    b.Property<int?>("SecondaryCandidateId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("PrimaryCandidateId");

                    b.HasIndex("SecondaryCandidateId");

                    b.ToTable("Perform");
                });

            modelBuilder.Entity("ScoreManager.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsRater")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Login")
                        .IsUnique();

                    b.ToTable("User");
                });

            modelBuilder.Entity("ScoreManager.Entities.Perform", b =>
                {
                    b.HasOne("ScoreManager.Entities.Category", "Category")
                        .WithMany("Performs")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ScoreManager.Entities.Candidate", "PrimaryCandidate")
                        .WithMany()
                        .HasForeignKey("PrimaryCandidateId");

                    b.HasOne("ScoreManager.Entities.Candidate", "SecondaryCandidate")
                        .WithMany()
                        .HasForeignKey("SecondaryCandidateId");

                    b.Navigation("Category");

                    b.Navigation("PrimaryCandidate");

                    b.Navigation("SecondaryCandidate");
                });

            modelBuilder.Entity("ScoreManager.Entities.Category", b =>
                {
                    b.Navigation("Performs");
                });
#pragma warning restore 612, 618
        }
    }
}
