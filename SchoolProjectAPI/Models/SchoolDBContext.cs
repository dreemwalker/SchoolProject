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

        public virtual DbSet<ClassTeacher> ClassTeacher { get; set; }
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<Perfect> Perfects { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=SchoolDB;Trusted_Connection=True;");
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
                    .HasConstraintName("FK_ClassTeacher_Classes1");

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
                    .HasName("IX_Perfects_Cl")
                    .IsUnique();
                entity.HasIndex(e => new { e.StudentId })
                   .HasName("IX_Perfects_St")
                   .IsUnique();

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Perfects)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Perfects_Classes");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Perfects)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.Cascade)
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
