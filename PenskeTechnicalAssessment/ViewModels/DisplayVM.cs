using PenskeTechnicalAssessment.JSONObjects;
using PenskeTechnicalAssessment.Models;
using PenskeTechnicalAssessment.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Navigation;

namespace PenskeTechnicalAssessment.ViewModels
{
    public class DisplayVM : INotifyPropertyChanged
    {
        //Dear NASCAR, why does one endpoint use snake_case and the other uses dashes?
        private string raceList = "https://cf.nascar.com/cacher/YEAR/race_list_basic.json";

        private int lastSelectedSeries = 1;


        public LapDisplayVM LapDisplay { get; set; }


        private ObservableCollection<SeriesList> _seriesData;
        public ObservableCollection<SeriesList> SeriesData
        {
            get => _seriesData;
            set{
                if (value == _seriesData) return;
                _seriesData = value;
                OnPropertyChanged();
            }
        }

        private SeriesList _currentSeries;
        public SeriesList CurrentSeries
        {
            get => _currentSeries;
            set
            {
                if (value == _currentSeries) return;
                _currentSeries = value;
                OnPropertyChanged();
            }
        }


        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                if (value == _isLoading) return;
                _isLoading = value;
                OnPropertyChanged();
            }
        }



        private bool _hasError;
        public bool HasError
        {
            get => _hasError;
            set
            {
                if (value == _hasError) return;
                _hasError = value;
                OnPropertyChanged();
            }
        }

        private bool _displayContent = false;
        public bool DisplayContent
        {
            get => _displayContent;
            set
            {
                if (value == _displayContent) return;
                _displayContent = value;
                OnPropertyChanged();
            }
        }


        public ICommand GetLapDataCommand => new RelayCommand(
            param => !IsLoading,
            (lapDataRequestParam) =>
            {
                DisplayContent = false;
                LapDataRequest request = lapDataRequestParam as LapDataRequest;
                LapDisplay.RefreshLapData(request);
            });

        public ICommand SelectSeriesCommand => new RelayCommand(
            param => !IsLoading,
            selectedSeriesParam =>
            {
                if ( SeriesData is null || SeriesData.Count == 0) return;
                lastSelectedSeries = int.Parse(selectedSeriesParam as string);
                CurrentSeries = SeriesData.Single(series => series.SeriesNumber == lastSelectedSeries);
            }
            );

        public DisplayVM() 
        {
            RefreshData(2023);
            LapDisplay = new LapDisplayVM(this);

        }

        public async void RefreshData(int year)
        {
            DisplayContent = true;
            IsLoading = true;
            HasError = false;
            SeriesData?.Clear();
            CurrentSeries = null;
            var raceData = await Task.Run(() => GetYearInfo(year));
            if (raceData.Count == 0)
            {
                HasError = true;
                IsLoading = false;
                return;
            }
            var data = await Task.Run(() => FormatYearData(raceData, year));
            if(data.Count == 0)
            {
                HasError = true;
                IsLoading = false;
                return;
            }
            SeriesData = data;
            CurrentSeries = SeriesData.Single(series => series.SeriesNumber == lastSelectedSeries);
            IsLoading = false;


        }

 


        public Dictionary<string, List<JSONSeries>> GetYearInfo(int year)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(raceList.Replace("YEAR", year.ToString()));
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                HttpResponseMessage response = client.GetAsync("").Result;
                response.EnsureSuccessStatusCode();
                string data = response.Content.ReadAsStringAsync().Result;
                //Convert the snake_case json to camelCase so the deserializer knows how to use the data
                //This is not a problem with Newtonsoft
                data = data.Split(new[] { "_" }, StringSplitOptions.RemoveEmptyEntries).Select(s => char.ToUpperInvariant(s[0]) + s.Substring(1, s.Length - 1)).Aggregate(string.Empty, (s1, s2) => s1 + s2);

                var returnData = JsonSerializer.Deserialize<Dictionary<string, List<JSONSeries>>>(data);

                if (returnData is null)
                {
                    throw new JsonException();
                }
                return returnData;


            }
            catch (Exception ex)
            {
                return new Dictionary<string, List<JSONSeries>>();
            }


        }


        public ObservableCollection<SeriesList> FormatYearData(Dictionary<string, List<JSONSeries>> returnResults, int year)
        {
            var interm = new ObservableCollection<SeriesList>();
            try
            {
                foreach (KeyValuePair<string, List<JSONSeries>> kvp in returnResults)
                {
                    List<Series> convertedData = new List<Series>();
                    foreach (JSONSeries series in kvp.Value)
                    {
                        Series newSeries = new Series(series);
                        convertedData.Add(newSeries);
                        newSeries.Year = year;
                    }
                    int seriesNumber = int.Parse(kvp.Key[kvp.Key.Length - 1].ToString());
                    if (seriesNumber < 1 || seriesNumber > 3)
                    {
                        throw new Exception();
                    }
                    string seriesName = kvp.Key switch
                    {
                        "series1" => "Cup Series",
                        "series2" => "Xfinity Series",
                        "series3" => "Truck Series",
                        _ => throw new Exception()
                    };
                    interm.Add(new SeriesList()
                    {
                        QueryResult = convertedData,
                        SeriesName = seriesName,
                        SeriesNumber = seriesNumber
                    });
                }

                return interm;
            }
            catch(Exception ex)
            {
                return new ObservableCollection<SeriesList>();
            }
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }
}
