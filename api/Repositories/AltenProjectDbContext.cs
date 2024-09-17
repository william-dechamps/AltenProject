namespace AltenProject.Repositories;
using Microsoft.EntityFrameworkCore;
using AltenProject.Entities;

public class AltenProjectDbContext : DbContext
{
    public AltenProjectDbContext(DbContextOptions<AltenProjectDbContext> options)
       : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductEntity>()
            .Property(p => p.InventoryStatus)
            .HasConversion(
                v => v.ToString(),
                v => (InventoryStatus)Enum.Parse(typeof(InventoryStatus), v));
    }

    public override int SaveChanges()
    {
        ChangeTracker.DetectChanges();
        return base.SaveChanges();
    }

    public DbSet<ProductEntity> Products { get; set; } = null!;
}