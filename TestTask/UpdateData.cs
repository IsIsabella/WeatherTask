using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestTask
{
    public class UpdateData
    {
        private readonly string host;
        private readonly string username;
        private readonly string password;
        private readonly string databaseName;
        public UpdateData(string host, string username, string password, string databaseName)
        {
            this.host = host;
            this.username = username;
            this.password = password;
            this.databaseName = databaseName;
        }
        private async Task<List<string>> GetCities()
        {
            NpgsqlConnection connection = null;
            try
            {
                string connectionString = $"Host={host};Username={username};Password={password};Database={databaseName}";
                List<string> cities = new List<string>();
                using (connection = new NpgsqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    //Получаем список городов, информацию по которым необходимо обновлять.
                    string sqlchecking = $"select distinct cityname\r\nfrom city;";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sqlchecking, connection))
                    {
                        await cmd.ExecuteNonQueryAsync();
                        using (NpgsqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            int fieldCount = dataReader.FieldCount;
                            while (dataReader.Read())
                            {
                                cities.Add(dataReader.GetString(0));
                            }
                        }
                    }
                }
                return cities;
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
        public async Task UpdateWeatherData()
        {
            try
            {
                List<string> cities = await GetCities();
                foreach (string city in cities)
                {
                    CreateInsertDb createInsertDb = new CreateInsertDb(host, username, password, databaseName, city);
                    await createInsertDb.InsertDatabase();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
