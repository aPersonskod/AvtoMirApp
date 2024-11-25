using AvtoMirIsit.DataContexts;
using AvtoMirModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace AvtoMirIsit.Services;

public interface IAvtoService
{
    public DbSet<Auto> GetAll();
    Auto Create(Auto auto);
}

public interface IDogovorService
{
    public DbSet<Dogovor> GetAll();
    Dogovor Create(Dogovor dogovor);
}

public interface IClientService
{
    public DbSet<Client> GetAll();
    Client Create(Client client);
    Client Update(Client client);
    void Delete(int id);
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
    Employee Create(Employee employee);
    Employee Update(Employee employee);
    void Delete(int id);
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
    public Auto Create(Auto auto)
    {
        var lastAuto = _dataContext.Autos.ToList()?.LastOrDefault();
        auto.Id = lastAuto is null ? 1 : lastAuto.Id + 1;
        _dataContext.Database.ExecuteSqlRaw(
            "INSERT INTO Автомобиль (id_автомобиля, Номер, vin_номер, Год_выпуска, Цена, Цвет, id_типа, photo)"
        +" VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7})", auto.Id, auto.RegNumber, auto.VinNumber, auto.CreationYear,
            auto.Price, auto.Color, auto.IdType, auto.Image);
        return auto;
    }
}

public class DogovorService : IDogovorService
{
    private readonly DataContext _dataContext;
    public DogovorService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    public DbSet<Dogovor> GetAll() => _dataContext.Dogovors;
    public Dogovor Create(Dogovor dogovor)
    {
        var lastDogovor = _dataContext.Dogovors.ToList().LastOrDefault();
        dogovor.Id = lastDogovor is null ? 1 : lastDogovor.Id + 1;
        _dataContext.Database.ExecuteSqlRaw(
            "INSERT INTO Договор (id_договора, Дата_продажи, Сумма_продажи, id_сотрудника, id_автомобиля, id_клиента) VALUES ({0}, {1}, {2}, {3}, {4}, {5})", 
            dogovor.Id, dogovor.SaleDate.ToUniversalTime(), dogovor.Cost, dogovor.IdEmployee, dogovor.IdAvto, dogovor.IdClient);
        return dogovor;
    }
}

public class ClientService : IClientService
{
    private readonly DataContext _dataContext;
    public ClientService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    public DbSet<Client> GetAll() => _dataContext.Clients;
    public Client Create(Client client)
    {
        var lastClient = _dataContext.Clients.ToList().LastOrDefault();
        client.Id = lastClient is null ? 1 : lastClient.Id + 1;
        _dataContext.Database.ExecuteSqlRaw("INSERT INTO Клиент (id_клиента, ФИО, Адрес, Телефон) VALUES ({0}, {1}, {2}, {3})", 
            client.Id, client.Fio, client.Adress, client.Mobile);
        return client;
    }
    public Client Update(Client client)
    {
        _dataContext.Database.ExecuteSqlRaw(
            "UPDATE Клиент SET ФИО={0}, Адрес={1}, Телефон={2} WHERE id_клиента={3}",
            client.Fio, client.Adress, client.Mobile, client.Id);
        return client;
    }
    public void Delete(int id)
    {
        _dataContext.Database.ExecuteSqlRaw("DELETE FROM Клиент WHERE id_клиента={0}", id);
    }
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
    public Employee Create(Employee employee)
    {
        var lastEmployee = _dataContext.Employees.ToList().LastOrDefault();
        employee.Id = lastEmployee is null ? 1 : lastEmployee.Id + 1;
        _dataContext.Database.ExecuteSqlRaw("INSERT INTO Сотрудник (id_сотрудника, ФИО, Телефон, id_магазина) VALUES ({0}, {1}, {2}, {3})", 
            employee.Id, employee.Fio, employee.Mobile, employee.ShopId);
        return employee;
    }
    public Employee Update(Employee employee)
    {
        _dataContext.Database.ExecuteSqlRaw(
            "UPDATE Сотрудник SET ФИО={0}, Телефон={1}, id_магазина={2} WHERE id_сотрудника={3}",
            employee.Fio, employee.Mobile, employee.ShopId, employee.Id);
        return employee;
    }
    public void Delete(int id)
    {
        _dataContext.Database.ExecuteSqlRaw("DELETE FROM Сотрудник WHERE id_сотрудника={0}", id);
    }
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