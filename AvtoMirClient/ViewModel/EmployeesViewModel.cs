using System.Threading.Tasks;
using System.Windows.Input;
using AvtoMirClient.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvtoMirClient.ViewModel;

public class EmployeesViewModel : ObservableObject, IInitable
{
    private readonly MainWindowViewModel _owner;
    public ICommand CmdNavigateMain { get; set; }
    public EmployeesViewModel(MainWindowViewModel owner)
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