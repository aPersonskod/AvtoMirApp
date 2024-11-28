using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using HospitalProj.Connection;
using HospitalProj.Enums;
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
            query = $"UPDATE Запись SET Дата_и_время_проведения='{Recording.PlanDate.ToUniversalTime()}', "
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
                    + $"VALUES ({newId}, '{Recording.PlanDate.ToUniversalTime()}', {Recording.Patient.Id}, {Recording.Specialist.Id}, '{Recording.Format}', {Recording.Service.Id});";
            query.DoSqlCommand();
        }
        new ScheduleViewModel(_owner).Navigate();
    }
}
public class ServicesViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _owner;
    private readonly bool _needToCreate;
    public ICommand CmdNavigateBack { get; }
    public ICommand CmdSave { get; }
    public Service Service { get; }
    public ServicesViewModel(MainWindowViewModel owner, Service service = null)
    {
        _owner = owner;
        _owner.HeaderText = "Услуги";
        if (service == null) _needToCreate = true;
        Service = service ?? new Service();
        CmdSave = new RelayCommand(CmdSaveHandler);
        CmdNavigateBack = new RelayCommand(() => new MainViewModel(_owner).Navigate());
    }
    private void CmdSaveHandler()
    {
        var query = "";
        if (!_needToCreate)
        {
            //update
            query = $"UPDATE Услуги SET Название_услуги='{Service.ServiceName}', "
                    + $"Стоимость={Service.Price} "
                    + $"WHERE Код_услуги={Service.Id}";
            query.DoSqlCommand();
        }
        //create
        if (_needToCreate)
        {
            var newId = AllInfo.Instance.Services.GetNewId();
            query = $"INSERT INTO Услуги (Код_услуги, Название_услуги, Стоимость) "
                    + $"VALUES ({newId}, '{Service.ServiceName}', {Service.Price});";
            query.DoSqlCommand();
        }
        new ScheduleViewModel(_owner).Navigate();
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
    public ICommand CmdNavigatePatientCard { get; }
    public ICommand CmdNewPatient { get; }
    public ObservableCollection<Recording> DiagPatients { get; }
    public ObservableCollection<Recording> TherapyPatients { get; }
    public ObservableCollection<Recording> PausedPatients { get; }
    public ObservableCollection<Recording> EndPatients { get; }
    public PatientsViewModel(MainWindowViewModel owner)
    {
        _owner = owner;
        _owner.HeaderText = "Пациенты";
        DiagPatients = new(AllInfo.Instance.Recordings.Where(x => x.Patient.Status == "Диагностическая"));
        TherapyPatients = new(AllInfo.Instance.Recordings.Where(x => x.Patient.Status == "Терапия"));      
        PausedPatients = new(AllInfo.Instance.Recordings.Where(x => x.Patient.Status == "На паузе"));      
        EndPatients = new(AllInfo.Instance.Recordings.Where(x => x.Patient.Status == "Закончили"));        
        CmdNavigateBack = new RelayCommand(() => new MainViewModel(_owner).Navigate());
        CmdNavigatePatientCard = new RelayCommand<Recording>(r => new PatientCardViewModel(_owner, r).Navigate());
        CmdNewPatient = new RelayCommand(() => new NewPatientViewModel(_owner).Navigate());
    }
}
public class NewPatientViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _owner;
    private readonly bool _needToCreate;
    public ICommand CmdNavigateBack { get; }
    public ICommand CmdSave { get; }
    public Patient Patient { get; }
    public Questionnaire Questionnaire { get; } = new Questionnaire();
    private Sex _selectedSex = Sex.M;
    public Sex SelectedSex
    {
        get => _selectedSex;
        set
        {
            Set(() => SelectedSex, ref _selectedSex, value);
            Patient.Sex = value switch
            {
                Sex.M => "M",
                Sex.F => "Ж"
            };
        }
    }
    private PatientStatus _selectedStatus = PatientStatus.Diag;
    public PatientStatus SelectedStatus
    {
        get => _selectedStatus;
        set
        {
            Set(() => SelectedStatus, ref _selectedStatus, value);
            Patient.Status = value switch
            {
                PatientStatus.Diag => "Диагностическая",
                PatientStatus.Therapy => "Терапия",
                PatientStatus.Paused => "На паузе",
                PatientStatus.Ended => "Закончили"
            };
        }
    }
    public NewPatientViewModel(MainWindowViewModel owner, Patient patient = null)
    {
        _owner = owner;
        _owner.HeaderText = "Новый пациент";
        if (patient == null) _needToCreate = true;
        Patient = patient ?? new Patient();
        CmdSave = new RelayCommand(CmdSaveHandler);
        CmdNavigateBack = new RelayCommand(() => new PatientsViewModel(_owner).Navigate());
    }
    private void CmdSaveHandler()
    {
        var patientId = CreatePatient();
        CreateQuestionnaire(patientId);
        new PatientsViewModel(_owner).Navigate();
    }

    private int CreatePatient()
    {
        var query = "";
        if (!_needToCreate)
        {
            //update
            query = $"UPDATE Пациент SET ФИО='{Patient.Fio}', "
                    + $"Пол='{Patient.Sex}', Почта='{Patient.Gmail}', "
                    + $"Статус='{Patient.Status}', Откуда='{Patient.From}', Дата_рождения='{Patient.BirthDay.ToUniversalTime()}', Номер_телефона='{Patient.Mobile}'"
                    + $"WHERE Код_пациента={Patient.Id}";
            query.DoSqlCommand();
            return Patient.Id;
        }
        //create
        if (_needToCreate)
        {
            var newId = AllInfo.Instance.Patients.GetNewId();
            query = $"INSERT INTO Пациент (Код_пациента, ФИО, Пол, Почта, Статус, Откуда, Дата_рождения, Номер_телефона) "
                    + $"VALUES ({newId}, '{Patient.Fio}', '{Patient.Sex}', '{Patient.Gmail}', '{Patient.Status}', '{Patient.From}', '{Patient.BirthDay.ToUniversalTime()}', '{Patient.Mobile}');";
            query.DoSqlCommand();
            return newId;
        }
        throw new Exception("this is wrong way to create patient");
    }

    private void CreateQuestionnaire(int patientId)
    {
        var query = "";
        if (!_needToCreate)
        {
            //update
            query = $"UPDATE Анкета SET Жалобы='{Questionnaire.Jaloby}', Проблема='{Questionnaire.Problems}' "
                    + $"Цели_терапии='{Questionnaire.TherapyTarget}', Запрос='{Questionnaire.Request}', "
                    + $"Препятсвия='{Questionnaire.Obstacles}', Ценности='{Questionnaire.Values}', Стремления='{Questionnaire.Pursuit}', "
                    + $"Цели='{Questionnaire.LifeTargets}, Код_пациента={patientId} "
                    + $"WHERE Код_анкеты={Questionnaire.Id}";
            query.DoSqlCommand();
        }
        //create
        if (_needToCreate)
        {
            var newId = AllInfo.Instance.Questionnaires.GetNewId();
            query = $"INSERT INTO Анкета (Код_анкеты, Жалобы, Проблема, Цели_терапии, Запрос, Препятсвия, Ценности, Стремления, Цели, Код_пациента) "
                    + $"VALUES ({newId}, '{Questionnaire.Jaloby}', '{Questionnaire.Problems}', '{Questionnaire.TherapyTarget}', '{Questionnaire.Request}', '{Questionnaire.Obstacles}', '{Questionnaire.Values}', '{Questionnaire.Pursuit}', '{Questionnaire.LifeTargets}', {patientId});";
            query.DoSqlCommand();
        }
    }
}
public class PatientCardViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _owner;
    public ICommand CmdNavigateBack { get; }
    public PatientCardViewModel(MainWindowViewModel owner, Recording recording = null)
    {
        _owner = owner;
        _owner.HeaderText = "Карточка пациента";
        CmdNavigateBack = new RelayCommand(() => new MainViewModel(_owner).Navigate());
    }
}

