using System.Threading.Tasks;
using System.Windows.Input;
using AvtoMirClient.Interfaces;
using AvtoMirModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvtoMirClient.ViewModel;

public class ClientViewModel : ObservableObject, IInitable
{
    private readonly MainWindowViewModel _owner;
    public Client Client { get; }
    public ICommand CmdSave { get; }
    public ClientViewModel(MainWindowViewModel owner, object? client = null)
    {
        if (client is Client c)
            Client = c;
        _owner = owner;
        CmdSave = new AsyncRelayCommand(CmdSaveHandler);
    }
    private async Task CmdSaveHandler()
    {
        if (Client == null)
        {
            // create
            return;
        }
        // update
    }
    public async Task Init()
    {
    }
}