using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SchoolProjectAPI.Models
{
    public partial class SchoolDBContext : DbContext
    {
        public SchoolDBContext()
        {
        }

        public SchoolDBContext(DbContextOptions<SchoolDBContext> options)
            : base(options)
        {
        }

        public DbSet<ClassTeacher> ClassTeacher { get; set; }
        public  DbSet<Class> Classes { get; set; }
        public  DbSet<Perfect> Perfects { get; set; }
        public  DbSet<Student> Students { get; set; }
        public  DbSet<Teacher> Teachers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=KELPI\\SQLEXPRESS;Initial Catalog=SchoolDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            modelBuilder.Entity<ClassTeacher>(entity =>
            {
                entity.HasOne(d => d.ClassTeacherNavigation)
                    .WithMany(p => p.ClassTeacher)
                    .HasForeignKey(d => d.ClassTeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClassTeacher_Classes");

                entity.HasOne(d => d.TeacherClass)
                    .WithMany(p => p.ClassTeacher)
                    .HasForeignKey(d => d.TeacherClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClassTeacher_Teachers");
            });

            modelBuilder.Entity<Class>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Perfect>(entity =>
            {
                entity.HasIndex(e => new { e.ClassId, e.StudentId })
                    .HasName("IX_Perfects")
                    .IsUnique();

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Perfects)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Perfects_Classes");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Perfects)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Perfects_Students");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.ClassId).HasColumnName("classId");

                entity.Property(e => e.Fullname)
                    .HasColumnName("fullname")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasColumnName("gender")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.ClassId)
                    .HasConstraintName("FK_Students_Classes");
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Fullname)
                    .HasColumnName("fullname")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Position)
                    .HasColumnName("position")
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });
        }
    }
}
