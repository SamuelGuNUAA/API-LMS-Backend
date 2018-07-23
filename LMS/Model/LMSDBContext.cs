using System;
using Microsoft.EntityFrameworkCore;

namespace LMS.Model
{
    public class LMSDBContext : DbContext
    {
        public DbSet<UserLS> UserLSs { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<Enrolment> Enrolments { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Study> Studies { get; set; }

        public LMSDBContext(DbContextOptions<LMSDBContext> options) :base(options)
        {
            Database.EnsureCreated();
            //Database.Migrate();

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            //User
            modelBuilder.Entity<UserLS>()
                        .Property(u => u.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<UserLS>()
                        .HasKey(u => u.Id);
                        
            //Course
            modelBuilder.Entity<Course>()
                        .Property(a => a.CourseId).HasMaxLength(10);
            
            modelBuilder.Entity<Course>()
                        .HasKey(a => a.CourseId);


            //Lecturer
            modelBuilder.Entity<Lecturer>()
                        .Property(a => a.LecturerId).HasMaxLength(10);

            modelBuilder.Entity<Lecturer>()
                        .HasKey(a => a.LecturerId);

            modelBuilder.Entity<Lecturer>()
                        .HasOne(s => s.UserLS)
                        .WithOne(s => s.Lecturer)
                        .HasForeignKey<UserLS>(s => s.LecturerId)
                        .OnDelete(DeleteBehavior.Cascade);

            //Student                
            modelBuilder.Entity<Student>()
                        .Property(a => a.StudentId).HasMaxLength(10);

            modelBuilder.Entity<Student>()
                        .HasKey(a => a.StudentId);

            modelBuilder.Entity<Student>()
                        .HasOne(s => s.UserLS)
                        .WithOne(s => s.Student)
                        .HasForeignKey<UserLS>(s => s.StudentId)
                        .OnDelete(DeleteBehavior.Cascade);

            //Enrollment
            modelBuilder.Entity<Enrolment>()
                        .HasKey(a => new{a.CourseId,a.StudentId}); //Remeber the Order


            modelBuilder.Entity<Enrolment>()
                        .HasOne(bc => bc.Course)
                        .WithMany(b => b.Enrolments)
                        .HasForeignKey(bc => bc.CourseId);

            modelBuilder.Entity<Enrolment>()
                        .HasOne(bc => bc.Student)
                        .WithMany(b => b.Enrolments)
                        .HasForeignKey(bc => bc.StudentId);





            //Assignment - Course

            modelBuilder.Entity<Assignment>()
                        .Property(a => a.AssignmentId).HasMaxLength(10);

            modelBuilder.Entity<Assignment>()
                        .HasKey(a => a.AssignmentId);

            modelBuilder.Entity<Course>()
                        .HasMany(c => c.Assignments)
                        .WithOne(a => a.course);

            //Study - Students/Assignemts
            modelBuilder.Entity<Study>()
                        .HasKey(a => new { a.StudentId, a.AssignmentId });

            modelBuilder.Entity<Study>()
                        .HasOne(s => s.Student)
                        .WithMany(st => st.Studies)
                        .HasForeignKey(s => s.StudentId);

            modelBuilder.Entity<Study>()
                        .HasOne(a => a.Assignment)
                        .WithMany(s => s.Studies)
                        .HasForeignKey(a => a.AssignmentId);
        }


       protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
       {
            // DB setting is set here 
            var connectionString = "server=localhost;userid=root;pwd=password;port=3306;database=lms-20180719;sslmode=none;";
            optionsBuilder.UseMySQL(connectionString);
            base.OnConfiguring(optionsBuilder);
       }

    }
}
