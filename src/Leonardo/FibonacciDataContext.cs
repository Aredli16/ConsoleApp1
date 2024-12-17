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