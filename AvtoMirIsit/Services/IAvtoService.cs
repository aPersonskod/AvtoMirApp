using AvtoMirIsit.DataContexts;
using AvtoMirIsit.Entities;
using Microsoft.EntityFrameworkCore;

namespace AvtoMirIsit.Services;

public interface IAvtoService
{
    public DbSet<Auto> GetAll();
}

public class AvtoService : IAvtoService
{
    private readonly DataContext _dataContext;
    public AvtoService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public DbSet<Auto> GetAll() => _dataContext.Autos;
}