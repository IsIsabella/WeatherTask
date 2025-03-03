using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TestTask
{
    public class Information//Класс для работы с корневыми объектами JSON-ответа.
    {
        [JsonPropertyName("cod")]//200 - код успешного ответа на API-запрос.
        public string Cod { get; set; }

        [JsonPropertyName("message")]//Сообщение в случае ошибки.
        public int Message { get; set; }

        [JsonPropertyName("cnt")]//Количество Forecasts.
        public int Count { get; set; }

        [JsonPropertyName("list")]//Список объектов Forecast.
        public List<Forecast> Forecasts { get; set; }
    }

    public class Forecast//Разбираем массив "list" JSON-ответа.
    {
        [JsonPropertyName("dt")]//В формате Unix timestamp.
        public long dateTimeUnix { get; set; }

        [JsonPropertyName("main")]//Основная информация о погоде (это какие-то числовые показатели: давление, температура и т.д.).
        public Main Main { get; set; }

        [JsonPropertyName("weather")]//Группа параметров погоды (Дождь, Снег, Облака и т.д.).
        public List<Weather> Weather { get; set; }

        [JsonPropertyName("clouds")]//Облачность, %.
        public Clouds Clouds { get; set; }

        [JsonPropertyName("wind")]//Ветер (скорость, порывы, направление).
        public Wind Wind { get; set; }

        [JsonPropertyName("visibility")]//Видимость, м.
        public int Visibility { get; set; }

        [JsonPropertyName("pop")]//Вероятность выпадения осадков, где  где 0 = 0%, 1 = 100%.
        private double _pop;
        public double Pop
        {
            get
            {
                if (_pop == 0)
                    return 0;
                else
                    return _pop * 100;
            }
            set { _pop = value; }
        }
        public DateTime DateTime
        {
            get => DateTimeOffset.FromUnixTimeSeconds(dateTimeUnix).DateTime;
        }
    }

    public class Main //Разбираем объект "main", который внутри "list", JSON-ответа.
    {
        [JsonPropertyName("temp")]//Температура.
        public double Temperature { get; set; }

        [JsonPropertyName("feels_like")]//Человеческое восприятие погоды.
        public double FeelsLike { get; set; }

        [JsonPropertyName("pressure")]//Атмосферное давление на уровне моря по умолчанию, гПа.
        private double _pressure;
        public double Pressure
        {
            get => _pressure * 0.75;
            set
            {
                _pressure = value;
            }
        }

        [JsonPropertyName("humidity")]//Влажность, %.
        public int Humidity { get; set; }
    }

    public class Weather //Разбираем массив "weather", который внутри "list", JSON-ответа.
    {
        [JsonPropertyName("description")]//Погодные условия.
        public string Description { get; set; }
    }

    public class Clouds//Разбираем объект "clouds", который внутри "list", JSON-ответа.
    {
        [JsonPropertyName("all")]//Облачность, %.
        public int All { get; set; }
    }

    public class Wind //Разбираем объект "wind", который внутри "list", JSON-ответа.
    {
        [JsonPropertyName("speed")]//Скорость ветра, м/с.
        public double Speed { get; set; }
    }
}

