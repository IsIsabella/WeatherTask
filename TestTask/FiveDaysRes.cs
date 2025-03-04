using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestTask
{
    public partial class FiveDaysRes: Form
    {
        private readonly string host;
        private readonly string username;
        private readonly string password;
        private readonly string databaseName;
        private readonly string cityName;
        public FiveDaysRes(string host, string username, string password, string db, string city)
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
                string result = await showResult.Results("fivedays");
                string[] forecasts = result.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                string[] firstForecastParts = forecasts[0].Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
                labelcity.Text = $"{firstForecastParts[0]}";
                labeldate1.Text = $"{DateTime.Parse(firstForecastParts[1]):dd.MM.yyyy}";
                labeltemp1.Text = firstForecastParts[2];
                labelcond1.Text = firstForecastParts[3];

                ShowHistory showHistory = new ShowHistory(host, username, password, databaseName);
                DateTime date = DateTime.Parse(firstForecastParts[1]);
                await showHistory.InsertHistory(cityName, date, decimal.Parse(firstForecastParts[2]), firstForecastParts[3]);


                string[] firstForecastParts1 = forecasts[1].Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
                labeltemp2.Text = firstForecastParts1[2];
                labelcond2.Text = firstForecastParts1[3];

                await showHistory.InsertHistory(cityName, date, decimal.Parse(firstForecastParts1[2]), firstForecastParts1[3]);

                string[] firstForecastParts2 = forecasts[2].Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
                labeldate2.Text = $"{DateTime.Parse(firstForecastParts2[1]):dd.MM.yyyy}";
                labeltemp3.Text = firstForecastParts2[2];
                labelcond3.Text = firstForecastParts2[3];

                date = DateTime.Parse(firstForecastParts2[1]);
                await showHistory.InsertHistory(cityName, date, decimal.Parse(firstForecastParts2[2]), firstForecastParts2[3]);

                string[] firstForecastParts3 = forecasts[3].Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
                labeltemp4.Text = firstForecastParts3[2];
                labelcond4.Text = firstForecastParts3[3];

                await showHistory.InsertHistory(cityName, date, decimal.Parse(firstForecastParts3[2]), firstForecastParts3[3]);

                string[] firstForecastParts4 = forecasts[4].Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
                labeldate3.Text = $"{DateTime.Parse(firstForecastParts4[1]):dd.MM.yyyy}";
                labeltemp5.Text = firstForecastParts4[2];
                labelcond5.Text = firstForecastParts4[3];

                date = DateTime.Parse(firstForecastParts4[1]);
                await showHistory.InsertHistory(cityName, date, decimal.Parse(firstForecastParts4[2]), firstForecastParts4[3]);

                string[] firstForecastParts5 = forecasts[5].Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
                labeltemp6.Text = firstForecastParts5[2];
                labelcond6.Text = firstForecastParts5[3];

                await showHistory.InsertHistory(cityName, date, decimal.Parse(firstForecastParts5[2]), firstForecastParts5[3]);

                string[] firstForecastParts6 = forecasts[6].Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
                labeldate4.Text = $"{DateTime.Parse(firstForecastParts6[1]):dd.MM.yyyy}";
                labeltemp7.Text = firstForecastParts6[2];
                labelcond7.Text = firstForecastParts6[3];

                date = DateTime.Parse(firstForecastParts6[1]);
                await showHistory.InsertHistory(cityName, date, decimal.Parse(firstForecastParts6[2]), firstForecastParts6[3]);

                string[] firstForecastParts7 = forecasts[7].Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
                labeltemp8.Text = firstForecastParts7[2];
                labelcond8.Text = firstForecastParts7[3];

                await showHistory.InsertHistory(cityName, date, decimal.Parse(firstForecastParts7[2]), firstForecastParts7[3]);

                string[] firstForecastParts8 = forecasts[8].Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
                labeldate5.Text = $"{DateTime.Parse(firstForecastParts8[1]):dd.MM.yyyy}";
                labeltemp9.Text = firstForecastParts8[2];
                labelcond9.Text = firstForecastParts8[3];

                date = DateTime.Parse(firstForecastParts8[1]);
                await showHistory.InsertHistory(cityName, date, decimal.Parse(firstForecastParts8[2]), firstForecastParts8[3]);

                string[] firstForecastParts9 = forecasts[9].Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
                labeltemp10.Text = firstForecastParts9[2];
                labelcond10.Text = firstForecastParts9[3];

                await showHistory.InsertHistory(cityName, date, decimal.Parse(firstForecastParts9[2]), firstForecastParts9[3]);

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
