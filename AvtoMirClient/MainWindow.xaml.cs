using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
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

namespace AvtoMirClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GetInfo();
        }

        private async void GetInfo()
        {
            var message = "";
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("https://localhost:7258/Auto/getAll2");
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<List<AutoDto>>();
                }
                else
                {
                    message = $"server error code {response.StatusCode}";
                }
            }
        }
    }

    public class AutoDto
    {
        public int id { get; set; }
        public int regNumber { get; set; }
        public string vinNumber { get; set; }
        public DateTime creationYear { get; set; }
        public int price { get; set; }
        public string color { get; set; }
        public int idType { get; set; }
        public string image { get; set; }
    }
}