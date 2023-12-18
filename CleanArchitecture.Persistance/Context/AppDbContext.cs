using CleanArchitecture.Domain.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Persistance.Context;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyeReferance).Assembly); // Configuration klasörürümüzde oluşturduğumuz tüm configleri görmemeizi sağlar.

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) // SaveChanges metodu kullanıldığında yani databasede ekleme güncelleme işlemleri için ekleme ve güncelleme için datetimelarını atadık.
    {
        var entries = ChangeTracker.Entries<Entity>();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
                entry.Property(p => p.CreatedDate)
                    .CurrentValue = DateTime.Now;

            if (entry.State == EntityState.Modified)
                entry.Property(p => p.UpdatedDate)
                    .CurrentValue = DateTime.Now;
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
