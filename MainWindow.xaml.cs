using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace vejrApp
{
    public partial class MainWindow : Window
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public MainWindow()
        {
            InitializeComponent();
            FetchAndDisplayWeatherData();
        }

        private async void FetchAndDisplayWeatherData()
        {
            try
            {
                string latitude = "55.6820136";
                string longitude = "12.5618463";
                string requestUri = "https://api.open-meteo.com/v1/forecast?" +
                                    "latitude=" + latitude +
                                    "&longitude=" + longitude +
                                    "&current=temperature_2m,wind_speed_10m" +
                                    "&hourly=temperature_2m,relative_humidity_2m,wind_speed_10m" +
                                    "&forecast_days=1";

                HttpResponseMessage response = await _httpClient.GetAsync(requestUri);
                response.EnsureSuccessStatusCode();

                // Once the response is successful, you can process the weather data as desired.
                string responseBody = await response.Content.ReadAsStringAsync();
                ProcessWeatherData(responseBody);
            }
            catch (HttpRequestException e)
            {
                MessageBox.Show($"Error fetching weather data: {e.Message}");
            }
        }

        private void ProcessWeatherData(string data)
        {
            // Update the TextBox with the weather data
            Boxen.Text = data;
        }

        private void Boxen_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            // This method can be used to handle user input changes if necessary
        }
    }
}