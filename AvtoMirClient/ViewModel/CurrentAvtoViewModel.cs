using System;
using System.Collections.ObjectModel;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using AvtoMirClient.Extensions;
using AvtoMirClient.Interfaces;
using AvtoMirModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvtoMirClient.ViewModel;

public class CurrentAvtoViewModel : ObservableObject, IInitable
{
    private readonly MainWindowViewModel _owner;
    public AutoModel Auto { get; private set; }
    private Auto _auto { get; }
    private ObservableCollection<Client> _clients = new ObservableCollection<Client>();
    public ObservableCollection<Client> Clients
    {
        get => _clients;
        set => SetProperty(ref _clients, value);
    }
    private Client _selectedClient;
    public Client SelectedClient
    {
        get => _selectedClient;
        set => SetProperty(ref _selectedClient, value);
    }
    private ObservableCollection<Employee> _employees = new ObservableCollection<Employee>();
    public ObservableCollection<Employee> Employees
    {
        get => _employees;
        set => SetProperty(ref _employees, value);
    }
    private Employee _selectedEmployee;
    public Employee SelectedEmployee
    {
        get => _selectedEmployee;
        set => SetProperty(ref _selectedEmployee, value);
    }
    public ICommand CmdGoBack { get; }
    public ICommand CmdNavigateOrders { get; }
    public ICommand CmdPlaceOrder { get; }
    public ICommand CmdConfirmOrder { get; }
    public ICommand CmdCancelOrder { get; }
    private bool _isOrdering;
    public bool IsOrdering
    {
        get => _isOrdering;
        set => SetProperty(ref _isOrdering, value);
    }
    private bool _isOrdered;
    public bool IsOrdered
    {
        get => _isOrdered;
        set => SetProperty(ref _isOrdered, value);
    }
    public CurrentAvtoViewModel(MainWindowViewModel owner, Auto auto)
    {
        _owner = owner;
        _auto = auto;
        CmdGoBack = new AsyncRelayCommand(async () =>
            await NavigationService.GetInstance().Navigate(new AvtoCatalogViewModel(owner)));
        CmdNavigateOrders = new AsyncRelayCommand(async () =>
            await NavigationService.GetInstance().Navigate(new OrdersViewModel(owner)));
        CmdPlaceOrder = new RelayCommand(CmdPlaceOrderHandler);
        CmdConfirmOrder = new AsyncRelayCommand(CmdConfirmOrderHandler);
        CmdCancelOrder = new RelayCommand(() =>
        {
            IsOrdering = false;
            SelectedClient = null;
            SelectedEmployee = null;
        });
    }

    private void CmdPlaceOrderHandler()
    {
        IsOrdering = true;
    }
    private async Task CmdConfirmOrderHandler()
    {
        // todo создать заказ
        // если заказ создался то...
        if(SelectedClient == null) return;
        if(SelectedEmployee == null) return;
        var dogovor = new Dogovor()
        {
            SaleDate = DateTime.Now,
            Cost = Auto.Price,
            IdEmployee = SelectedEmployee.Id,
            IdClient = SelectedClient.Id,
            IdAvto = Auto.Id
        };
        await "https://localhost:7258/Dogovor/Create".PostQuery(dogovor);
        IsOrdered = true;
        IsOrdering = false;
        "Звказ успешно создан".Show();
    }
    public async Task Init()
    {
        var autos = await _owner.GetAutoModels();
        Auto = autos.First(x => x.Id == _auto.Id);
        OnPropertyChanged(nameof(Auto));
        Employees = await "https://localhost:7258/Employee/getAll".GetQuery<Employee>();
        Clients = await "https://localhost:7258/Client/getAll".GetQuery<Client>();
    }
}