﻿using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HospitalProj.Connection;

namespace HospitalProj
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var currentFolder = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location) + @"\";
            DbConnection.CurrentFolder = currentFolder;

            try
            {
                var testData = "SELECT * FROM Услуги".DoSqlCommand(3);
                testData.First()[1].ToString().Show();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                InstallDriver();
            }
        }

        private void InstallDriver()
        {
            Process.Start(DbConnection.CurrentFolder + "msoledbsql.msi");
        }
    }
}