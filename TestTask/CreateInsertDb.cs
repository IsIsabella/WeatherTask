using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TestTask
{
    public class CreateInsertDb
    {
        private readonly string host;
        private readonly string username;
        private readonly string password;
        private readonly string databaseName;
        private readonly string city;

        public CreateInsertDb(string host,string username,string password, string databaseName, string city)
        {
            this.host = host;
            this.username = username;
            this.password = password;
            this.databaseName = databaseName;
            this.city = city;
        }
        public async Task<string> CheckDatabase()
        {
            NpgsqlConnection connection = null;
            try
            {
                string connectionString = $"Host={host};Username={username};Password={password};Database=postgres";
                object value = null;
                using (connection = new NpgsqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    //Проверяем нет ли уже такой бд.
                    string sqlchecking = $"select exists(\r\n select datname from pg_catalog.pg_database where lower(datname) = lower('{databaseName}')\r\n);";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sqlchecking, connection))
                    {
                        await cmd.ExecuteNonQueryAsync();
                        using (NpgsqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                value = dataReader.GetValue(0);
                            }
                        }
                    }
                    return value.ToString();
                }
            }
            catch (NpgsqlException ex)
            {
                throw new Exception($"Ошибка при соединении с базой данных: "+ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (connection != null)
                {
                    await connection.CloseAsync();
                    await connection.DisposeAsync(); //Освобождение ресурсов.
                }
            }
        }
        public async Task CreateDatabase()
        {
            NpgsqlConnection connection = null;
            try
            {
                if (await CheckDatabase() == "False")
                {
                    string connectionString = $"Host={host};Username={username};Password={password};Database=postgres";
                    using (connection = new NpgsqlConnection(connectionString))
                    {
                        await connection.OpenAsync();
                        //Создаем бд. 
                        string sqlCreateDb = $"create database {databaseName}";
                        using (NpgsqlCommand createDbcmd = new NpgsqlCommand(sqlCreateDb, connection))
                        {
                            await createDbcmd.ExecuteNonQueryAsync();//Создаем бд.
                        }
                        await connection.CloseAsync();
                        await connection.DisposeAsync();
                        connectionString = $"Host={host};Username={username};Password={password};Database={databaseName}";
                        using (connection = new NpgsqlConnection(connectionString))
                        {
                            await connection.OpenAsync();
                            //Создаем таблицы в бд.
                            string sqlCreateTable = $"create table city(\r\ncityid integer not null primary key generated always as identity check(cityid >0),\r\ncityname varchar(100) not null\r\n);\r\ncreate table forecast(\r\nforecastid integer not null primary key generated always as identity check(forecastid>0),\r\ncityid integer not null check(cityid >0),\r\ndatetime timestamp not null,\r\nforeign key(cityid) references city(cityid) on update cascade\r\n);\r\ncreate table information(\r\ninformationid integer not null primary key check(informationid>0),\r\ntemperature decimal(4,2) not null,\r\nfeelslike decimal(4,2) not null,\r\npressure decimal(6,2) not null,\r\nhumidity integer not null,\r\nconditions varchar(200) not null,\r\nclouds integer not null,\r\nwindspeed decimal(4,2) not null,\r\nvisibility integer not null,\r\npop integer not null,\r\nforeign key(informationid) references forecast(forecastid) on update cascade\r\n);\r\ncreate table history (\r\noperationid integer not null primary key generated always as identity check(operationid >0), \r\noperationdate timestamp not null default now(),\r\ncityname varchar(100) not null,\r\ndatetime timestamp not null,\r\ntemperature decimal(4,2) not null,\r\nconditions varchar(200) not null\r\n);";
                            using (NpgsqlCommand createTableCmd = new NpgsqlCommand(sqlCreateTable, connection))
                            {
                                await createTableCmd.ExecuteNonQueryAsync();
                            }
                        }
                    }
                }
                if (await CheckDatabase() != "True" && await CheckDatabase() != "False")
                {
                    throw new Exception();
                }
            }
            catch (NpgsqlException ex)
            {
                throw new Exception($"Ошибка при соединении с базой данных: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (connection != null)
                {
                    await connection.CloseAsync();
                    await connection.DisposeAsync(); //Освобождение ресурсов.
                }
            }
        }
        public async Task InsertDatabase()
        {
            NpgsqlConnection connection = null;
            try
            {
                string connectionString = $"Host={host};Username={username};Password={password};Database={databaseName}";
                using (connection = new NpgsqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    ConnectToService weatherService = new ConnectToService();
                    Information forecast = await weatherService.GetWeatherForecastAsync(city);
                    if (forecast != null && city!=null)
                    {
                        //Заполняем таблицу city.
                        string sqlInsertCity = "insert into city (cityname) select @city where not exists (select 1 from city where cityname = @city)";
                        using (NpgsqlCommand cmdInsertCity = new NpgsqlCommand(sqlInsertCity, connection))
                        {
                            cmdInsertCity.Parameters.AddWithValue("city", city);
                            await cmdInsertCity.ExecuteNonQueryAsync();
                        }
                        foreach (var f in forecast.Forecasts)
                        {
                            //Заполняем таблицу forecast.
                            string sqlInsertForecast = "insert into forecast (cityid, datetime)\r\nselect (select cityid from city where cityname = @city), @datetime\r\nwhere not exists (\r\n    select 1\r\n    from forecast\r\n    where cityid = (select cityid from city where cityname = @city)\r\n    and datetime = @datetime\r\n);";
                            using (NpgsqlCommand cmdInsertForecast = new NpgsqlCommand(sqlInsertForecast, connection))
                            {
                                cmdInsertForecast.Parameters.AddWithValue("city", city);
                                cmdInsertForecast.Parameters.AddWithValue("datetime", f.DateTime);
                                await cmdInsertForecast.ExecuteNonQueryAsync();
                            }
                            //Заполняем таблицу information.
                            string sqlInsertInformation = "insert into information (temperature, feelslike, pressure, humidity, \r\nconditions, clouds, windspeed, visibility, pop, informationid)\r\nselect @temperature, @feelslike, @pressure, @humidity, @conditions, @clouds, @windspeed, @visibility, @pop, \r\n(select forecastid \r\nfrom forecast \r\nwhere cityid = (select cityid from city where cityname = @city) \r\nand datetime = @datetime)\r\nwhere not exists (\r\nselect 1\r\nfrom information\r\nwhere informationid = (select forecastid \r\nfrom forecast \r\nwhere cityid = (select cityid from city where cityname = @city)\r\nand datetime = @datetime)\r\n);";
                            using (NpgsqlCommand cmdInsertInformation = new NpgsqlCommand(sqlInsertInformation, connection))
                            {
                                cmdInsertInformation.Parameters.AddWithValue("city", city);
                                cmdInsertInformation.Parameters.AddWithValue("datetime", f.DateTime);
                                cmdInsertInformation.Parameters.AddWithValue("temperature", f.Main.Temperature);
                                cmdInsertInformation.Parameters.AddWithValue("feelslike", f.Main.FeelsLike);
                                cmdInsertInformation.Parameters.AddWithValue("pressure", f.Main.Pressure);
                                cmdInsertInformation.Parameters.AddWithValue("humidity", f.Main.Humidity);
                                cmdInsertInformation.Parameters.AddWithValue("conditions", f.Weather[0].Description);
                                cmdInsertInformation.Parameters.AddWithValue("clouds", f.Clouds.All);
                                cmdInsertInformation.Parameters.AddWithValue("windspeed", f.Wind.Speed);
                                cmdInsertInformation.Parameters.AddWithValue("visibility", f.Visibility);
                                cmdInsertInformation.Parameters.AddWithValue("pop", f.Pop);
                                await cmdInsertInformation.ExecuteNonQueryAsync();
                            }

                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                throw new Exception($"Ошибка при соединении с базой данных: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (connection != null)
                {
                    await connection.CloseAsync();
                    await connection.DisposeAsync(); //Освобождение ресурсов.
                }
            }
        }
    }
}
