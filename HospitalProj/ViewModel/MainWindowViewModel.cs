﻿using System;
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
                e.Message.Show("Ошибка");
                InstallDriver();
                throw;
            }
        }
        private void InstallDriver()
        {
            Process.Start("msoledbsql.msi");
        }
    }
}