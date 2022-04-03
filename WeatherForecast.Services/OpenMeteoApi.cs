using System.Globalization;
using System.Text.Json;
using System.Text.Json.Nodes;
using WeatherForecast.Domain;

namespace WeatherForecast.Services
{
    public class OpenMeteoApi
    {
        private readonly HttpClient _httpClient;

        public OpenMeteoApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://api.open-meteo.com");

        }

        public async Task<WeatherData> GetForecast(float lat, float lon)
        {
            try
            {
                var latStr = lat.ToString("0.00", CultureInfo.InvariantCulture);
                var lonStr = lon.ToString("0.00", CultureInfo.InvariantCulture);
                var responce = await _httpClient
                .GetAsync($"/v1/forecast?hourly=temperature_2m,precipitation,windspeed_10m,winddirection_10m&latitude={latStr}&longitude={lonStr}&temperature_unit=celsius&windspeed_unit=kmh&timeformat=iso8601");
                if (responce != null && responce.IsSuccessStatusCode)
                {
                    var jsonString = await responce.Content.ReadAsStringAsync();
                    var result = OpenMeteoApiConvertor.ConvertResponce(jsonString);
                    return result;
                }
                else
                {
                    //todo
                    throw new Exception();
                }
            }
            catch (HttpRequestException httpEx)
            {

                throw;
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
