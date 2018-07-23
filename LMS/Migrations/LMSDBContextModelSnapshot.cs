﻿// <auto-generated />
using LMS.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Storage.Internal;
using System;

namespace LMS.Migrations
{
    [DbContext(typeof(LMSDBContext))]
    partial class LMSDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011");

            modelBuilder.Entity("LMS.Model.Assignment", b =>
                {
                    b.Property<string>("AssignmentId")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(10);

                    b.Property<string>("AssignmentName");

                    b.Property<string>("CourseId");

                    b.HasKey("AssignmentId");

                    b.HasIndex("CourseId");

                    b.ToTable("Assignments");
                });

            modelBuilder.Entity("LMS.Model.Course", b =>
                {
                    b.Property<string>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(10);

                    b.Property<string>("Introduction");

                    b.Property<string>("LecturerId");

                    b.Property<string>("Name");

                    b.HasKey("CourseId");

                    b.HasIndex("LecturerId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("LMS.Model.Enrolment", b =>
                {
                    b.Property<string>("CourseId");

                    b.Property<string>("StudentId");

                    b.Property<decimal>("CourseGrade");

                    b.HasKey("CourseId", "StudentId");

                    b.HasIndex("StudentId");

                    b.ToTable("Enrolments");
                });

            modelBuilder.Entity("LMS.Model.Lecturer", b =>
                {
                    b.Property<string>("LecturerId")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(10);

                    b.Property<string>("Introduction");

                    b.Property<decimal>("Salary");

                    b.HasKey("LecturerId");

                    b.ToTable("Lecturers");
                });

            modelBuilder.Entity("LMS.Model.Student", b =>
                {
                    b.Property<string>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(10);

                    b.Property<decimal>("GPA");

                    b.HasKey("StudentId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("LMS.Model.Study", b =>
                {
                    b.Property<string>("StudentId");

                    b.Property<string>("AssignmentId");

                    b.Property<decimal>("AssignmentGrade");

                    b.HasKey("StudentId", "AssignmentId");

                    b.HasIndex("AssignmentId");

                    b.ToTable("Studies");
                });

            modelBuilder.Entity("LMS.Model.UserLS", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("Email");

                    b.Property<bool>("IsAdmin");

                    b.Property<string>("LecturerId");

                    b.Property<string>("Password");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("StudentId");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.HasIndex("LecturerId")
                        .IsUnique();

                    b.HasIndex("StudentId")
                        .IsUnique();

                    b.ToTable("UserLSs");
                });

            modelBuilder.Entity("LMS.Model.Assignment", b =>
                {
                    b.HasOne("LMS.Model.Course", "course")
                        .WithMany("Assignments")
                        .HasForeignKey("CourseId");
                });

            modelBuilder.Entity("LMS.Model.Course", b =>
                {
                    b.HasOne("LMS.Model.Lecturer", "Lecturer")
                        .WithMany("courses")
                        .HasForeignKey("LecturerId");
                });

            modelBuilder.Entity("LMS.Model.Enrolment", b =>
                {
                    b.HasOne("LMS.Model.Course", "Course")
                        .WithMany("Enrolments")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LMS.Model.Student", "Student")
                        .WithMany("Enrolments")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LMS.Model.Study", b =>
                {
                    b.HasOne("LMS.Model.Assignment", "Assignment")
                        .WithMany("Studies")
                        .HasForeignKey("AssignmentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LMS.Model.Student", "Student")
                        .WithMany("Studies")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LMS.Model.UserLS", b =>
                {
                    b.HasOne("LMS.Model.Lecturer", "Lecturer")
                        .WithOne("UserLS")
                        .HasForeignKey("LMS.Model.UserLS", "LecturerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LMS.Model.Student", "Student")
                        .WithOne("UserLS")
                        .HasForeignKey("LMS.Model.UserLS", "StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}