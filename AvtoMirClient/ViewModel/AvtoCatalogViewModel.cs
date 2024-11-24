using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using AvtoMirClient.DTO;
using AvtoMirClient.Extensions;
using AvtoMirClient.Interfaces;
using AvtoMirModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvtoMirClient.ViewModel;

public class AvtoCatalogViewModel : ObservableObject, IInitable
{
    private readonly MainWindowViewModel _owner;
    public ICommand CmdBack { get; }
    public ICommand CmdNavigateToAvto { get; }
    private ObservableCollection<Auto> _autos;
    public ObservableCollection<Auto> Autos { get => _autos; set => SetProperty(ref _autos, value); }
    public AvtoCatalogViewModel(MainWindowViewModel owner)
    {
        _owner = owner;
        CmdBack = new AsyncRelayCommand(async () 
            => await NavigationService.GetInstance().Navigate(new MainViewModel(_owner)));
        CmdNavigateToAvto = new AsyncRelayCommand<Auto>(CmdNavigateToAvtoHandler!);
    }

    public async Task Init()
    {
        Autos = await "https://localhost:7258/Auto/getAll".GetQuery<Auto>();
    }

    private async Task CmdNavigateToAvtoHandler(object auto)
    {
        if (auto is Auto a)
        {
            await NavigationService.GetInstance().Navigate(new CurrentAvtoViewModel(_owner, a));
        }
    }
}