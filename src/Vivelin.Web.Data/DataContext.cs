using Microsoft.EntityFrameworkCore;

namespace Vivelin.Web.Data;

public class DataContext(DbContextOptions options) : DbContext(options)
{
    public virtual DbSet<Quote> Quotes { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}
