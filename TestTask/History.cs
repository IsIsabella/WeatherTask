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
    public partial class History: Form
    {
        private readonly string host;
        private readonly string username;
        private readonly string password;
        private readonly string databaseName;
        public History(string host, string username, string password, string db)
        {
            InitializeComponent();
            this.host = host;
            this.username = username;
            this.password = password;
            databaseName = db;
            Result_Load(this, EventArgs.Empty);
        }
        private async void Result_Load(object sender, EventArgs e)
        {
            try
            {
                ShowHistory showResult = new ShowHistory(host, username, password, databaseName);
                string result = await showResult.History();
                richTextBoxHistory.Text = result;
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

        private async void buttonFilterOpDateAsc_Click(object sender, EventArgs e)
        {
            try
            {
                ShowHistory showResult = new ShowHistory(host, username, password, databaseName);
                string result = await showResult.FilterHistory("OpDateAsc");
                richTextBoxHistory.Clear();
                richTextBoxHistory.Text = result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async void buttonFilterOpDateDesc_Click(object sender, EventArgs e)
        {
            try
            {
                ShowHistory showResult = new ShowHistory(host, username, password, databaseName);
                string result = await showResult.FilterHistory("OpDateDesc");
                richTextBoxHistory.Clear();
                richTextBoxHistory.Text = result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async void buttonFilterCityAsc_Click(object sender, EventArgs e)
        {
            try
            {
                ShowHistory showResult = new ShowHistory(host, username, password, databaseName);
                string result = await showResult.FilterHistory("CityAsc");
                richTextBoxHistory.Clear();
                richTextBoxHistory.Text = result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async void buttonFilterCityDesc_Click(object sender, EventArgs e)
        {
            try
            {
                ShowHistory showResult = new ShowHistory(host, username, password, databaseName);
                string result = await showResult.FilterHistory("CityDesc");
                richTextBoxHistory.Clear();
                richTextBoxHistory.Text = result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async void buttonFilterDateAsc_Click(object sender, EventArgs e)
        {
            try
            {
                ShowHistory showResult = new ShowHistory(host, username, password, databaseName);
                string result = await showResult.FilterHistory("DateAsc");
                richTextBoxHistory.Clear();
                richTextBoxHistory.Text = result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async void buttonFilterDateDesc_Click(object sender, EventArgs e)
        {
            try
            {
                ShowHistory showResult = new ShowHistory(host, username, password, databaseName);
                string result = await showResult.FilterHistory("DateDesc");
                richTextBoxHistory.Clear();
                richTextBoxHistory.Text = result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async void buttonFilterTempAsc_Click(object sender, EventArgs e)
        {
            try
            {
                ShowHistory showResult = new ShowHistory(host, username, password, databaseName);
                string result = await showResult.FilterHistory("TempAsc");
                richTextBoxHistory.Clear();
                richTextBoxHistory.Text = result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async void buttonFilterTempDesc_Click(object sender, EventArgs e)
        {
            try
            {
                ShowHistory showResult = new ShowHistory(host, username, password, databaseName);
                string result = await showResult.FilterHistory("TempDesc");
                richTextBoxHistory.Clear();
                richTextBoxHistory.Text = result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
