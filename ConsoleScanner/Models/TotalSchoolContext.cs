using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ScannerAPIProject.Models;

public partial class TotalSchoolContext : DbContext
{
    public TotalSchoolContext()
    {
    }

    public TotalSchoolContext(DbContextOptions<TotalSchoolContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MenuPage> MenuPage { get; set; }

    public virtual DbSet<MenuPage3> MenuPage3 { get; set; }

    public virtual DbSet<MenuPageApi3> MenuPageApi3 { get; set; }

    public virtual DbSet<MenuPageMapping> MenuPageMapping { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=10.30.170.170,19434;Database=TotalSchool;User Id=SchoolWeb;Password=2wsx@WSX1qaz!QAZ;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MenuPage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_MenuPage_1");

            entity.ToTable("MenuPage");

            entity.Property(e => e.Controller).HasMaxLength(150);
            entity.Property(e => e.Icon).HasMaxLength(150);
            entity.Property(e => e.Path).HasMaxLength(500);
            entity.Property(e => e.State).HasMaxLength(150);
            entity.Property(e => e.State2).HasMaxLength(150);
            entity.Property(e => e.Title).HasMaxLength(150);
        });

        modelBuilder.Entity<MenuPage3>(entity =>
        {
            entity.HasKey(e => e.MenuPageId).HasName("PK__MenuPage__F643885139941E2E");

            entity.ToTable("MenuPage3");
        });

        modelBuilder.Entity<MenuPageApi3>(entity =>
        {
            entity.HasKey(e => e.MemuPageApiId).HasName("PK__MenuPage__BC4DBC6ED8FD0A92");

            entity.ToTable("MenuPageApi3");
        });

        modelBuilder.Entity<MenuPageMapping>(entity =>
        {
            entity.HasKey(e => e.MenuPageMappingId).HasName("PK__MenuPage__9F544F993B42020B");

            entity.ToTable("MenuPageMapping");

            entity.Property(e => e.Title).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
