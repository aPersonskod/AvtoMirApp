using AvtoMirIsit.DataContexts;
using AvtoMirModel;
using Microsoft.EntityFrameworkCore;

namespace AvtoMirIsit.Services;

public interface IAvtoService
{
    public DbSet<Auto> GetAll();
}

public interface IDogovorService
{
    public DbSet<Dogovor> GetAll();
}

public interface IClientService
{
    public DbSet<Client> GetAll();
}

public interface IShopService
{
    public DbSet<Shop> GetAll();
}

public interface ICarMakeService
{
    public DbSet<CarMake> GetAll();
}

public interface IEmployeeService
{
    public DbSet<Employee> GetAll();
}

public interface IAutoTypeService
{
    public DbSet<AutoType> GetAll();
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

public class DogovorService : IDogovorService
{
    private readonly DataContext _dataContext;
    public DogovorService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public DbSet<Dogovor> GetAll() => _dataContext.Dogovors;
}

public class ClientService : IClientService
{
    private readonly DataContext _dataContext;
    public ClientService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public DbSet<Client> GetAll() => _dataContext.Clients;
}

public class ShopService : IShopService
{
    private readonly DataContext _dataContext;
    public ShopService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public DbSet<Shop> GetAll() => _dataContext.Shops;
}

public class CarMakeService : ICarMakeService
{
    private readonly DataContext _dataContext;
    public CarMakeService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public DbSet<CarMake> GetAll() => _dataContext.CarMakes;
}

public class EmployeeService : IEmployeeService
{
    private readonly DataContext _dataContext;
    public EmployeeService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public DbSet<Employee> GetAll() => _dataContext.Employees;
}

public class AutoTypeService : IAutoTypeService
{
    private readonly DataContext _dataContext;
    public AutoTypeService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public DbSet<AutoType> GetAll() => _dataContext.AutoTypes;
}