using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
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

public class AvtoCatalogViewModel : ObservableObject, IInitable
{
    private readonly MainWindowViewModel _owner;
    public ICommand CmdBack { get; }
    public ICommand CmdNavigateToAvto { get; }
    public ICommand CmdNavigateToNewAvto { get; }
    public ICommand CmdConfirmFilter { get; }
    public ICommand CmdResetFilter { get; }
    private ObservableCollection<AutoModel> _autos;
    public ObservableCollection<AutoModel> Autos { get => _autos; set => SetProperty(ref _autos, value); }
    public ObservableCollection<AutoModel> AllAutos { get; set; }
    private bool _isAutoModelEnabled;
    public bool IsAutoModelEnabled
    {
        get => _isAutoModelEnabled;
        set => SetProperty(ref _isAutoModelEnabled, value);
    }
    private int _fromPriceFilter;
    public int FromPriceFilter
    {
        get => _fromPriceFilter;
        set => SetProperty(ref _fromPriceFilter, value);
    }
    private int _toPriceFilter;
    public int ToPriceFilter
    {
        get => _toPriceFilter;
        set => SetProperty(ref _toPriceFilter, value);
    }
    private List<AutoTypeModel> AllAutoTypes { get; set; }
    public List<AutoTypeModel> AutoTypes { get; set; }
    private AutoTypeModel _selectedAutoType;
    public AutoTypeModel SelectedAutoType
    {
        get => _selectedAutoType;
        set => SetProperty(ref _selectedAutoType, value);
    }
    public List<CarMake> AllAutoMarks { get; set; }
    private CarMake _selectedAutoMark;
    public CarMake SelectedAutoMark
    {
        get => _selectedAutoMark;
        set
        {
            SetProperty(ref _selectedAutoMark, value);
            if (value == null) return;
            AutoTypes = AllAutoTypes.Where(x => x.Mark.Id == value.Id).ToList();
            IsAutoModelEnabled = true;
            OnPropertyChanged(nameof(AutoTypes));
        }
    }
    public AvtoCatalogViewModel(MainWindowViewModel owner)
    {
        _owner = owner;
        IsAutoModelEnabled = false;
        CmdBack = new AsyncRelayCommand(async () 
            => await NavigationService.GetInstance().Navigate(new MainViewModel(_owner)));
        CmdNavigateToAvto = new AsyncRelayCommand<AutoModel>(CmdNavigateToAvtoHandler!);
        CmdNavigateToNewAvto = new AsyncRelayCommand(async () =>
            await NavigationService.GetInstance().Navigate(new AutoCreationViewModel(owner)));
        CmdConfirmFilter = new RelayCommand(CmdConfirmFilterHandler);
        CmdResetFilter = new RelayCommand(CmdResetFilterHandler);
    }

    private void CmdConfirmFilterHandler()
    {
        var filteredAutos = AllAutos.ToList();
        if (FromPriceFilter != 0)
        {
            filteredAutos = filteredAutos.Where(x => x.Price >= FromPriceFilter).ToList();
        }
        if (ToPriceFilter != 0)
        {
            filteredAutos = filteredAutos.Where(x => x.Price <= ToPriceFilter).ToList();
        }
        if (SelectedAutoMark != null)
        {
            filteredAutos = filteredAutos.Where(x => x.Type.Mark.Id == SelectedAutoMark.Id).ToList();
        }
        if (SelectedAutoType != null)
        {
            filteredAutos = filteredAutos.Where(x => x.Type.Id == SelectedAutoType.Id).ToList();
        }
        Autos = new ObservableCollection<AutoModel>(filteredAutos);
    }
    private void CmdResetFilterHandler()
    {
        SelectedAutoMark = null;
        SelectedAutoType = null;
        IsAutoModelEnabled = false;
        CmdConfirmFilterHandler();
    }

    public async Task Init()
    {
        AllAutos = await _owner.GetAutoModels();
        FromPriceFilter = AllAutos.Min(x => x.Price);
        ToPriceFilter = AllAutos.Max(x => x.Price);
        Autos = await _owner.GetAutoModels();
        var autoTypes = await "https://localhost:7258/AutoType/getAll".GetQuery<AutoType>();
        AllAutoMarks = (await "https://localhost:7258/CarMake/getAll".GetQuery<CarMake>()).ToList();
        var autoTypeModels = new List<AutoTypeModel>();
        foreach (var autoType in autoTypes)
        {
            autoTypeModels.Add(new AutoTypeModel()
            {
                Id = autoType.Id,
                Mark = AllAutoMarks.First(x => x.Id == autoType.Id),
                Model = autoType.Model
            });
        }
        AllAutoTypes = autoTypeModels.ToList();
        OnPropertyChanged(nameof(AllAutoTypes));
        OnPropertyChanged(nameof(AllAutoMarks));
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