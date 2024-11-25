using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using AvtoMirClient.Export;
using AvtoMirClient.Extensions;
using AvtoMirClient.Interfaces;
using AvtoMirModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;

namespace AvtoMirClient.ViewModel;

public class OrdersViewModel : ObservableObject, IInitable
{
    private readonly MainWindowViewModel _owner;
    private List<Auto> _autos;
    private List<Employee> _emploees;
    private List<Client> _clients;
    private ObservableCollection<DogovorModel> _dogovors = new ObservableCollection<DogovorModel>();
    public ObservableCollection<DogovorModel> Dogovors
    {
        get => _dogovors;
        set => SetProperty(ref _dogovors, value);
    }
    public ICommand CmdNavigateMain { get; }
    public ICommand CmdNavigateCurrentAvto { get; }
    public ICommand CmdExport { get; }
    public OrdersViewModel(MainWindowViewModel owner)
    {
        _owner = owner;
        CmdNavigateMain = new AsyncRelayCommand(async () =>
        { 
            await NavigationService.GetInstance().Navigate(new MainViewModel(owner));
        });
        CmdNavigateCurrentAvto = new AsyncRelayCommand<DogovorModel>(async (d) =>
        {
            var auto = _autos?.First(x => x.Id == d.IdAvto);
            var vm = new CurrentAvtoViewModel(owner, auto);
            await vm.Init();
            vm.Fluent(x => x.IsOrdered = true)
                .Fluent(x => x.SelectedEmployee = _emploees?.First(f => f.Id == d.IdEmployee))
                .Fluent(x => x.SelectedClient = _clients?.First(f => f.Id == d.IdClient));
                
            await NavigationService.GetInstance().Navigate(vm);
        });
        CmdExport = new RelayCommand(ExportHandler);
    }
    public async Task Init()
    {
        var dogovors = await "https://localhost:7258/Dogovor/getAll".GetQuery<Dogovor>();
        var dogovoRs = new ObservableCollection<DogovorModel>(); //очень жесский костыль, мне пришлось
        _autos = new List<Auto>(await "https://localhost:7258/Auto/getAll".GetQuery<Auto>());
        _emploees = new List<Employee>(await "https://localhost:7258/Employee/getAll".GetQuery<Employee>());
        _clients = new List<Client>(await "https://localhost:7258/Client/getAll".GetQuery<Client>());
        var autoTypes = new List<AutoType>(await "https://localhost:7258/AutoType/getAll".GetQuery<AutoType>());
        var carMakes = new List<CarMake>(await "https://localhost:7258/CarMake/getAll".GetQuery<CarMake>());
        foreach (var dogovor in dogovors)
        {
            dogovoRs.Add(new DogovorModel()
            {
                Id = dogovor.Id,
                SaleDate = dogovor.SaleDate,
                Cost = dogovor.Cost,
                IdEmployee = dogovor.IdEmployee,
                IdAvto = dogovor.IdAvto,
                IdClient = dogovor.IdClient,
            });
        }
        
        foreach (var dogovor in dogovoRs)
        {
            dogovor.Employee = _emploees.First(x => x.Id == dogovor.IdEmployee);
            var currAuto = _autos.First(x => x.Id == dogovor.IdAvto);
            var currAutoType = autoTypes.First(x => x.Id == currAuto.IdType);
            var currCarMake = carMakes.First(x => x.Id == currAutoType.MarkId);
            dogovor.CarInfo = $"{currCarMake.Mark} " + $"{currAutoType.Model} " + $"{currAuto.CreationYear} ";
            dogovor.Client = _clients.First(x => x.Id == dogovor.IdClient);
        }
        Dogovors = dogovoRs;
    }

    private void ExportHandler()
    {
        var fd = new OpenFileDialog();
        fd.Multiselect = false;
        fd.Filter = "Word файлы (*.docx)|*.docx|Все файлы (*.*)|*.*";
        fd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        var result = "";
        if (fd.ShowDialog() == true)
        {
            result = fd.FileName;
        }
        var lines = new List<List<string>>()
        {
            new List<string>(){"Клиент", "Марка, Модель, год выпуска", "Дата покупки"}
        };
        lines.AddRange(Dogovors.Select(dogovorModel => new List<string>()
        {
            dogovorModel.Client.Fio, dogovorModel.CarInfo, dogovorModel.SaleDate.ToString("dd.MM.yyyy") + " г."
        }));
        if(string.IsNullOrEmpty(result)) return;
        if(lines.Count < 2) return;
            
        new Word().GenerateWordExportFile(lines, result);
        Process.Start(new ProcessStartInfo(result) { UseShellExecute = true });
    }
}