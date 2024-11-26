using System;
using System.Diagnostics;
using GalaSoft.MvvmLight;
using HospitalProj.Connection;

namespace HospitalProj.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ViewModelBase _currentVM;
        public ViewModelBase CurrentVM
        {
            get => _currentVM;
            set => Set(() => CurrentVM, ref _currentVM, value);
        }

        private string _headerText = "Главная страница";
        public string HeaderText
        {
            get => _headerText;
            set => Set(() => HeaderText, ref _headerText, value);
        }
        public MainWindowViewModel()
        {
            Test();
            AllInfo.Init();
            NavigationService.Init(this);
        }

        private void Test()
        {
            try
            {
                var testData = "SELECT * FROM Услуги".DoSqlCommand(3);
            }
            catch (Exception e)
            {
                //todo uncomment it !!!
                /*e.Message.Show("Ошибка");
                InstallDriver();*/
                throw;
            }
        }
        private void InstallDriver()
        {
            Process.Start("msoledbsql.msi");
        }
    }
}