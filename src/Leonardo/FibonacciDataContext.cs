using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Leonardo;

public partial class FibonacciDataContext : DbContext
{
    public FibonacciDataContext()
    {
    }

    public FibonacciDataContext(DbContextOptions<FibonacciDataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TFibonacci> TFibonaccis { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost,1433;Initial Catalog=FibonacciData;Integrated Security=False;User ID=SA;Password=Password!;MultipleActiveResultSets=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TFibonacci>(entity =>
        {
            entity.HasKey(e => e.FibId).HasName("PK_Fibonacci");

            entity.ToTable("T_Fibonacci", "sch_fib");

            entity.Property(e => e.FibId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("FIB_Id");
            entity.Property(e => e.FibInput).HasColumnName("FIB_Input");
            entity.Property(e => e.FibOutput).HasColumnName("FIB_Output");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
