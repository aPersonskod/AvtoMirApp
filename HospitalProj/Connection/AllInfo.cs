using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using HospitalProj.Models;
using HospitalProj.ViewModel;

namespace HospitalProj.Connection;

public class AllInfo
{
    public List<Service> Services { get; private set; } = new List<Service>();
    public List<Specialist> Specialists { get; private set; } = new List<Specialist>();
    public List<Patient> Patients { get; private set; } = new List<Patient>();
    public List<Questionnaire> Questionnaires { get; private set; } = new List<Questionnaire>();
    public List<MeetingInfo> MeetingInfos { get; private set; } = new List<MeetingInfo>();
    public List<Recording> Recordings { get; private set; } = new List<Recording>();

    private static AllInfo _instance = null;
    public static AllInfo Instance => _instance;
    public static void Init()
    {
        _instance = new AllInfo();
    }
    private AllInfo()
    {
        Refresh();
    }

    public void Refresh()
    {
        Services = "SELECT * FROM Услуги".DoSqlCommand(3).Select(x => new Service()
        {
            Id = (int)x[0],
            ServiceName = (string)x[1],
            Price = (int)x[2]
        }).ToList();
        Specialists = "SELECT * FROM Специалист".DoSqlCommand(4).Select(x => new Specialist()
        {
            Id = (int)x[0],
            Fio = (string)x[1],
            Mobile = (string)x[2],
            JobTitle = (string)x[3],
        }).ToList();
        Patients = "SELECT * FROM Пациент".DoSqlCommand(8)?.Select(x => new Patient()
        {
            Id = (int)x[0],
            Fio = (string)x[1],
            Sex = (string)x[2],
            Gmail = (string)x[3],
            Status = (string)x[4],
            From = (string)x[5],
            BirthDay = (DateTime)x[6],
            Mobile = (string)x[7],
        }).ToList();
        Questionnaires = "SELECT * FROM Анкета".DoSqlCommand(10).Select(x => new Questionnaire()
        {
            Id = (int)x[0],
            Jaloby = (string)x[1],
            Problems = (string)x[2],
            TherapyTarget = (string)x[3],
            Request = (string)x[4],
            Obstacles = (string)x[5],
            Values = (string)x[6],
            Pursuit = (string)x[7],
            LifeTargets = (string)x[8],
            Patient = Patients.First(p => p.Id == (int)x[9]),
        }).ToList();
        Recordings = "SELECT * FROM Запись".DoSqlCommand(6).Select(x => new Recording()
        {
            Id = (int)x[0],
            PlanDate = (DateTime)x[1],
            Patient = Patients.First(p => p.Id == (int)x[2]),
            Specialist = Specialists.First(p => p.Id == (int)x[3]),
            Service = Services.First(p => p.Id == (int)x[4]),
            Format = (bool)x[5],
        }).ToList();
        MeetingInfos = "SELECT * FROM Информация_о_встрече".DoSqlCommand(10).Select(x => new MeetingInfo()
        {
            Id = (int)x[0],
            Feelings = (string)x[1],
            Symptoms = (string)x[2],
            Intervents = (string)x[3],
            Quotes = (string)x[4],
            HomeWork = (string)x[5],
            FeedBack = (string)x[6],
            NextTime = (string)x[7],
            Impression = (string)x[8],
            Recording = Recordings.First(p => p.Id == (int)x[9]),
        }).ToList();
    }
}

public static class Ext
{
    public static int GetNewId(this IEnumerable<IdElem> collection)
    {
        var lastItem = collection?.OrderBy(x => x.Id).LastOrDefault();
        return lastItem == null ? 1 : lastItem.Id + 1;
    }

    public static void Navigate(this ViewModelBase vm)
    {
        NavigationService.GetInstance().Navigate(vm);
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

    public void Navigate(ViewModelBase vm)
    {
        _mainViewModel.CurrentVM = vm;
    }
}