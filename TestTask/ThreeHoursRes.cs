using Npgsql;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace TestTask
{
    public partial class ThreeHoursRes: Form
    {
        private string databaseName;
        private string cityName;
        public ThreeHoursRes(string db, string city)
        {
            InitializeComponent();
            databaseName = db;
            cityName= city;
            labelCity.Text = city;
            //Result_Load(this, EventArgs.Empty);
        }
        private async void Result_Load(object sender, EventArgs e)
        {
            try
            {
                ShowResult showResult = new ShowResult(databaseName, cityName);
                string result = await showResult.Results("threehours");
                string[] parts = result.Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                labelTemp.Text = $"{parts[2]}°C";
                labelFeelsLike.Text = $"{parts[3]}°C";
                labelPressure.Text = $"{parts[4]} мм рт. ст.";
                labelHumidity.Text = $"{parts[5]}%";
                labelConditions.Text = $"{parts[6]}";
                labelClouds.Text= $"{ parts[7]}% ";
                labelWindSpeed.Text = $"{parts[8]} м/с";
                labelVisibility.Text = $"{parts[9]} м";
                labelPop.Text = $"{parts[10]}%";
                ShowHistory showHistory = new ShowHistory(databaseName);
                DateTime date = DateTime.Parse(parts[1]);
                await showHistory.InsertHistory(cityName, date, decimal.Parse(parts[2]), parts[6]);
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
