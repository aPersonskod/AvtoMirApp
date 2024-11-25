using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using AvtoMirClient.Extensions;
using AvtoMirClient.Interfaces;
using AvtoMirModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvtoMirClient.ViewModel;

public class ClientsViewModel : ObservableObject, IInitable
{
    private readonly MainWindowViewModel _owner;
    private ObservableCollection<Client> _clients = new ObservableCollection<Client>();
    public ObservableCollection<Client> Clients
    {
        get => _clients;
        set => SetProperty(ref _clients, value);
    }
    public ICommand CmdNavigateMain { get; set; }
    public ICommand CmdGoToClient { get; set; }
    public ICommand CmdChangeClient { get; set; }
    public ICommand CmdDeleteClient { get; set; }
    public ClientsViewModel(MainWindowViewModel owner)
    {
        _owner = owner;
        CmdNavigateMain = new AsyncRelayCommand(async () =>
        { 
            await NavigationService.GetInstance().Navigate(new MainViewModel(owner));
        });
        CmdChangeClient = new AsyncRelayCommand<Client>(async c =>
        {
            await NavigationService.GetInstance().Navigate(new ClientViewModel(owner, c));
        });
        CmdGoToClient = new AsyncRelayCommand(async () =>
        {
            await NavigationService.GetInstance().Navigate(new ClientViewModel(owner));
        });
        CmdDeleteClient =
            new AsyncRelayCommand<Client>(async c =>
            {
                if(!AppExtensions.SureDo($"Точно хотите удалить {c.Fio}?")) return;
                await $"https://localhost:7258/Client/delete/{c.Id}".DeleteQuery();
                await Init();
            });
    }
    public async Task Init()
    {
        Clients = await "https://localhost:7258/Client/getAll".GetQuery<Client>();
    }
}