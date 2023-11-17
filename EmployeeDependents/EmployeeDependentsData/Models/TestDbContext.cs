using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDependentsData.Models;

public partial class TestDbContext : DbContext
{
    public TestDbContext()
    {
    }

    public TestDbContext(DbContextOptions<TestDbContext> options)
        : base(options)
    {
    }

    //public virtual DbSet<ElectionType> ElectionTypes { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeDependent> EmployeeDependents { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<ElectionType>(entity =>
        //{
        //    entity.ToTable("ElectionType");

        //    entity.Property(e => e.Id).ValueGeneratedNever();
        //    entity.Property(e => e.ElectionType1)
        //        .HasMaxLength(320)
        //        .HasColumnName("ElectionType");
        //});

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("Employee");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.FirstName).HasMaxLength(320);
            entity.Property(e => e.LastName).HasMaxLength(320);
        });

        modelBuilder.Entity<EmployeeDependent>(entity =>
        {
            entity.ToTable("EmployeeDependent");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.FirstName).HasMaxLength(320);
            entity.Property(e => e.LastName).HasMaxLength(320);

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeDependents)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeeDependent_Employee");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
