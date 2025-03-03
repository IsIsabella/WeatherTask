using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace TestTask
{
    public class ConnectToService
    {
        private readonly string apiKey = "303716978080f5d89c11fb41a42db11e";
        private readonly HttpClient httpClient;

        public ConnectToService()
        {
            httpClient = new HttpClient();
        }

        public async Task<Information> GetWeatherForecastAsync(string city, int days = 5)
        {
            string apiUrl = $"https://api.openweathermap.org/data/2.5/forecast?q={city}&appid={apiKey}&units=metric";  //units=metric для градусов Цельсия
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();
                string jsonString = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    NumberHandling = JsonNumberHandling.AllowReadingFromString
                };
                Information weatherResponse = JsonSerializer.Deserialize<Information>(jsonString, options);
                if (weatherResponse.Cod != "200")
                {
                    MessageBox.Show($"API указан неверно: {weatherResponse.Message}");
                    return null;
                }
                return weatherResponse;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Ошибка при соединении с сайтом: "+ex.Message);
            }
            catch (JsonException ex)
            {
                throw new Exception("Ошибка обработки JSON-формата : " + ex.Message);
            }
            catch (TaskCanceledException ex)
            {
                throw new Exception("Превышено время ожидания ответа от API : " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
