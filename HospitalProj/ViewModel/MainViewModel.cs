using System.Windows.Input;
using System.Windows.Navigation;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using NavigationService = HospitalProj.Connection.NavigationService;

namespace HospitalProj.ViewModel;

public class MainViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _owner;
    public ICommand CmdNavigateSchedule { get; }
    public ICommand CmdNavigateServices { get; }
    public ICommand CmdNavigatePatients { get; }

    public MainViewModel(MainWindowViewModel owner)
    {
        _owner = owner;
        CmdNavigateSchedule = new RelayCommand(() =>
        {
            NavigationService.GetInstance().Navigate(new ScheduleViewModel(_owner));
        });
        CmdNavigateServices = new RelayCommand(() =>
        {
            NavigationService.GetInstance().Navigate(new ServicesViewModel(_owner));
        });
        CmdNavigatePatients = new RelayCommand(() =>
        {
            NavigationService.GetInstance().Navigate(new PatientsViewModel(_owner));
        });
    }

}

public class ScheduleViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _owner;
    public ICommand CmdNavigateBack { get; }
    public ScheduleViewModel(MainWindowViewModel owner)
    {
        _owner = owner;
        CmdNavigateBack = new RelayCommand(() =>
        {
            NavigationService.GetInstance().Navigate(new MainViewModel(_owner));
        });
    }

}

public class NewScheduleViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _owner;
    public NewScheduleViewModel(MainWindowViewModel owner)
    {
        _owner = owner;
    }
}

public class NewPatientViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _owner;
    public NewPatientViewModel(MainWindowViewModel owner)
    {
        _owner = owner;
    }
}
public class ServicesViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _owner;
    public ICommand CmdNavigateBack { get; }
    public ServicesViewModel(MainWindowViewModel owner)
    {
        _owner = owner;
        CmdNavigateBack = new RelayCommand(() =>
        {
            NavigationService.GetInstance().Navigate(new MainViewModel(_owner));
        });
    }
}
public class NewServiceViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _owner;
    public NewServiceViewModel(MainWindowViewModel owner)
    {
        _owner = owner;
    }
}
public class PatientsViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _owner;
    public ICommand CmdNavigateBack { get; }
    public PatientsViewModel(MainWindowViewModel owner)
    {
        _owner = owner;
        CmdNavigateBack = new RelayCommand(() =>
        {
            NavigationService.GetInstance().Navigate(new MainViewModel(_owner));
        });
    }
}
public class PatientCardViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _owner;
    public PatientCardViewModel(MainWindowViewModel owner)
    {
        _owner = owner;
    }
}

