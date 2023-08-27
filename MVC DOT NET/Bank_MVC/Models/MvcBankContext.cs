using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Bank_MVC.Models;

namespace Bank_MVC.Models;

public partial class MvcBankContext : DbContext
{
    public MvcBankContext()
    {
    }

    public MvcBankContext(DbContextOptions<MvcBankContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MsSqlLocalDb;Initial Catalog=mvc_bank;Integrated Security=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.AccountNumber).HasName("PK__Customer__BE2ACD6E922A0303");

            entity.Property(e => e.AccountNumber).ValueGeneratedNever();
            entity.Property(e => e.Balance).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.FullName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__Transact__55433A6BD5E5D5AF");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TransactionDate)
                .HasColumnType("datetime")
                .HasColumnName("Transaction_Date");

            entity.HasOne(d => d.ReciverAccountNavigation).WithMany(p => p.TransactionReciverAccountNavigations)
                .HasForeignKey(d => d.ReciverAccount)
                .HasConstraintName("FK_RecieverAccount");

            entity.HasOne(d => d.SenderAccountNavigation).WithMany(p => p.TransactionSenderAccountNavigations)
                .HasForeignKey(d => d.SenderAccount)
                .HasConstraintName("FK_SenderAccount");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public DbSet<Bank_MVC.Models.Login> Login { get; set; } = default!;
}
