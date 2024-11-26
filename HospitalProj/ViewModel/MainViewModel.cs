using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Navigation;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using HospitalProj.Connection;
using HospitalProj.Models;
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
        _owner.HeaderText = "Главная страница";
        CmdNavigateSchedule = new RelayCommand(async () => await new ScheduleViewModel(_owner).Navigate());
        CmdNavigateServices = new RelayCommand(async () => await new ServicesViewModel(_owner).Navigate());
        CmdNavigatePatients = new RelayCommand(async () => await new PatientsViewModel(_owner).Navigate());
    }

}

public class ScheduleViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _owner;
    public ICommand CmdNavigateBack { get; }
    public ICommand CmdCreateSchedule { get; }
    public ICommand CmdUpdateSchedule { get; }
    public ScheduleViewModel(MainWindowViewModel owner)
    {
        _owner = owner;
        _owner.HeaderText = "Расписание";
        CmdNavigateBack = new RelayCommand(async () => await new MainViewModel(_owner).Navigate());
        CmdCreateSchedule = new RelayCommand(async () => await new NewScheduleViewModel(_owner).Navigate());
        CmdUpdateSchedule = new RelayCommand<Recording>(async (r) => await new NewScheduleViewModel(_owner, r).Navigate());
    }

}

public class NewScheduleViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _owner;
    public ICommand CmdNavigateBack { get; }
    public ICommand CmdSave { get; }
    public Recording Recording { get; }
    public NewScheduleViewModel(MainWindowViewModel owner, Recording? recording = null)
    {
        _owner = owner;
        _owner.HeaderText = "Новая запись";
        if (recording != null) Recording = recording;
        CmdNavigateBack = new RelayCommand(async () => await new ScheduleViewModel(_owner).Navigate());
        CmdSave = new RelayCommand(async () =>
        {
            //todo save info
            var query = "";
            if (recording != null)
            {
                //update
                query = $"UPDATE Запись SET ДатаИВремяПроведения='{Recording.PlanDate.ToString("dd.MM.yyyy")}', "
                    + $"КодПациент={Recording.Patient.Id}, КодСпециалиста={Recording.Specialist.Id}, "
                    + $"Формат='{Recording.Format}', КодУслуги={Recording.Service.Id} "
                    + $"WHERE КодЗаписи={Recording.Id}";
                query.DoSqlCommand();
            }
            //create
            if (recording == null)
            {
                var newId = AllInfo.Instance.Recordings.GetNewId();
                query = $"INSERT INTO Запись (КодЗаписи, ДатаИВремяПроведения, КодПациент, КодСпециалиста, Формат, КодУслуги) "
                        + $"VALUES ({newId}, '{Recording.PlanDate.ToString("dd.MM.yyyy")}', {Recording.Patient.Id}, {Recording.Specialist.Id}, '{Recording.Format}', {Recording.Service.Id})";
                query.DoSqlCommand();
            }
            await new ScheduleViewModel(_owner).Navigate();
        });
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
        _owner.HeaderText = "Услуги";
        CmdNavigateBack = new RelayCommand(async () => await new MainViewModel(_owner).Navigate());
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
        _owner.HeaderText = "Пациенты";
        CmdNavigateBack = new RelayCommand(async () => await new MainViewModel(_owner).Navigate());
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

