using Microsoft.EntityFrameworkCore;
using School.Domain.Core;
using System;

namespace School.Infrastructure.Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext()
        {
        }

        public SchoolContext(DbContextOptions<SchoolContext> options)
            : base(options)
        { }

        public DbSet<Grade> Grades { get; set; }
        public DbSet<LessonTime> LessonTimes { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Theachers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Grade>().ToTable("Class");
            modelBuilder.Entity<LessonTime>().ToTable("LessonTime");
            modelBuilder.Entity<Schedule>().ToTable("Schedule");
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Subject>().ToTable("Subject");
            modelBuilder.Entity<Teacher>().ToTable("Teacher");
        }
    }
}
