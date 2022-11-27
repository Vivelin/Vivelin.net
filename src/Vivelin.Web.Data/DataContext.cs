using Microsoft.EntityFrameworkCore;

using System;

namespace Vivelin.Web.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options)
        : base(options)
    {
    }

    public virtual DbSet<Quote> Quotes { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}
