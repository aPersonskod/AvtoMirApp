using System;
using System.Collections.Generic;
using System.Linq;
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
        Patients = "SELECT * FROM Пациент".DoSqlCommand(8).Select(x => new Patient()
        {
            Id = (int)x[0],
            Fio = (string)x[1],
            Sex = (string)x[2],
            BirthDay = (DateTime)x[3],
            Mobile = (string)x[4],
            Gmail = (string)x[5],
            Status = (string)x[6],
            From = (string)x[7],
        }).ToList();
        Questionnaires = "SELECT * FROM Анкета".DoSqlCommand(10).Select(x => new Questionnaire()
        {
            Id = (int)x[0],
            Patient = Patients.First(p => p.Id == (int)x[1]),
            Jaloby = (string)x[2],
            Problems = (string)x[3],
            TherapyTarget = (string)x[4],
            Request = (string)x[5],
            Obstacles = (string)x[6],
            Values = (string)x[7],
            Pursuit = (string)x[8],
            LifeTargets = (string)x[9],
        }).ToList();
        Recordings = "SELECT * FROM Запись".DoSqlCommand(6).Select(x => new Recording()
        {
            Id = (int)x[0],
            PlanDate = (DateTime)x[1],
            Patient = Patients.First(p => p.Id == (int)x[2]),
            Specialist = Specialists.First(p => p.Id == (int)x[3]),
            Format = (bool)x[4],
            Service = Services.First(p => p.Id == (int)x[5]),
        }).ToList();
        MeetingInfos = "SELECT * FROM ИнформацияОВстрече".DoSqlCommand(10).Select(x => new MeetingInfo()
        {
            Id = (int)x[0],
            Recording = Recordings.First(p => p.Id == (int)x[1]),
            Feelings = (string)x[2],
            Symptoms = (string)x[3],
            Intervents = (string)x[4],
            Quotes = (string)x[5],
            HomeWork = (string)x[6],
            FeedBack = (string)x[7],
            NextTime = (string)x[8],
            Impression = (string)x[9],
        }).ToList();
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