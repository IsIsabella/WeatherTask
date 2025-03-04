using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace TestTask
{
    public class ShowHistory
    {
        private readonly string host;
        private readonly string username;
        private readonly string password;
        private readonly string databaseName;

        public ShowHistory(string host, string username, string password, string databaseName)
        {
            this.host = host;
            this.username = username;
            this.password = password;
            this.databaseName = databaseName;
        }
        public async Task InsertHistory(string city, DateTime datetime, decimal temperature, string conditions)
        {
            NpgsqlConnection connection = null;
            try
            {
                string connectionString = $"Host={host};Username={username};Password={password};Database={databaseName}";
                using (connection = new NpgsqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    string sql = "insert into history (cityname, datetime, temperature, conditions)\r\nvalues (@city, @datetime, @temperature, @conditions);";
                    using (NpgsqlCommand cmdInsertHistory = new NpgsqlCommand(sql, connection))
                    {
                        cmdInsertHistory.Parameters.AddWithValue("city", city);
                        cmdInsertHistory.Parameters.AddWithValue("datetime", datetime);
                        cmdInsertHistory.Parameters.AddWithValue("temperature", temperature);
                        cmdInsertHistory.Parameters.AddWithValue("conditions", conditions);
                        await cmdInsertHistory.ExecuteNonQueryAsync();
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
                    await connection.DisposeAsync();
                }
            }
        }
        public async Task<string> History()
        {
            string connectionString = $"Host={host};Username={username};Password={password};Database={databaseName}";
            string sql = $"select\r\n    operationid,\r\n    to_char(operationdate, 'yyyy.MM.dd HH24:MI:SS'),\r\n    cityname,\r\n    datetime,\r\n    temperature,\r\n    conditions\r\nfrom\r\n    history;";
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
                                for (int i = 0; i < fieldCount; i++)//Получаем значения каждого столбца.
                                {
                                    object value = dataReader.GetValue(i);
                                    string stringValue = "";
                                    if (value == DBNull.Value)
                                    {
                                        stringValue = "NULL";
                                    }
                                    else
                                    {
                                        stringValue = value.ToString() + "          ";
                                    }
                                    result.Append(stringValue);
                                }
                                result.AppendLine();
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
        public async Task<string> FilterHistory(string filtertype)
        {
            string connectionString = $"Host={host};Username={username};Password={password};Database={databaseName}";
            string sql = "select\r\n    operationid,\r\n    to_char(operationdate, 'yyyy.MM.dd HH24:MI:SS'),\r\n    cityname,\r\n    datetime,\r\n    temperature,\r\n    conditions\r\nfrom\r\n    history\r\n";
            string orderByPart = "";
            switch (filtertype)
            {
                case "OpDateAsc":
                    orderByPart = "order by\r\n    operationdate asc;";
                    break;
                case "OpDateDesc":
                    orderByPart = "order by\r\n    operationdate desc;";
                    break;
                case "CityAsc":
                    orderByPart = "order by\r\n    cityname asc;";
                    break;
                case "CityDesc":
                    orderByPart = "order by\r\n    cityname desc;";
                    break;
                case "DateAsc":
                    orderByPart = "order by\r\n    datetime asc;";
                    break;
                case "DateDesc":
                    orderByPart = "order by\r\n    datetime desc;";
                    break;
                case "TempAsc":
                    orderByPart = "order by\r\n    temperature asc;";
                    break;
                case "TempDesc":
                    orderByPart = "order by\r\n    temperature desc;";
                    break;
                default:
                    break;
            }
            sql += orderByPart;
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
                                        stringValue = value.ToString() + "          ";
                                    }
                                    result.Append(stringValue);
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
