using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TestTask
{
    public partial class Menu : Form
    {

        private readonly string databaseName = "weather";
        private Timer Timer;
        private readonly TimeSpan timeInterval = TimeSpan.FromMinutes(180); //Обновлять каждые 3 часа.
        public Menu()
        {
            InitializeComponent();
            InitializeTimer();
            comboBoxTime.DropDownStyle = ComboBoxStyle.DropDownList;//Запрещаем писать свой текст в comboBox.
            comboBoxTime.Items.AddRange(new string[] { "Ближайшие три часа", "На день", "На три дня", "На пять дней" });
            FormClosing += Menu_FormClosing;
        }
        
        private async void FindOut_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(CityName.Text))
                {
                    throw new Exception($"Введите название города.");
                }
                if (comboBoxTime == null || comboBoxTime.SelectedItem == null)
                {
                    throw new Exception($"Выберете период для прогноза.");
                }
                else
                {
                    string city = CityName.Text;
                    CreateInsertDb createInsertDb = new CreateInsertDb(databaseName, city);
                    await createInsertDb.CreateDatabase();
                    await createInsertDb.InsertDatabase();
                    Hide();
                    if (comboBoxTime.SelectedItem.ToString() == "Ближайшие три часа")
                    {
                        var ResForm = new ThreeHoursRes(databaseName, city);
                        ResForm.Show();
                    }
                    if (comboBoxTime.SelectedItem.ToString() == "На день")
                    {
                        var ResForm = new OneDayRes(databaseName, city);
                        ResForm.Show();
                    }
                    if (comboBoxTime.SelectedItem.ToString() == "На три дня")
                    {
                        var ResForm = new ThreeDaysRes(databaseName, city);
                        ResForm.Show();
                    }
                    if (comboBoxTime.SelectedItem.ToString() == "На пять дней")
                    {
                        var ResForm = new FiveDaysRes(databaseName, city);
                        ResForm.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }
        private void buttonHistory_Click(object sender, EventArgs e)
        {
            Hide();
            var ResForm = new History(databaseName);
            ResForm.Show();
        }
        private void Menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Выйти из приложения?", "Предупреждение", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }

        }
        private void InitializeTimer()
        {
            Timer = new Timer();
            Timer.Interval = (int)timeInterval.TotalMilliseconds;
            Timer.Tick += UpdateStart;
            Timer.Start();
        }
        private async void UpdateStart(object sender, EventArgs e)
        {
            try
            {
                UpdateData updateData = new UpdateData(databaseName);
                await updateData.UpdateWeatherData();
                MessageBox.Show("Данные успешно обновлены.");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private async void buttonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateData updateData = new UpdateData(databaseName);
                await updateData.UpdateWeatherData();
                MessageBox.Show("Данные успешно обновлены.");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
