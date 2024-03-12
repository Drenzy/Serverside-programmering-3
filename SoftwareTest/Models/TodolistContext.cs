using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SoftwareTest.Models;

public partial class TodolistContext : DbContext
{
    public TodolistContext()
    {
    }

    public TodolistContext(DbContextOptions<TodolistContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cpr> Cprs { get; set; }

    public virtual DbSet<TodolostTb> TodolostTbs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Todolist;Trusted_Connection=True;MultipleActiveResultSets=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cpr>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CPR__3213E83F1EB15153");

            entity.ToTable("CPR");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Cprnr)
                .HasMaxLength(500)
                .HasColumnName("CPRnr");
            entity.Property(e => e.User).HasMaxLength(200);
        });

        modelBuilder.Entity<TodolostTb>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("TodolostTB");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Item).HasMaxLength(1);
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.IdNavigation).WithMany()
                .HasForeignKey(d => d.Id)
                .HasConstraintName("FK_TodolostTB_CPR");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
