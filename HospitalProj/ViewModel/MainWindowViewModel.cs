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
            AllInfo.Init();
            NavigationService.Init(this);
        }
    }
}