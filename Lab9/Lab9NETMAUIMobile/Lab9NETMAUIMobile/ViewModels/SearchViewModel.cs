using Lab9NETMAUIMobile.Models;
using Lab9NETMAUIMobile.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Lab9NETMAUIMobile.ViewModels
{
    class SearchViewModel : INotifyPropertyChanged
    {
        private APIService _apiService;
        private string _addResponse;
        private bool _isLoading;
        public ObservableCollection<GetBookingResponse> GetBookingResults { get; set; } = new();
        public ObservableCollection<GetModelInformationResponse> GetModelResults { get; set; } = new();
        public ObservableCollection<GetVehicleCategoryResponse> GetVehicleResults { get; set; } = new();
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public string AddResponse
        {
            get => _addResponse;
            set
            {
                _addResponse = value;
                OnPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public GetBookingModel GetBookingRequest { get; set; } = new();
        public GetModelInformationModel GetModelRequest { get; set; } = new();
        public GetVehicleCategoryModel GetVehicleCategoryRequest { get; set; } = new();

        public SendBookingModel SendBookingRequest { get; set; } = new();
        public SendModelInformation SendModelRequest { get; set; } = new();
        public SendVehicleCategoryModel SendVehicleRequest { get; set; } = new();
        public BookingDate SendBookingDate { get; set; } = new();
        public ICommand SearchBookingCommand { get; }
        public ICommand SearchModelCommand { get; }
        public ICommand SearchVehicleCommand { get; }

        public ICommand SendBookingCommand { get; }
        public ICommand SendModelCommand { get; }
        public ICommand SendVehicleCommand { get; }

        public class BookingDate 
        { 
            public int Day_from { get; set; }
            public int Month_from { get; set; }
            public int Year_from { get; set; }
            public int Day_to { get; set; }
            public int Month_to { get; set; }
            public int Year_to { get; set; }
        }

        public SearchViewModel()
        {
            _apiService = new APIService();
            SearchBookingCommand = new Command(async () => await SearchBooking());
            SearchModelCommand = new Command(async () => await SearchModel());
            SearchVehicleCommand = new Command(async () => await SearchVehicleCategory());
            
            SendBookingCommand = new Command(async () => await SendBooking());
            SendModelCommand = new Command(async () => await SendModel());
            SendVehicleCommand = new Command(async () => await SendVehicle());
        }
        private async Task SearchBooking()
        {
            IsLoading = true;
            try
            {
                var results = await _apiService.GetBookingInformationV1(GetBookingRequest);
                GetBookingResults.Clear();
                foreach (var result in results)
                {
                    GetBookingResults.Add(result);
                }
                await Task.Delay(2000);
            }
            finally
            {
                IsLoading = false;
            }
        }
        private async Task SearchModel()
        {
            IsLoading = true;
            try
            {
                var results = await _apiService.GetModelInformation(GetModelRequest);
                GetModelResults.Clear();
                foreach (var result in results)
                {
                    GetModelResults.Add(result);
                }
                await Task.Delay(2000);
            }
            finally
            {
                IsLoading = false;
            }
        }
        public async Task SearchVehicleCategory()
        {
            IsLoading = true;
            try
            {
                var results = await _apiService.GetVehicleCategoryInformation(GetVehicleCategoryRequest);
                GetVehicleResults.Clear();
                foreach (var result in results)
                {
                    GetVehicleResults.Add(result);
                }
                await Task.Delay(2000);
            }
            finally
            {
                IsLoading = false;
            }
        }
        public async Task SendBooking()
        {
            IsLoading = true;
            try
            {
                SendBookingRequest.date_from = new DateTime(SendBookingDate.Year_from, SendBookingDate.Month_from, SendBookingDate.Day_from);
                SendBookingRequest.date_to = new DateTime(SendBookingDate.Year_to, SendBookingDate.Month_to, SendBookingDate.Day_to);
                var response = await _apiService.AddBookingInformationV1(SendBookingRequest);
                AddResponse = response;
                await Task.Delay(2000);
            }
            finally
            {
                IsLoading = false;
            }
        }
        public async Task SendModel()
        {
            IsLoading = true;
            try
            {
                var response = await _apiService.AddModelInformation(SendModelRequest);
                AddResponse = response;
                await Task.Delay(2000);
            }
            finally
            {
                IsLoading = false;
            }
        }
        public async Task SendVehicle()
        {
            IsLoading = true;
            try
            {
                var response = await _apiService.AddVehicleCategoryInformation(SendVehicleRequest);
                AddResponse = response;
                await Task.Delay(2000);
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
