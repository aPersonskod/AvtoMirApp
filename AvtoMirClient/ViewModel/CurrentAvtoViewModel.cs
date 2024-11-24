using System.DirectoryServices.ActiveDirectory;
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
    public Auto Auto { get; }
    public ICommand CmdGoBack { get; }
    public ICommand CmdNavigateAutoCatalog { get; }
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
        Auto = auto;
        CmdGoBack = new AsyncRelayCommand(async () =>
            await NavigationService.GetInstance().Navigate(new AvtoCatalogViewModel(owner)));
        CmdNavigateOrders = new AsyncRelayCommand(async () =>
            await NavigationService.GetInstance().Navigate(new OrdersViewModel(owner)));
        CmdPlaceOrder = new RelayCommand(CmdPlaceOrderHandler);
        CmdConfirmOrder = new AsyncRelayCommand(CmdConfirmOrderHandler);
        CmdCancelOrder = new RelayCommand(() => { IsOrdering = false;});
    }

    private void CmdPlaceOrderHandler()
    {
        IsOrdering = true;
    }
    private async Task CmdConfirmOrderHandler()
    {
        IsOrdered = true;
    }
    public async Task Init()
    {
        
    }
}