using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using AvtoMirClient.Extensions;
using AvtoMirClient.Interfaces;
using AvtoMirModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvtoMirClient.ViewModel;

public class ClientViewModel : ObservableObject, IInitable
{
    private readonly MainWindowViewModel _owner;
    public Client Client { get; } = new Client();
    private bool _needToUpdate;
    public ICommand CmdSave { get; }
    public ICommand CmdGoBack { get; }
    public ClientViewModel(MainWindowViewModel owner, object? client = null)
    {
        if (client is Client c)
        {
            Client = c;
            _needToUpdate = true;
        }
        _owner = owner;
        CmdSave = new AsyncRelayCommand(CmdSaveHandler);
        CmdGoBack = new AsyncRelayCommand(async () =>
        {
            await NavigationService.GetInstance().Navigate(new ClientsViewModel(owner));
        });
    }
    private async Task CmdSaveHandler()
    {
        if (!_needToUpdate)
        {
            // create
            await "https://localhost:7258/Client/create".PostQuery(Client);
            await NavigationService.GetInstance().Navigate(new ClientsViewModel(_owner));
            return;
        }
        // update
        await "https://localhost:7258/Client/update".PatchQuery(Client);
        await NavigationService.GetInstance().Navigate(new ClientsViewModel(_owner));
    }
    public async Task Init()
    {
    }
}