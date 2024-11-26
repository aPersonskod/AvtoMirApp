using System;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using HospitalProj.Connection;
using HospitalProj.Models;

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
        CmdNavigateSchedule = new RelayCommand(() => new ScheduleViewModel(_owner).Navigate());
        CmdNavigateServices = new RelayCommand(() => new ServicesViewModel(_owner).Navigate());
        CmdNavigatePatients = new RelayCommand(() => new PatientsViewModel(_owner).Navigate());
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
        CmdNavigateBack = new RelayCommand(() =>  new MainViewModel(_owner).Navigate());
        CmdCreateSchedule = new RelayCommand(() =>  new NewScheduleViewModel(_owner).Navigate());
        CmdUpdateSchedule = new RelayCommand<Recording>(r => new NewScheduleViewModel(_owner, r).Navigate());
    }

}

public class NewScheduleViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _owner;
    private bool _needToCreate;
    public ICommand CmdNavigateBack { get; }
    public ICommand CmdSave { get; }
    public Recording Recording { get; }
    public NewScheduleViewModel(MainWindowViewModel owner, Recording? recording = null)
    {
        _owner = owner;
        _owner.HeaderText = "Новая запись";
        CmdNavigateBack = new RelayCommand(CmdNavigateBackHandler);
        if (recording == null) _needToCreate = true;
        Recording = recording ?? new Recording(){PlanDate = DateTime.Now};
        CmdSave = new RelayCommand(CmdSaveHandler);
    }
    
    private void CmdNavigateBackHandler() => new ScheduleViewModel(_owner).Navigate();
    private void CmdSaveHandler()
    {
        var query = "";
        if (!_needToCreate)
        {
            //update
            query = $"UPDATE Запись SET Дата_и_время_проведения='{Recording.PlanDate.ToString("dd.MM.yyyy")}', "
                    + $"Код_пациента={Recording.Patient.Id}, Код_специалиста={Recording.Specialist.Id}, "
                    + $"Формат='{Recording.Format}', Код_услуги={Recording.Service.Id} "
                    + $"WHERE Код_записи={Recording.Id}";
            query.DoSqlCommand();
        }
        //create
        if (_needToCreate)
        {
            var newId = AllInfo.Instance.Recordings.GetNewId();
            query = $"INSERT INTO Запись (Код_записи, Дата_и_время_проведения, Код_пациента, Код_специалиста, Формат, Код_услуги) "
                    + $"VALUES ({newId}, '{Recording.PlanDate.ToString("dd.MM.yyyy HH:mm:ss")}', {Recording.Patient.Id}, {Recording.Specialist.Id}, '{Recording.Format}', {Recording.Service.Id});";
            query.DoSqlCommand();
        }
        new ScheduleViewModel(_owner).Navigate();
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
        CmdNavigateBack = new RelayCommand(() => new MainViewModel(_owner).Navigate());
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
        CmdNavigateBack = new RelayCommand(() => new MainViewModel(_owner).Navigate());
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

