using AvtoMirIsit.Entities;
using Microsoft.EntityFrameworkCore;

namespace AvtoMirIsit.DataContexts;

public class DataContext : DbContext
{
    public DbSet<Auto> Autos { get; set; }
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseSerialColumns();
    }
}