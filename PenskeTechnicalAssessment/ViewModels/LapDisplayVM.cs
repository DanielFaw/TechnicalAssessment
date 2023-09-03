using PenskeTechnicalAssessment.Models;
using PenskeTechnicalAssessment.JSONObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using PenskeTechnicalAssessment.Tools;
using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PenskeTechnicalAssessment.ViewModels
{
    public class LapDisplayVM : INotifyPropertyChanged
    {
        private string lapTimes = "https://cf.nascar.com/cacher/YEAR/SERIES/RACEID/lap-times.json";
        private DisplayVM parentVM;
        private int lastYearRequest;

        private ObservableCollection<RaceInfo> _raceData;
        public ObservableCollection<RaceInfo> RaceData
        {
            get => _raceData;
            set
            {
                if (value == _raceData) return;
                _raceData = value;
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


        private LapDataRequest _dataRequest;
        public LapDataRequest DataRequest
        {
            get => _dataRequest;
            set
            {
                if (value == _dataRequest) return;
                _dataRequest = value;
                OnPropertyChanged();
            }
        }

        public ICommand BackToMainViewCommand => new RelayCommand(
            p => true,
            p =>
            {
                DisplayContent = false;
                parentVM.RefreshData(lastYearRequest);
            }
            );

        public LapDisplayVM(DisplayVM parent)
        {
            parentVM = parent;
        }

        public async void RefreshLapData(LapDataRequest request)
        {
            DataRequest = request;
            RefreshLapData(request.Year, request.Series, request.RaceID);
        }

        public async void RefreshLapData(int year, int seriesId, int raceId)
        {
            DisplayContent = true;
            IsLoading = true;
            HasError = false;
            RaceData?.Clear();
            lastYearRequest = year;
            var raceData = await Task.Run(() => GetRaceInfo(year, seriesId, raceId));
            if (raceData.Count == 0)
            {
                HasError = true;
                IsLoading = false;
                return;
            }
            var data = await Task.Run(() => FormatRaceData(raceData));
            RaceData = data;
            IsLoading = false;
        }



        public List<RaceInfo> GetRaceInfo(int year, int seriesId, int raceId)
        {
            HttpClient client = new HttpClient();
            string lapTimeUri =
                lapTimes.Replace("YEAR", year.ToString())
                .Replace("SERIES", seriesId.ToString())
                .Replace("RACEID", raceId.ToString());

            client.BaseAddress = new Uri(lapTimeUri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                HttpResponseMessage response = client.GetAsync("").Result;
                response.EnsureSuccessStatusCode();
                string data = response.Content.ReadAsStringAsync().Result;

                //This data is in the format we need
                var returnData = JsonSerializer.Deserialize<JSONLapTimes>(data);

                if (returnData is null)
                {
                    throw new JsonException();
                }
                return returnData.laps;


            }
            catch (Exception ex)
            {
                return new  List<RaceInfo>();
            }
        }

        public ObservableCollection<RaceInfo> FormatRaceData(List<RaceInfo> returnResults)
        {
            var interm = new ObservableCollection<RaceInfo>(returnResults);

            foreach(var racer in interm)
            {
                //Remove the first lap 
                racer.StartingPosition = racer.Laps[0].RunningPos;
                racer.Laps.RemoveAt(0);

                //Get average laptime
                float time = 0.0f;
                int counter = 0;
                foreach(var lap in racer.Laps)
                {
                    if(lap.LapTime.HasValue)
                    {
                        time += (float)lap.LapTime;
                        counter++;
                    }
                }

                racer.AverageLapTime = time / counter;

            }

            return interm;
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
