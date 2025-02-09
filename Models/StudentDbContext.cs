using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace StudentDatabase.Models;

public partial class StudentDbContext : DbContext
{
    private IConfiguration _EmpConfig { get; set; }
    public StudentDbContext(IConfiguration configuration)
    {
        _EmpConfig = configuration;
    }

    public StudentDbContext(DbContextOptions<StudentDbContext> options, IConfiguration configuration)
        : base(options)
    {
        _EmpConfig = configuration;
    }

    public virtual DbSet<StudentDatum> StudentData { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(_EmpConfig.GetConnectionString("StudentConnectionString"));
        }
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StudentDatum>(entity =>
        {
            entity.HasKey(e => e.StudentId);

            entity.Property(e => e.StudentId)
                .ValueGeneratedNever()
                .HasColumnName("student_id");
            entity.Property(e => e.StudentAge).HasColumnName("student_age");
            entity.Property(e => e.StudentName)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("student_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
