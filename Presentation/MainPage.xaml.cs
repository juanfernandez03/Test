using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace Presentation
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private static readonly HttpClient _client = new HttpClient();
        string _url = "http://localhost:52218/Matrix";
        public MainPage()
        {
            this.InitializeComponent();
            GetMatrix();
        }
        async void GetMatrix()
        {
            var response = await _client.GetStringAsync(_url);
            var wordstream = JsonConvert.DeserializeObject<IEnumerable<IEnumerable<char>>>(response);
            var rowMatrix = new StringBuilder(string.Empty);
            string row = "";
            List<string> matrix = new List<string>();
            foreach (var item in wordstream)
            {
                foreach (var i in item)
                {
                    row += i.ToString().PadLeft(3);
                    rowMatrix.Append(i);
                }
                matrix.Add(rowMatrix.ToString());
                rowMatrix.Clear();
                row += "\n";
            }
            txt_Matrix.Text = row;
            SendMatrix(row);
        }

        async void SendMatrix(string matrix)
        {
            var sendMatrix = JsonConvert.SerializeObject(matrix);
            var jsonResult = await _client.PostAsync(_url, new StringContent(sendMatrix, Encoding.UTF8, "application/json"));
            var jsonRespond = jsonResult.Content.ReadAsStringAsync().Result;
            var wordList = JsonConvert.DeserializeObject<IEnumerable<string>>(jsonRespond);
            WordsFinder.Text = "Welcome to WordFinder the words found  are " + string.Join(",", wordList);
        }
    }
}
