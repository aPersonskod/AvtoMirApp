using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
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
    }
    public async Task Init()
    {
        await SetClients();
    }
    
    private async Task SetClients()
    {
        using var client = new HttpClient();
        var response = await client.GetAsync("https://localhost:7258/Client/getAll");
        response.EnsureSuccessStatusCode();
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<List<Client>>();
            Clients = result != null ? new ObservableCollection<Client>(result) : new ObservableCollection<Client>();
        }
        else
        {
            $"server error code {response.StatusCode}".Show("Error");
        }
    }
}