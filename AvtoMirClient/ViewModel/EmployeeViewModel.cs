using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows.Input;
using AvtoMirClient.Extensions;
using AvtoMirClient.Interfaces;
using AvtoMirModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvtoMirClient.ViewModel;

public class EmployeeViewModel : ObservableObject, IInitable
{
    private readonly MainWindowViewModel _owner;
    public Employee Employee { get; } = new Employee();
    private Shop _selectedShop;
    public Shop SelectedShop
    {
        get => Employee.Shop;
        set
        {
            Employee.Shop = value;
            Employee.ShopId = value.Id;
            OnPropertyChanged(nameof(SelectedShop));
        }
    }
    private bool _needToUpdate;
    public ICommand CmdSave { get; }
    public ICommand CmdGoBack { get; }
    public ObservableCollection<Shop> Shops { get; set; }
    public EmployeeViewModel(MainWindowViewModel owner, object? client = null)
    {
        if (client is Employee c)
        {
            Employee = c;
            _needToUpdate = true;
        }
        _owner = owner;
        CmdSave = new AsyncRelayCommand(CmdSaveHandler);
        CmdGoBack = new AsyncRelayCommand(async () =>
        {
            await NavigationService.GetInstance().Navigate(new EmployeesViewModel(owner));
        });
    }
    private async Task CmdSaveHandler()
    {
        if (!_needToUpdate)
        {
            // create
            await "https://localhost:7258/Employee/create".PostQuery(Employee);
            await NavigationService.GetInstance().Navigate(new EmployeesViewModel(_owner));
            return;
        }
        // update
        await "https://localhost:7258/Employee/update".PatchQuery(Employee);
        await NavigationService.GetInstance().Navigate(new EmployeesViewModel(_owner));
    }
    public async Task Init()
    {
        Shops = await "https://localhost:7258/Shop/getAll".GetQuery<Shop>();
        OnPropertyChanged(nameof(Shops));
        if(Employee.Shop == null) return;
        SelectedShop = Shops.First(x => x.Id == Employee.Shop.Id);
        OnPropertyChanged(nameof(SelectedShop));
    }
}