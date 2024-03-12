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
            entity.HasKey(e => e.Id).HasName("PK__CPR__3213E83F7E677FE0");

            entity.ToTable("CPR");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cprnr)
                .HasMaxLength(500)
                .HasColumnName("CPRnr");
            entity.Property(e => e.User).HasMaxLength(500);
        });

        modelBuilder.Entity<TodolostTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TodolostTb_Id"); // Define the primary key
            entity.ToTable("TodolostTB");
            entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
            entity.Property(e => e.Items).HasMaxLength(255);
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.User).WithMany().HasForeignKey(d => d.Userid).HasConstraintName("FK_CPR_Todo");
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
