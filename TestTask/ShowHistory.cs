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
        private readonly string host = "localhost";
        private readonly string username = "postgres";
        private readonly string password = "12345";
        private string databaseName;

        public ShowHistory(string databaseName)
        {
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
        public async Task<string> FilterHistory(string filtertype)
        {
            string connectionString = $"Host={host};Username={username};Password={password};Database={databaseName}";
            string sql = "";
            if (filtertype == "OpDateAsc")
            {
                sql = "select\r\n    operationid,\r\n    to_char(operationdate, 'yyyy.MM.dd HH24:MI:SS'),\r\n    cityname,\r\n    datetime,\r\n    temperature,\r\n    conditions\r\nfrom\r\n    history\r\norder by\r\n    operationdate asc;";
            }
            if (filtertype == "OpDateDesc")
            {
                sql = "select\r\n    operationid,\r\n    to_char(operationdate, 'yyyy.MM.dd HH24:MI:SS'),\r\n    cityname,\r\n    datetime,\r\n    temperature,\r\n    conditions\r\nfrom\r\n    history\r\norder by\r\n    operationdate desc;";
            }
            if (filtertype == "CityAsc")
            {
                sql = "select\r\n    operationid,\r\n    to_char(operationdate, 'yyyy.MM.dd HH24:MI:SS'),\r\n    cityname,\r\n    datetime,\r\n    temperature,\r\n    conditions\r\nfrom\r\n    history\r\norder by\r\n    cityname asc;";
            }
            if (filtertype == "CityDesc")
            {
                sql = "select\r\n    operationid,\r\n    to_char(operationdate, 'yyyy.MM.dd HH24:MI:SS'),\r\n    cityname,\r\n    datetime,\r\n    temperature,\r\n    conditions\r\nfrom\r\n    history\r\norder by\r\n    cityname desc;";
            }
            if (filtertype == "DateAsc")
            {
                sql = "select\r\n    operationid,\r\n    to_char(operationdate, 'yyyy.MM.dd HH24:MI:SS'),\r\n    cityname,\r\n    datetime,\r\n    temperature,\r\n    conditions\r\nfrom\r\n    history\r\norder by\r\n    datetime asc;";
            }
            if (filtertype == "DateDesc")
            {
                sql = "select\r\n    operationid,\r\n    to_char(operationdate, 'yyyy.MM.dd HH24:MI:SS'),\r\n    cityname,\r\n    datetime,\r\n    temperature,\r\n    conditions\r\nfrom\r\n    history\r\norder by\r\n    datetime desc;";
            }
            if (filtertype == "TempAsc")
            {
                sql = "select\r\n    operationid,\r\n    to_char(operationdate, 'yyyy.MM.dd HH24:MI:SS'),\r\n    cityname,\r\n    datetime,\r\n    temperature,\r\n    conditions\r\nfrom\r\n    history\r\norder by\r\n    temperature asc;";
            }
            if (filtertype == "TempDesc")
            {
                sql = "select\r\n    operationid,\r\n    to_char(operationdate, 'yyyy.MM.dd HH24:MI:SS'),\r\n    cityname,\r\n    datetime,\r\n    temperature,\r\n    conditions\r\nfrom\r\n    history\r\norder by\r\n    temperature desc;";
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
