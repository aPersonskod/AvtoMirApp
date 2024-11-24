using System.Threading.Tasks;
using System.Windows.Input;
using AvtoMirClient.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvtoMirClient.ViewModel;

public class MainViewModel : ObservableObject, IInitable
{
    private readonly MainWindowViewModel _owner;
    public ICommand CmdGoToAvtoCatalog { get; }
    public ICommand CmdGoToOrders { get; }
    public ICommand CmdGoToClients { get; }
    public ICommand CmdGoToEmployees { get; }
    public MainViewModel(MainWindowViewModel owner)
    {
        _owner = owner;
        CmdGoToAvtoCatalog = new AsyncRelayCommand(CmdGoToAvtoCatalogHandler);
        CmdGoToOrders = new AsyncRelayCommand(async () => await NavigationService.GetInstance().Navigate(new OrdersViewModel(_owner)));
        CmdGoToClients = new AsyncRelayCommand(async () => await NavigationService.GetInstance().Navigate(new ClientsViewModel(_owner)));
        CmdGoToEmployees = new AsyncRelayCommand(async () => await NavigationService.GetInstance().Navigate(new EmployeesViewModel(_owner)));
    }

    private async Task CmdGoToAvtoCatalogHandler() => 
        await NavigationService.GetInstance().Navigate(new AvtoCatalogViewModel(_owner));
    public async Task Init()
    {
    }
}