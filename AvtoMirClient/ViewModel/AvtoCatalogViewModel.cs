using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
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
    public ICommand CmdNavigateToNewAvto { get; }
    private ObservableCollection<AutoModel> _autos;
    public ObservableCollection<AutoModel> Autos { get => _autos; set => SetProperty(ref _autos, value); }
    public AvtoCatalogViewModel(MainWindowViewModel owner)
    {
        _owner = owner;
        CmdBack = new AsyncRelayCommand(async () 
            => await NavigationService.GetInstance().Navigate(new MainViewModel(_owner)));
        CmdNavigateToAvto = new AsyncRelayCommand<AutoModel>(CmdNavigateToAvtoHandler!);
        CmdNavigateToNewAvto = new AsyncRelayCommand(async () =>
            await NavigationService.GetInstance().Navigate(new AutoCreationViewModel(owner)));
    }

    public async Task Init()
    {
        Autos = await _owner.GetAutoModels();
    }

    private async Task CmdNavigateToAvtoHandler(object auto)
    {
        if (auto is AutoModel am)
        {
            var a = new Auto()
            {
                Id = am.Id,
                RegNumber = am.RegNumber,
                VinNumber = am.VinNumber,
                CreationYear = am.CreationYear,
                Price = am.Price,
                Color = am.Color,
                IdType = am.Type.Id,
                Image = am.Image
            };
            await NavigationService.GetInstance().Navigate(new CurrentAvtoViewModel(_owner, a));
        }
    }
}