using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using AvtoMirClient.Extensions;
using AvtoMirClient.Interfaces;
using AvtoMirModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvtoMirClient.ViewModel;

public class EmployeesViewModel : ObservableObject, IInitable
{
    private readonly MainWindowViewModel _owner;
    private ObservableCollection<Employee> _employees = new ObservableCollection<Employee>();
    public ObservableCollection<Employee> Employees
    {
        get => _employees;
        set => SetProperty(ref _employees, value);
    }
    public ICommand CmdNavigateMain { get; set; }
    public ICommand CmdGoToEmployee { get; set; }
    public ICommand CmdChangeEmployee { get; set; }
    public ICommand CmdDeleteEmployee { get; set; }
    public EmployeesViewModel(MainWindowViewModel owner)
    {
        _owner = owner;
        CmdNavigateMain = new AsyncRelayCommand(async () =>
        { 
            await NavigationService.GetInstance().Navigate(new MainViewModel(owner));
        });
        CmdChangeEmployee = new AsyncRelayCommand<Employee>(async c =>
        {
            await NavigationService.GetInstance().Navigate(new EmployeeViewModel(owner, c));
        });
        CmdGoToEmployee = new AsyncRelayCommand(async () =>
        {
            await NavigationService.GetInstance().Navigate(new EmployeeViewModel(owner));
        });
        CmdDeleteEmployee =
            new AsyncRelayCommand<Employee>(async e =>
            {
                if(!AppExtensions.SureDo($"Точно хотите удалить {e.Fio}?")) return;
                await $"https://localhost:7258/Employee/delete/{e.Id}".DeleteQuery();
                await Init();
            });
    }
    public async Task Init()
    {
        var emps = await "https://localhost:7258/Employee/getAll".GetQuery<Employee>();
        var shops = await "https://localhost:7258/Shop/getAll".GetQuery<Shop>();
        foreach (var employee in emps)
        {
            employee.Shop = shops.First(x => x.Id == employee.ShopId);
        }
        Employees = emps;
    }
}