using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Foods.Data;

public partial class FopsContext : DbContext
{
    public FopsContext()
    {
    }

    public FopsContext(DbContextOptions<FopsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Enquiries> CustomerEnquiries { get; set; }

    public virtual DbSet<Food> Foods { get; set; }

    public virtual DbSet<FoodsCustomization> FoodsCustomizations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DEV-5CG2136386\\MSSQLSERVER15;Initial Catalog=FOPS;Trusted_Connection=true;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customer");

            entity.Property(e => e.CustomerId).ValueGeneratedNever();
            entity.Property(e => e.CustomerContact)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CustomerEmail)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CustomerLastCreated).HasColumnType("datetime");
            entity.Property(e => e.CustomerLastUpdated).HasColumnType("datetime");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Enquiries>(entity =>
        {
            entity.HasKey(e => e.EnquiriesId);

            entity.Property(e => e.EnquiriesId).ValueGeneratedNever();
            entity.Property(e => e.EnquiriesDescription)
                .HasMaxLength(5000)
                .IsUnicode(false);
            entity.Property(e => e.EnquiriesLastCreated).HasColumnType("datetime");
            entity.Property(e => e.EnquiriesLastUpdated).HasColumnType("datetime");
            entity.Property(e => e.EnquiriesSubject)
                .HasMaxLength(1000)
                .IsUnicode(false);

            entity.HasOne(d => d.Customer).WithMany(p => p.CustomerEnquiries)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Customer_CustomerEnquiries");
        });

        modelBuilder.Entity<Food>(entity =>
        {
            entity.Property(e => e.FoodId).ValueGeneratedNever();
            entity.Property(e => e.FoodDescription)
                .HasMaxLength(5000)
                .IsUnicode(false);
            entity.Property(e => e.FoodLastCreated).HasColumnType("datetime");
            entity.Property(e => e.FoodLastUpdated).HasColumnType("datetime");
            entity.Property(e => e.FoodName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FoodPrice).HasColumnType("decimal(11, 2)");
        });

        modelBuilder.Entity<FoodsCustomization>(entity =>
        {
            entity.HasKey(e => e.FoodCustomizationId);

            entity.ToTable("FoodsCustomization");

            entity.Property(e => e.FoodCustomizationId).ValueGeneratedNever();
            entity.Property(e => e.FoodCustomizationLastCreated).HasColumnType("datetime");
            entity.Property(e => e.FoodCustomizationLastUpdated).HasColumnType("datetime");
            entity.Property(e => e.FoodCustomizationName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FoodCustomizationPrice).HasColumnType("decimal(11, 2)");

            entity.HasOne(d => d.Food).WithMany(p => p.FoodsCustomizations)
                .HasForeignKey(d => d.FoodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Foods_FoodsCustomization");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
