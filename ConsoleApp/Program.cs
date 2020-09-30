using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        private static readonly HttpClient Client = new HttpClient();
        static void Main(string[] args)
        {
           var matrix =  RunAsync().GetAwaiter().GetResult();
            SendMatrix(matrix);
        }

        private static void SendMatrix(List<string> matrix)
        {
            var sendMatrix = JsonConvert.SerializeObject(matrix);
            Client.PostAsync("http://localhost:52218/api/matrix", new StringContent(sendMatrix, Encoding.UTF8, "application/json"));
        }

        private static async Task<List<string>> GetItems(string path)
        {
            //var response = await Client.GetAsync(path);
            var url = "http://localhost:52218/api/matrix";

            var response = await Client.GetStringAsync(url);
            List<string> matrix = new List<string>();
            var row = new StringBuilder();
            var m = JsonConvert.DeserializeObject<IEnumerable<IEnumerable<char>>>(response);
            foreach (var item in m)
            {
                foreach (var i in item)
                {
                    row.Append(i);
                    Console.Write($"{i}".PadLeft(2));
                }
                Console.WriteLine();
                matrix.Add(row.ToString());
                row.Clear();
            }
            return matrix;


        }

        private static async Task<List<string>> RunAsync()
        {
            // Update your local service port no. / service APIs etc in the following line
            Client.BaseAddress = new Uri("http://localhost:57579/api/values/");
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                var items = await GetItems("http://localhost:57579/api/values/");
                return items;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception("Mensaje.", e);
            }

            Console.ReadLine();
        }

        //static async void GetMatrix()
        //{
        //    //using (var client = new HttpClient())
        //    //{
        //    //    var response =
        //    //        await client.GetAsync("http://localhost:52218/weatherforecast");
        //    //    string apiResponse = await response.Content.ReadAsStringAsync();
        //    //    var reservationList = JsonConvert.DeserializeObject<List<string>>(apiResponse);

        //    //    return reservationList;
        //    //}
        //    var url = "http://localhost:52218/api/matrix";
        //    var client = new HttpClient();
        //    var response = await client.GetStringAsync(url);
        //    var m = JsonConvert.DeserializeObject<IEnumerable<IEnumerable<char>>>(response);
        //    foreach (var item in m)
        //    {
        //        foreach (var i in item)
        //        {
        //            Console.Write($"{i}".PadLeft(2));
        //        }
        //        Console.WriteLine();
        //    }

        //}
    }
}
