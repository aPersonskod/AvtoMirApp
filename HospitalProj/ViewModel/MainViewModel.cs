using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using HospitalProj.Connection;
using HospitalProj.Enums;
using HospitalProj.Export;
using HospitalProj.Models;
using Microsoft.Win32;

namespace HospitalProj.ViewModel;

public interface ICmdBack
{

}

public static class VmExt
{
    public static CmdBackViewModel SetCmdNavigate(this CmdBackViewModel vm, Action handler)
    {
        vm.CmdNavigateBack = new RelayCommand(handler);
        return vm;
    }
}

public class CmdBackViewModel : ViewModelBase
{
    public ICommand CmdNavigateBack { get; set; }
}

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
    public ICommand CmdDeleteSchedule { get; }
    public ICommand CmdNavigatePatientCard { get; }
    public ObservableCollection<Recording> DiagPatients { get; }
    public ObservableCollection<Recording> TherapyPatients { get; }
    public ObservableCollection<Recording> PausedPatients { get; }
    public ObservableCollection<Recording> EndPatients { get; }
    public ScheduleViewModel(MainWindowViewModel owner)
    {
        _owner = owner;
        _owner.HeaderText = "Расписание";
        DiagPatients = new(AllInfo.Instance.Recordings.Where(x => x.Patient.Status == "Диагностическая"));
        TherapyPatients = new(AllInfo.Instance.Recordings.Where(x => x.Patient.Status == "Терапия"));      
        PausedPatients = new(AllInfo.Instance.Recordings.Where(x => x.Patient.Status == "На паузе"));      
        EndPatients = new(AllInfo.Instance.Recordings.Where(x => x.Patient.Status == "Закончили"));  
        CmdNavigateBack = new RelayCommand(() =>  new MainViewModel(_owner).Navigate());
        CmdCreateSchedule = new RelayCommand(() =>  new NewScheduleViewModel(_owner).Navigate());
        CmdUpdateSchedule = new RelayCommand<Recording>(r => new NewScheduleViewModel(_owner, r).Navigate());
        CmdNavigatePatientCard = new RelayCommand<Recording>(CmdNavigatePatientCardHandler);
        CmdDeleteSchedule = new RelayCommand<Recording>(CmdDeleteRecording);
    }

    private void CmdNavigatePatientCardHandler(Recording r)
    {
        new PatientCardViewModel(_owner, r).SetCmdNavigate(this.Navigate).Navigate();
    }

    private void CmdDeleteRecording(Recording r)
    {
        $"DELETE FROM Запись WHERE Код_записи = {r.Id}".DoSqlCommand();
        new ScheduleViewModel(_owner).Navigate();
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
            if(Recording.PlanDate==null || Recording.Specialist==null || Recording.Patient==null || Recording.Service==null)return;
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
    public ICommand CmdNewPatient { get; }
    public PatientsViewModel(MainWindowViewModel owner)
    {
        _owner = owner;
        _owner.HeaderText = "Пациенты";
        CmdNavigateBack = new RelayCommand(() => new MainViewModel(_owner).Navigate());
        CmdNewPatient = new RelayCommand<Patient>((p) => new NewPatientViewModel(_owner, p).SetCmdNavigate(this.Navigate).Navigate());
    }
}
public class NewPatientViewModel : CmdBackViewModel
{
    private readonly MainWindowViewModel _owner;
    private readonly bool _needToCreate;
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
        if (patient != null)
            Questionnaire = AllInfo.Instance.Questionnaires.First(x => x.Patient.Id == Patient.Id);
        CmdSave = new RelayCommand(CmdSaveHandler);
        //CmdNavigateBack = new RelayCommand(() => new PatientsViewModel(_owner).Navigate());
    }
    private void CmdSaveHandler()
    {
        var patientId = CreatePatient();
        CreateQuestionnaire(patientId);
        CmdNavigateBack.Execute(null);
    }

    private int CreatePatient()
    {
        var query = "";
        if (!_needToCreate)
        {
            //update
            query = $"UPDATE Пациент SET ФИО='{Patient.Fio}', "
                    + $"Пол='{Patient.Sex}', Почта='{Patient.Gmail}', "
                    + $"Статус='{Patient.Status}', Откуда='{Patient.From}', Дата_рождения='{Patient.BirthDay.ToUniversalTime()}', Номер_телефона='{Patient.Mobile}' "
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
            query = $"UPDATE Анкета SET Жалобы='{Questionnaire.Jaloby}', Проблема='{Questionnaire.Problems}', "
                    + $"Цели_терапии='{Questionnaire.TherapyTarget}', Запрос='{Questionnaire.Request}', "
                    + $"Препятсвия='{Questionnaire.Obstacles}', Ценности='{Questionnaire.Values}', Стремления='{Questionnaire.Pursuit}', "
                    + $"Цели='{Questionnaire.LifeTargets}', Код_пациента='{patientId}' "
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
public class PatientCardViewModel : CmdBackViewModel
{
    private readonly MainWindowViewModel _owner;
    public ICommand CmdChangeClient { get; }
    public Recording Recording { get; }
    public MeetingInfo MeetingInfo { get; } = new MeetingInfo();

    #region Meetings

    private MeetingInfo _selectedMeetingInfo = new MeetingInfo();
    public MeetingInfo SelectedMeetingInfo
    {
        get => _selectedMeetingInfo;
        set => Set(() => SelectedMeetingInfo, ref _selectedMeetingInfo, value);
    }

    public ICommand CmdCreateMeeting { get; }
    private void CmdCreateMeetingHandler()
    {
        var query = "";
        if (SelectedMeetingInfo.Id != 0)
        {
            //update
            query = $"UPDATE Анкета SET Самочуствие='{SelectedMeetingInfo.Feelings}', Симптомы='{SelectedMeetingInfo.Symptoms}', "
                    + $"Интервенции='{SelectedMeetingInfo.Intervents}', Цитаты='{SelectedMeetingInfo.Quotes}', "
                    + $"ДЗ='{SelectedMeetingInfo.HomeWork}', Обратная_связь='{SelectedMeetingInfo.FeedBack}', На_следующий_раз='{SelectedMeetingInfo.NextTime}', "
                    + $"Впечатления='{SelectedMeetingInfo.Impression}', Код_записи='{Recording.Id}' "
                    + $"WHERE Код_встречи={SelectedMeetingInfo.Id}";
            query.DoSqlCommand();
        }
        //create
        if (SelectedMeetingInfo.Id == 0)
        {
            var newId = AllInfo.Instance.MeetingInfos.GetNewId();
            query = $"INSERT INTO Информация_о_встрече (Код_встречи, Самочуствие, Симптомы, Интервенции, Цитаты, ДЗ, Обратная_связь, На_следующий_раз, Впечатления, Код_записи) "
                    + $"VALUES ({newId}, '{SelectedMeetingInfo.Feelings}', '{SelectedMeetingInfo.Symptoms}', '{SelectedMeetingInfo.Intervents}', '{SelectedMeetingInfo.Quotes}', '{SelectedMeetingInfo.HomeWork}', '{SelectedMeetingInfo.FeedBack}', '{SelectedMeetingInfo.NextTime}', '{SelectedMeetingInfo.Impression}', {Recording.Id});";
            query.DoSqlCommand();
        }
    }

    #endregion

    #region Report
    
    public ICommand CmdCreateReport { get; }
    private void CreateReportHandler()
    {
        var meetings = AllInfo.Instance.MeetingInfos.Where(x => x.Recording.PlanDate > StartDate && x.Recording.PlanDate < EndDate);
        var data = new List<List<string>>();
        var header = new List<string>();
        if(Feelings)   header.Add("Самочувствие с момента последней сессии/что изменилось");
        if(Symptoms)   header.Add("Беспокоящие симптомы/поведение");
        if(Intervents) header.Add("Интервенции на сессии");
        if(Quotes)     header.Add("Важные цитаты");
        if(HomeWork)   header.Add("Домашнее задание + прогноз выполнения %");
        if(FeedBack)   header.Add("Обратная связь в конце сессии");
        if(NextTime)   header.Add("На следующий раз");
        if(Impression) header.Add("Впечатление от клиента");
        header.Add("Дата встречи");
        data.Add(header);
        foreach (var meetingInfo in meetings)
        {
            var line = new List<string>();
            if(Feelings)   line.Add(meetingInfo.Feelings);
            if(Symptoms)   line.Add(meetingInfo.Symptoms);
            if(Intervents) line.Add(meetingInfo.Intervents);
            if(Quotes)     line.Add(meetingInfo.Quotes);
            if(HomeWork)   line.Add(meetingInfo.HomeWork);
            if(FeedBack)   line.Add(meetingInfo.FeedBack);
            if(NextTime)   line.Add(meetingInfo.NextTime);
            if(Impression) line.Add(meetingInfo.Impression);
            line.Add(meetingInfo.Recording.PlanDate.ToString("dd.MM.yyyy"));
            data.Add(line);
        }
        
        if(data.Count < 2) return;
        var fd = new OpenFileDialog();
        fd.Multiselect = false;
        fd.Filter = "Word файлы (*.docx)|*.docx|Все файлы (*.*)|*.*";
        fd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        var result = "";
        if (fd.ShowDialog() == true)
        {
            result = fd.FileName;
        }
        if(string.IsNullOrEmpty(result)) return;
        new Word().GenerateWordExportFile(data, result);
        Process.Start(new ProcessStartInfo(result) { UseShellExecute = true });
    }

    private DateTime _startDate = DateTime.Today;
    private DateTime _endtDate = DateTime.Today;
    public DateTime StartDate
    {
        get => _startDate;
        set => Set(() => StartDate, ref _startDate, value);
    }
    
    public DateTime EndDate
    {
        get => _endtDate;
        set => Set(() => EndDate, ref _endtDate, value);
    }
    private bool feelings;
    private bool symptoms;
    private bool intervents;
    private bool quotes;
    private bool homeWork;
    private bool feedBack;
    private bool nextTime;
    private bool impression;
    public bool Feelings
    {
        get => feelings;
        set => Set(() => Feelings, ref feelings, value);
    }
    public bool Symptoms
    {
        get => symptoms;
        set => Set(() => Symptoms, ref symptoms, value);
    }
    public bool Intervents
    {
        get => intervents;
        set => Set(() => Intervents, ref intervents, value);
    }
    public bool Quotes
    {
        get => quotes;
        set => Set(() => Quotes, ref quotes, value);
    }
    public bool HomeWork
    {
        get => homeWork;
        set => Set(() => HomeWork, ref homeWork, value);
    }
    public bool FeedBack
    {
        get => feedBack;
        set => Set(() => FeedBack, ref feedBack, value);
    }
    public bool NextTime
    {
        get => nextTime;
        set => Set(() => NextTime, ref nextTime, value);
    }
    public bool Impression
    {
        get => impression;
        set => Set(() => Impression, ref impression, value);
    }

    #endregion
    public Questionnaire Questionnaire { get; } = new Questionnaire();

    public PatientCardViewModel(MainWindowViewModel owner, Recording recording = null)
    {
        _owner = owner;
        _owner.HeaderText = "Карточка пациента";
        Recording = recording ?? new Recording();
        if(recording is not null)
        {
            MeetingInfo = AllInfo.Instance.MeetingInfos?.FirstOrDefault(x => x.Recording.Id == Recording.Id);
            Questionnaire = AllInfo.Instance.Questionnaires?.FirstOrDefault(x => x.Patient.Id == Recording.Patient.Id);
        }
        CmdNavigateBack = new RelayCommand(() => new PatientsViewModel(_owner).Navigate());
        CmdChangeClient = new RelayCommand(CmdChangeClientHandler);
        CmdCreateReport = new RelayCommand(CreateReportHandler);
        CmdCreateMeeting = new RelayCommand(CmdCreateMeetingHandler);
    }

    private void CmdChangeClientHandler()
    {
        new NewPatientViewModel(_owner, Recording.Patient).SetCmdNavigate(this.Navigate).Navigate();
    }
}

