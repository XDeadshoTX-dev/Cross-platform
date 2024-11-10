using Lab9NETMAUIMobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lab9NETMAUIMobile.Services
{
    class APIService
    {
        private readonly HttpClient _httpClient;
        private string ApiSearchUrl = DeviceInfo.Current.Platform == DevicePlatform.Android ? "http://10.0.2.2:5178/api/v1/search" : "http://localhost:5178/api/v1/search";
        private string ApiAddUrl = DeviceInfo.Current.Platform == DevicePlatform.Android ? "http://10.0.2.2:5178/api/add" : "http://localhost:5178/api/add";
        public APIService()
        {
            _httpClient = new HttpClient();
        }
        public async Task<List<GetBookingResponse>> GetBookingInformationV1(GetBookingModel request)
        {
            var response = await _httpClient.PostAsJsonAsync($"{ApiSearchUrl}/GetBookingInformation", request);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<GetBookingResponse>>(json);
        }
        public async Task<List<GetModelInformationResponse>> GetModelInformation(GetModelInformationModel request)
        {
            var response = await _httpClient.PostAsJsonAsync($"{ApiSearchUrl}/ModelInformation", request);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<GetModelInformationResponse>>(json);
        }
        public async Task<List<GetVehicleCategoryResponse>> GetVehicleCategoryInformation(GetVehicleCategoryModel request)
        {
            var response = await _httpClient.PostAsJsonAsync($"{ApiSearchUrl}/VehicleCategoryInformation", request);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<GetVehicleCategoryResponse>>(json);
        }
        public async Task<string> AddBookingInformationV1(SendBookingModel request)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{ApiAddUrl}/AddBookingInformation", request);
                response.EnsureSuccessStatusCode();

                var json = JsonSerializer.Deserialize<ResponseAdd>(await response.Content.ReadAsStringAsync());
                return json.response;
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}. InnerException: {ex.InnerException.Message}";
            }
        }
        public async Task<string> AddModelInformation(SendModelInformation request)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{ApiAddUrl}/AddModelInformation", request);
                response.EnsureSuccessStatusCode();

                var json = JsonSerializer.Deserialize<ResponseAdd>(await response.Content.ReadAsStringAsync());
                return json.response;
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}. InnerException: {ex.InnerException.Message}";
            }
        }
        public async Task<string> AddVehicleCategoryInformation(SendVehicleCategoryModel request)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{ApiAddUrl}/AddVehicleCategoryInformation", request);
                response.EnsureSuccessStatusCode();

                var json = JsonSerializer.Deserialize<ResponseAdd>(await response.Content.ReadAsStringAsync());
                return json.response;
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}. InnerException: {ex.InnerException.Message}";
            }
        }
        class ResponseAdd
        {
            public string response { get; set; }
        }
    }
}
