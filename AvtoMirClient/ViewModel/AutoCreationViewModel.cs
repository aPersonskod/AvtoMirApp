using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using AvtoMirClient.Extensions;
using AvtoMirClient.Interfaces;
using AvtoMirModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvtoMirClient.ViewModel;

public class AutoCreationViewModel : ObservableObject, IInitable
{
    private readonly MainWindowViewModel _owner;
    public AutoModel Auto { get; } = new AutoModel();
    public ObservableCollection<AutoType> Types { get; set; }
    public ICommand CmdSave { get; }
    public ICommand CmdGoBack { get; }
    public AutoCreationViewModel(MainWindowViewModel owner)
    {
        _owner = owner;
        CmdSave = new AsyncRelayCommand(CmdSaveHandler);
        CmdGoBack = new AsyncRelayCommand(
            async () => await NavigationService.GetInstance().Navigate(new AvtoCatalogViewModel(owner)));
    }
    private async Task CmdSaveHandler()
    {
        if (Auto.Price == 0) return;
        if (Auto.Type == null) return;
        if (string.IsNullOrEmpty(Auto.CreationYear)) return;
        if (string.IsNullOrEmpty(Auto.Image)) return;
        var auto = new Auto()
        {
            Id = Auto.Id,
            RegNumber = Auto.RegNumber,
            VinNumber = Auto.VinNumber,
            CreationYear = Auto.CreationYear,
            Price = Auto.Price,
            Color = Auto.Color,
            IdType = Auto.Type.Id,
            Image = Auto.Image,
        };
        await "https://localhost:7258/Auto/create".PostQuery(auto);
        await NavigationService.GetInstance().Navigate(new AvtoCatalogViewModel(_owner));
    }
    public async Task Init()
    {
        Types = await "https://localhost:7258/AutoType/getAll".GetQuery<AutoType>();
        OnPropertyChanged(nameof(Types));
    }
}