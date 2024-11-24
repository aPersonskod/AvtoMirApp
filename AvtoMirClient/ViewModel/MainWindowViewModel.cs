using System;
using System.Threading.Tasks;
using AvtoMirClient.Interfaces;
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