﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BlazorEcommerce.Server.Data
{
    public partial class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CartItems> CartItems { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Class> Class { get; set; }
        public virtual DbSet<Finance> Finance { get; set; }
        public virtual DbSet<Images> Images { get; set; }
        public virtual DbSet<Months> Months { get; set; }
        public virtual DbSet<OrderItems> OrderItems { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<ProductTypes> ProductTypes { get; set; }
        public virtual DbSet<ProductVariants> ProductVariants { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartItems>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.ProductId, e.ProductTypeId });
            });

            modelBuilder.Entity<Categories>(entity =>
            {
                entity.Property(e => e.Deleted)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.Url).IsRequired();

                entity.Property(e => e.Visible)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");
            });

            modelBuilder.Entity<Class>(entity =>
            {
                entity.ToTable("CLASS");

                entity.Property(e => e.Classid)
                    .HasColumnName("CLASSID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Classname).HasColumnName("CLASSNAME");

                entity.Property(e => e.Note).HasColumnName("NOTE");
            });

            modelBuilder.Entity<Finance>(entity =>
            {
                entity.ToTable("FINANCE");

                entity.Property(e => e.Financeid)
                    .HasColumnName("FINANCEID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Ispaid).HasColumnName("ISPAID");

                entity.Property(e => e.Lastpaymentdate)
                    .HasColumnType("date")
                    .HasColumnName("LASTPAYMENTDATE");

                entity.Property(e => e.Monthid).HasColumnName("MONTHID");

                entity.Property(e => e.Note).HasColumnName("NOTE");

                entity.Property(e => e.Paymentdate)
                    .HasColumnType("date")
                    .HasColumnName("PAYMENTDATE");

                entity.Property(e => e.Personid).HasColumnName("PERSONID");

                entity.HasOne(d => d.Month)
                    .WithMany(p => p.Finance)
                    .HasForeignKey(d => d.Monthid)
                    .HasConstraintName("FK_FINANCE_MONTHS");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Finance)
                    .HasForeignKey(d => d.Personid)
                    .HasConstraintName("FK_FINANCE_PERSON");
            });

            modelBuilder.Entity<Images>(entity =>
            {
                entity.HasIndex(e => e.ProductId, "IX_Images_ProductId");

                entity.Property(e => e.Data).IsRequired();

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Images)
                    .HasForeignKey(d => d.ProductId);
            });

            modelBuilder.Entity<Months>(entity =>
            {
                entity.HasKey(e => e.Monthid);

                entity.ToTable("MONTHS");

                entity.Property(e => e.Monthid)
                    .HasColumnName("MONTHID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Monthname)
                    .HasMaxLength(50)
                    .HasColumnName("MONTHNAME");
            });

            modelBuilder.Entity<OrderItems>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ProductId, e.ProductTypeId });

                entity.HasIndex(e => e.ProductId, "IX_OrderItems_ProductId");

                entity.HasIndex(e => e.ProductTypeId, "IX_OrderItems_ProductTypeId");

                entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.OrderId);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.ProductId);

                entity.HasOne(d => d.ProductType)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.ProductTypeId);
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("PERSON");

                entity.Property(e => e.Personid)
                    .HasColumnName("PERSONID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Classid).HasColumnName("CLASSID");

                entity.Property(e => e.Deleted).HasColumnName("DELETED");

                entity.Property(e => e.Fullname)
                    .IsRequired()
                    .HasColumnName("FULLNAME");

                entity.Property(e => e.Ismature).HasColumnName("ISMATURE");

                entity.Property(e => e.Note)
                    .HasMaxLength(255)
                    .HasColumnName("NOTE");

                entity.Property(e => e.Parentname)
                    .HasMaxLength(255)
                    .HasColumnName("PARENTNAME");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("PHONE");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Person)
                    .HasForeignKey(d => d.Classid)
                    .HasConstraintName("FK_PERSON_CLASS");
            });

            modelBuilder.Entity<ProductTypes>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<ProductVariants>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Deleted)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.OriginalPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ProductTypeId).ValueGeneratedOnAdd();

                entity.Property(e => e.Visible)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductVariants)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductVariants_Products");

                entity.HasOne(d => d.ProductType)
                    .WithMany(p => p.ProductVariants)
                    .HasForeignKey(d => d.ProductTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductVariants_Products1");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasIndex(e => e.CategoryId, "IX_Products_CategoryId");

                entity.Property(e => e.Deleted)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Featured)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.ImageUrl).IsRequired();

                entity.Property(e => e.Title).IsRequired();

                entity.Property(e => e.Visible)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Email).IsRequired();

                entity.Property(e => e.PasswordHash).IsRequired();

                entity.Property(e => e.PasswordSalt).IsRequired();

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasDefaultValueSql("(N'')");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}