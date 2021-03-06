﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using School.Infrastructure.Data;
using System;

namespace School.Infrastructure.Data.Migrations
{
    [DbContext(typeof(SchoolContext))]
    [Migration("20180308170440_schoolDB2")]
    partial class schoolDB2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("School.Domain.Core.Grade", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Number");

                    b.Property<string>("Symbol")
                        .HasColumnType("varchar(1)");

                    b.HasKey("ID");

                    b.ToTable("Class");
                });

            modelBuilder.Entity("School.Domain.Core.LessonTime", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Time");

                    b.HasKey("ID");

                    b.ToTable("LessonTime");
                });

            modelBuilder.Entity("School.Domain.Core.Schedule", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<int>("GradeID");

                    b.Property<int>("TeacherID");

                    b.Property<int>("TimeID");

                    b.HasKey("ID");

                    b.HasIndex("GradeID");

                    b.HasIndex("TeacherID");

                    b.HasIndex("TimeID");

                    b.ToTable("Schedule");
                });

            modelBuilder.Entity("School.Domain.Core.Student", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<int>("GradeID");

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.HasKey("ID");

                    b.HasIndex("GradeID");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("School.Domain.Core.Subject", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int?>("TeacherID");

                    b.HasKey("ID");

                    b.HasIndex("TeacherID");

                    b.ToTable("Subject");
                });

            modelBuilder.Entity("School.Domain.Core.Teacher", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("MiddleName");

                    b.HasKey("ID");

                    b.ToTable("Teacher");
                });

            modelBuilder.Entity("School.Domain.Core.Schedule", b =>
                {
                    b.HasOne("School.Domain.Core.Grade", "Grade")
                        .WithMany()
                        .HasForeignKey("GradeID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("School.Domain.Core.Teacher", "Teacher")
                        .WithMany()
                        .HasForeignKey("TeacherID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("School.Domain.Core.LessonTime", "Time")
                        .WithMany()
                        .HasForeignKey("TimeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("School.Domain.Core.Student", b =>
                {
                    b.HasOne("School.Domain.Core.Grade", "Grade")
                        .WithMany()
                        .HasForeignKey("GradeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("School.Domain.Core.Subject", b =>
                {
                    b.HasOne("School.Domain.Core.Teacher", "Teacher")
                        .WithMany("Subjects")
                        .HasForeignKey("TeacherID");
                });
#pragma warning restore 612, 618
        }
    }
}
