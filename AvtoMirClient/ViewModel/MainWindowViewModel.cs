using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AvtoMirClient.Extensions;
using AvtoMirClient.Interfaces;
using AvtoMirModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AvtoMirClient.ViewModel;

public class MainWindowViewModel : ObservableObject
{
    public MainWindowViewModel()
    {
        NavigationService.Init(this);
    }
    private object _currentVm;
    public object CurrentVM
    {
        get => _currentVm;
        set => SetProperty(ref _currentVm, value);
    }
    
    internal async Task<ObservableCollection<AutoModel>> GetAutoModels()
    {
        var autos = await "https://localhost:7258/Auto/getAll".GetQuery<Auto>();
        var autoTypes = await "https://localhost:7258/AutoType/getAll".GetQuery<AutoType>();
        var carMakes = await "https://localhost:7258/CarMake/getAll".GetQuery<CarMake>();
        var autoTypeModels = new List<AutoTypeModel>();
        foreach (var autoType in autoTypes)
        {
            autoTypeModels.Add(new AutoTypeModel()
            {
                Id = autoType.Id,
                Mark = carMakes.First(x => x.Id == autoType.Id),
                Model = autoType.Model
            });
        }
        var autoModels = new List<AutoModel>();
        foreach (var auto in autos)
        {
            autoModels.Add(new AutoModel()
            {
                Id = auto.Id,
                RegNumber = auto.RegNumber,
                VinNumber = auto.VinNumber,
                CreationYear = auto.CreationYear,
                Price = auto.Price,
                Color = auto.Color,
                Type = autoTypeModels.First(x => x.Id == auto.IdType),
                Image = auto.Image
            });
        }
        return new ObservableCollection<AutoModel>(autoModels);
    }
}

public class NavigationService
{
    private static NavigationService instance = null;
    private MainWindowViewModel _mainViewModel;
    public static NavigationService GetInstance() => instance ?? throw new Exception("navigation service is not inited");
    public static void Init(MainWindowViewModel vm)
    {
        instance = new NavigationService(vm);
    }

    private NavigationService(MainWindowViewModel vm)
    {
        _mainViewModel = vm;
        _mainViewModel.CurrentVM = new MainViewModel(_mainViewModel);
    }

    public async Task Navigate(IInitable vm)
    {
        _mainViewModel.CurrentVM = vm;
        await vm.Init();
    }
}