using PenskeTechnicalAssessment.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Navigation;

namespace PenskeTechnicalAssessment.ViewModels
{
    public class DisplayVM : DependencyObject
    {
        private const string endpoint = "https://cf.nascar.com/cacher/2023/race_list_basic.json";


        public static readonly DependencyProperty RaceDataProperty = DependencyProperty.Register(nameof(RaceData), typeof(ObservableCollection<SeriesList>), typeof(DisplayVM), new PropertyMetadata(default(ObservableCollection<SeriesList>)));
        public ObservableCollection<SeriesList> RaceData
        {
            get => (ObservableCollection<SeriesList>)GetValue(RaceDataProperty);
            set => SetValue(RaceDataProperty, value);
        }

        public static readonly DependencyProperty IsLoadingProperty = DependencyProperty.Register(nameof(IsLoading), typeof(bool), typeof(DisplayVM), new PropertyMetadata(default(bool)));
        public bool IsLoading
        {
            get => (bool)GetValue(IsLoadingProperty);
            set => SetValue(IsLoadingProperty, value);
        }

        public static readonly DependencyProperty SeriesAmountProperty = DependencyProperty.Register(nameof(SeriesAmount), typeof(int), typeof(DisplayVM), new PropertyMetadata(default(int)));
        public int SeriesAmount
        {
            get => (int)GetValue(SeriesAmountProperty);
            set => SetValue(SeriesAmountProperty, value);
        }




        public DisplayVM() 
        {
            GetData();


        }

        public async void GetData()
        {
            await GetRaceInfo();
        }

        public async Task<List<int>> GetRaceInfo()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(endpoint);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
 
            try
            {
                HttpResponseMessage response = await client.GetAsync("");
                response.EnsureSuccessStatusCode();
                string data = await response.Content.ReadAsStringAsync();

                //Convert the snake_case json to camelCase so the deserializer knows how to use the data
                //This is not a problem with Newtonsoft
                data = data.Split(new[] { "_" }, StringSplitOptions.RemoveEmptyEntries).Select(s => char.ToUpperInvariant(s[0]) + s.Substring(1, s.Length - 1)).Aggregate(string.Empty, (s1, s2) => s1 + s2);

                var returnResults = JsonSerializer.Deserialize<Dictionary<string, List<Series>>>(data);


                var interm = new ObservableCollection<SeriesList>();
                foreach (KeyValuePair<string, List<Series>> kvp in returnResults)
                {
                    int seriesIndex = (int.Parse(kvp.Key[kvp.Key.Length - 1].ToString())) - 1;
                    interm.Add(new SeriesList() { 
                        QueryResult = kvp.Value, 
                        SeriesName = kvp.Key,
                        SeriesIndex = seriesIndex
                        
                    });
                }

                SeriesAmount = returnResults.Keys.Count;
                RaceData = interm;

            }
            finally
            {

            }
            //catch(HttpRequestException hre)
            //{
            //    await Console.Out.WriteLineAsync("HTTP Request Exception!");
            //}
            //catch(Exception ex)
            //{
            //    await Console.Out.WriteLineAsync("Other exception!");
            //}


            return new List<int> { 0 };
        }



      
    }
}
