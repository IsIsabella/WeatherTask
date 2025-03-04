using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestTask
{
    public partial class OneDayRes: Form
    {

        private readonly string host;
        private readonly string username;
        private readonly string password;
        private readonly string databaseName;
        private readonly string cityName;
        public OneDayRes(string host, string username, string password, string db, string city)
        {
            InitializeComponent();
            this.host = host;
            this.username = username;
            this.password = password;
            databaseName = db;
            cityName = city;
            Result_Load(this, EventArgs.Empty);
        }
        private async void Result_Load(object sender, EventArgs e)
        {
            try
            {
                ShowResult showResult = new ShowResult(host, username, password, databaseName, cityName);
                string result = await showResult.Results("oneday");
                //Разделяем входную строку по отдельным прогнозам.
                string[] forecasts = result.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                string[] firstForecastParts = forecasts[0].Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                labelCity.Text = firstForecastParts[0];
                labelDate.Text= $"{DateTime.Parse(firstForecastParts[1]):dd.MM.yyyy}";
                labeltime1.Text = $"{DateTime.Parse(firstForecastParts[1]):HH:mm}";
                labeltemp1.Text = firstForecastParts[2];
                labelpost1.Text = firstForecastParts[3];
                labelprob1.Text = firstForecastParts[5];
                labelcond1.Text = firstForecastParts[4];
                ShowHistory showHistory = new ShowHistory(host, username, password, databaseName);
                DateTime date = DateTime.Parse(firstForecastParts[1]);
                await showHistory.InsertHistory(cityName, date, decimal.Parse(firstForecastParts[2]), firstForecastParts[4]);

                string[] firstForecastParts2 = forecasts[1].Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                labeltime2.Text = $"{DateTime.Parse(firstForecastParts2[1]):HH:mm}";
                labeltemp2.Text = firstForecastParts2[2];
                labelpost2.Text = firstForecastParts2[3];
                labelprob2.Text = firstForecastParts2[5];
                labelcond2.Text = firstForecastParts2[4];
                date = DateTime.Parse(firstForecastParts2[1]);
                await showHistory.InsertHistory(cityName, date, decimal.Parse(firstForecastParts2[2]), firstForecastParts2[4]);

                string[] firstForecastParts3 = forecasts[2].Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                labeltime3.Text = $"{DateTime.Parse(firstForecastParts3[1]):HH:mm}";
                labeltemp3.Text = firstForecastParts3[2];
                labelpost3.Text = firstForecastParts3[3];
                labelprob3.Text = firstForecastParts3[5];
                labelcond3.Text = firstForecastParts3[4];
                date = DateTime.Parse(firstForecastParts3[1]);
                await showHistory.InsertHistory(cityName, date, decimal.Parse(firstForecastParts3[2]), firstForecastParts3[4]);

                string[] firstForecastParts4 = forecasts[3].Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                labeltime4.Text = $"{DateTime.Parse(firstForecastParts4[1]):HH:mm}";
                labeltemp4.Text = firstForecastParts4[2];
                labelpost4.Text = firstForecastParts4[3];
                labelprob4.Text = firstForecastParts4[5];
                labelcond4.Text = firstForecastParts4[4];
                date = DateTime.Parse(firstForecastParts4[1]);
                await showHistory.InsertHistory(cityName, date, decimal.Parse(firstForecastParts4[2]), firstForecastParts4[4]);

                string[] firstForecastParts5 = forecasts[4].Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                labeltime5.Text = $"{DateTime.Parse(firstForecastParts5[1]):HH:mm}";
                labeltemp5.Text = firstForecastParts5[2];
                labelpost5.Text = firstForecastParts5[3];
                labelprob5.Text = firstForecastParts5[5];
                labelcond5.Text = firstForecastParts5[4];
                date = DateTime.Parse(firstForecastParts5[1]);
                await showHistory.InsertHistory(cityName, date, decimal.Parse(firstForecastParts5[2]), firstForecastParts5[4]);

                string[] firstForecastParts6 = forecasts[5].Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                labeltime6.Text = $"{DateTime.Parse(firstForecastParts6[1]):HH:mm}";
                labeltemp6.Text = firstForecastParts6[2];
                labelpost6.Text = firstForecastParts6[3];
                labelprob6.Text = firstForecastParts6[5];
                labelcond6.Text = firstForecastParts6[4];
                date = DateTime.Parse(firstForecastParts6[1]);
                await showHistory.InsertHistory(cityName, date, decimal.Parse(firstForecastParts6[2]), firstForecastParts6[4]);

                string[] firstForecastParts7 = forecasts[6].Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                labeltime7.Text = $"{DateTime.Parse(firstForecastParts7[1]):HH:mm}";
                labeltemp7.Text = firstForecastParts7[2];
                labelpost7.Text = firstForecastParts7[3];
                labelprob7.Text = firstForecastParts7[5];
                labelcond7.Text = firstForecastParts7[4];
                date = DateTime.Parse(firstForecastParts7[1]);
                await showHistory.InsertHistory(cityName, date, decimal.Parse(firstForecastParts7[2]), firstForecastParts7[4]);

                string[] firstForecastParts8 = forecasts[7].Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                labeltime8.Text = $"{DateTime.Parse(firstForecastParts8[1]):HH:mm}";
                labeltemp8.Text = firstForecastParts8[2];
                labelpost8.Text = firstForecastParts8[3];
                labelprob8.Text = firstForecastParts8[5];
                labelcond8.Text = firstForecastParts8[4];
                date = DateTime.Parse(firstForecastParts8[1]);
                await showHistory.InsertHistory(cityName, date, decimal.Parse(firstForecastParts8[2]), firstForecastParts8[4]);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private void buttonReturn_Click(object sender, EventArgs e)
        {
            Close();
            var ResForm = new Menu();
            ResForm.Show();
        }
    }
}
