using Lab9NETMAUIMobile.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microcharts;
using Microcharts.Maui;
using SkiaSharp;
using SkiaSharp.Views.Maui.Controls;
using Lab9NETMAUIMobile.Models;

namespace Lab9NETMAUIMobile.ViewModels
{
    class GraphicViewModel : INotifyPropertyChanged
    {
        private APIService _apiService;
        private Chart _chart;
        public Chart Chart
        {
            get => _chart;
            set
            {
                _chart = value;
                OnPropertyChanged();
            }
        }
        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public GraphicViewModel()
        {
            _apiService = new APIService();
            LoadChartData().ConfigureAwait(false);
        }
        public async Task LoadChartData()
        {
            if (IsLoading || Chart != null) return;
            IsLoading = true;
            try
            {
                var modelCodes = new List<string>
                {
                    "MOD012", "MOD111", "MOD112", "MOD123", "MOD234",
                    "MOD345", "MOD456", "MOD567", "MOD678", "MOD789",
                    "MOD890", "MOD901"
                };
                List<GetModelInformationResponse> results = new List<GetModelInformationResponse>();
                foreach (var code in modelCodes)
                {

                    var response = await _apiService.GetModelInformation(new GetModelInformationModel
                    {
                        model_code = code
                    });
                    results.Add(new GetModelInformationResponse
                    {
                        model_name = response[0].model_name,
                        daily_hire_rate = response[0].daily_hire_rate
                    });
                }


                var entries = results.Select(model => new ChartEntry((float)model.daily_hire_rate)
                {
                    Label = model.model_name,
                    ValueLabel = model.daily_hire_rate.ToString(),
                    Color = SKColor.Parse("#3498db")
                }).ToList();

                Chart = new BarChart { Entries = entries, LabelTextSize = 30 };
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
