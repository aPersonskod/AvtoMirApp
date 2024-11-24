using System.Threading.Tasks;
using System.Windows.Input;
using AvtoMirClient.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvtoMirClient.ViewModel;

public class OrdersViewModel : ObservableObject, IInitable
{
    private readonly MainWindowViewModel _owner;
    public ICommand CmdNavigateMain { get; }
    public OrdersViewModel(MainWindowViewModel owner)
    {
        _owner = owner;
        CmdNavigateMain = new AsyncRelayCommand(async () =>
        { 
            await NavigationService.GetInstance().Navigate(new MainViewModel(owner));
        });
    }
    public async Task Init()
    {
    }
}