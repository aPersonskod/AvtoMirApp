using AvtoMirModel;
using Microsoft.EntityFrameworkCore;

namespace AvtoMirIsit.DataContexts;

public class DataContext : DbContext
{
    public DbSet<Auto> Autos { get; set; }
    public DbSet<Dogovor> Dogovors { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Shop> Shops { get; set; }
    public DbSet<CarMake> CarMakes { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<AutoType> AutoTypes { get; set; }
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseSerialColumns();
    }
}