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
        private NotifyIcon trayIcon;
        private ContextMenuStrip trayMenu;
        public MainForm()
        {
            InitializeComponent();
        }

        private void ShowTree(object sender, EventArgs e)
        {
            Visible = true;
        }

        private void ShowHide(object sender, EventArgs e)
        {
            Visible = false;
        }

        private void OnExit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void MakeTree()
        {
            trayMenu = new ContextMenuStrip();
            trayMenu.Items.Add("Hide", null, ShowHide);
            trayMenu.Items.Add("Show", null, ShowTree);
            trayMenu.Items.Add("Exit", null, OnExit);
            Visible = true;
            ShowInTaskbar = true;
            trayIcon = new NotifyIcon();
            trayIcon.Text = "MysqlBackuper";
            trayIcon.Icon = SystemIcons.Application;
            trayIcon.ContextMenuStrip = trayMenu;
            trayIcon.Visible = true;
        }

        private void UpdateLabels()
        {
            if (Settings.HaveGames && Settings.StartThreads)
            {
                label1.Text = "Status: Work";
                button1.Text = "Stop Parser";
            }
            else if (!Settings.HaveGames)
            {
                label1.Text = "Status: Error (Games not found)";
                button1.Enabled = false;
            }
            else
            {
                label1.Text = "Status: Idle";
                button1.Text = "Start Parser";
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            MakeTree();
            UpdateLabels();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Settings.StartThreads)
            {
                button1.Enabled = false;
                SX.StartThreadsThread.Interrupt();
                Settings.StartThreads = false;
                button1.Enabled = true;
            }
            else if (!Settings.StartThreads)
            {
                button1.Enabled = false;
                SX.StartThreads();
                button1.Enabled = true;
            }
            UpdateLabels();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SettingsForm settings = new SettingsForm();
            settings.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ShowHide(sender, e);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Actions.MyAchivment();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!Settings.StartThreads)
            {
                Actions.Parse();
            }
            else
            {
                MessageBox.Show("Stop the parser first before parsing the games", "SAP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
