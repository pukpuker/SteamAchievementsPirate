using SteamAchivmentsForPirates;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SteamAchievementsPirate
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (Settings.HaveGames && Settings.StartThreads)
            {
                label1.Text = "Status: Work";
            }
            else if (!Settings.HaveGames)
            {
                label1.Text = "Status: Error (Games not found)";
            }
            else
            {
                label1.Text = "Status: Idle";
            }
        }

        private void StartParser()
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SettingsForm settings = new SettingsForm();
            settings.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
