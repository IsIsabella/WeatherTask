using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask
{
    public class ShowResult
    {
        private readonly string host = "localhost";
        private readonly string username = "postgres";
        private readonly string password = "12345";
        private string databaseName;
        private string city;
        public ShowResult(string databaseName, string city)
        {
            this.databaseName = databaseName;
            this.city = city;
        }
        public async Task<string> Results(string restype)
        {
            string connectionString = $"Host={host};Username={username};Password={password};Database={databaseName}";
            string sql = "";
            if (restype == "threehours")
            {
                sql = $"select\r\nc.cityname,\r\nf.datetime,\r\ni.temperature,\r\ni.feelslike,\r\ni.pressure,\r\ni.humidity,\r\ni.conditions,\r\ni.clouds,\r\ni.windspeed,\r\ni.visibility,\r\ni.pop\r\nfrom\r\ncity c\r\njoin\r\nforecast f on c.cityid = f.cityid\r\njoin\r\ninformation i on f.forecastid = i.informationid\r\nwhere\r\nc.cityname = '{city}'\r\nand\r\nf.datetime >= NOW()  \r\norder by\r\nabs(extract(epoch from (f.datetime - NOW()))) \r\nlimit 1;";
            }
            if (restype == "oneday")
            {
                sql = $"select\r\n    c.cityname,\r\n    f.datetime,\r\n    i.temperature,\r\n    i.pressure,\r\n    i.conditions,\r\n    i.pop\r\nfrom\r\n    city c\r\njoin\r\n    forecast f on c.cityid = f.cityid\r\njoin\r\n    information i on f.forecastid = i.informationid\r\nwhere\r\n    c.cityname = '{city}'\r\n    and f.datetime between current_date + interval '1 day' and (current_date + interval '1 day') + interval '21 hours'\r\n    and extract(hour from f.datetime) in (0, 3, 6, 9, 12, 15, 18, 21)\r\norder by\r\n    f.datetime;\r\n ";
            }
            if (restype == "threedays")
            {
                sql = $"select\r\nc.cityname,\r\nf.datetime,\r\ni.temperature,\r\ni.conditions\r\nfrom\r\ncity c\r\njoin\r\nforecast f on c.cityid = f.cityid\r\njoin\r\ninformation i on f.forecastid = i.informationid\r\nwhere\r\nc.cityname = '{city}'\r\nand (\r\nf.datetime between now() - interval '3 days' and now() + interval '3 days'\r\nor (extract(hour from f.datetime) = 0 \r\nand extract(minute from f.datetime) = 0 and extract(second from f.datetime) = 0) \r\n)\r\nand extract(hour from f.datetime) in (0, 12)\r\norder by\r\nf.datetime;";
            }
            if (restype == "fivedays")
            {
                sql = $"select\r\n    c.cityname,\r\n    f.datetime,\r\n    i.temperature,\r\n    i.conditions\r\nfrom\r\n    city c\r\njoin\r\n    forecast f on c.cityid = f.cityid\r\njoin\r\n    information i on f.forecastid = i.informationid\r\nwhere\r\n    c.cityname = '{city}'\r\n    and (\r\n        f.datetime between now() - interval '5 days' and now() + interval '5 days'\r\n        or (extract(hour from f.datetime) = 0 and extract(minute from f.datetime) = 0 and extract(second from f.datetime) = 0) \r\n    )\r\n    and extract(hour from f.datetime) in (0, 12)\r\norder by\r\n    f.datetime;";
            }
            StringBuilder result = new StringBuilder();
            NpgsqlConnection connection = null;
            try
            {
                using (connection = new NpgsqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, connection))
                    {
                        using (NpgsqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            int fieldCount = dataReader.FieldCount;
                            while (dataReader.Read())// Чтаем данные
                            {
                                for (int i = 0; i < fieldCount; i++)
                                {
                                    object value = dataReader.GetValue(i);//Получаем значение столбца.
                                    string stringValue = "";
                                    if (value == DBNull.Value)
                                    {
                                        stringValue = "NULL";
                                    }
                                    else
                                    {
                                        stringValue = value.ToString();
                                    }
                                    result.Append(stringValue).Append("\t");
                                }
                                result.AppendLine();//Переходим на новую строку после каждой записи
                            }
                        }
                    }
                }
                return result.ToString();
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
