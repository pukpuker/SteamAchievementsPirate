using SteamAchivmentsForPirates;
using System.Diagnostics;

namespace SteamAchievementsPirate
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            LanguageComboBox.Text = Settings.language;
            if (Settings.api_key == "1B01FE5A4ED8E822F7763B63F7A8D5FE")
            {
                steamapiTextBox.Text = $"FREE";
            }
            else
            {
                steamapiTextBox.Text = Settings.api_key;
            }
            startup_checkbox.Checked = Settings.StartUP;
            start_threads_checkbox.Checked = Settings.StartThreads;
            string ready_path = "";
            foreach (var one in Settings.games_path)
            {
                if (ready_path == "")
                {
                    ready_path = one;
                }
                else
                {
                    ready_path = ready_path + $";{one}";
                }
            }
            pathBox.Text = ready_path;
            if (Settings.notif_style == "steamnew")
            {
                NotifStyleComboBox.Text = "Steam New";
            }
            else if (Settings.notif_style == "steamold")
            {
                NotifStyleComboBox.Text = "Steam Old";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (LanguageComboBox.Text != Settings.language)
                {
                    Settings.UpdateValue("language", LanguageComboBox.Text);
                }
                if (!string.IsNullOrWhiteSpace(Settings.api_key) || steamapiTextBox.Text != Settings.api_key)
                {
                    Settings.UpdateValue("api_key", steamapiTextBox.Text);
                }
                if (!string.IsNullOrWhiteSpace(pathBox.Text))
                {
                    Settings.UpdateValue("games_path", pathBox.Text);
                }
                if (!string.IsNullOrWhiteSpace(NotifStyleComboBox.Text))
                {
                    string itog = null;
                    if (NotifStyleComboBox.Text == "Steam New")
                    {
                        itog = "steamnew";
                    }
                    else if (NotifStyleComboBox.Text == "Steam Old")
                    {
                        itog = "steamold";
                    }
                    else
                    {
                        itog = "steamnew";
                    }
                    Settings.UpdateValue("notif_style", itog);
                }
                if (startup_checkbox.Checked != Settings.StartUP)
                {
                    Settings.UpdateValue("startup", startup_checkbox.Checked.ToString());
                }
                if (start_threads_checkbox.Checked != Settings.StartThreads)
                {
                    Settings.UpdateValue("start_threads", start_threads_checkbox.Checked.ToString());
                }
                if (string.IsNullOrWhiteSpace(LanguageComboBox.Text))
                {
                    Settings.UpdateValue("language", "english");
                }
                if (string.IsNullOrWhiteSpace(steamapiTextBox.Text))
                {
                    Settings.UpdateValue("api_key", "FREE");
                }
                if (string.IsNullOrWhiteSpace(pathBox.Text))
                {
                    Settings.UpdateValue("games_path", "C:\\Games");
                }
                if (string.IsNullOrWhiteSpace(NotifStyleComboBox.Text))
                {
                    Settings.UpdateValue("notif_style", "steamnew");
                }
                Settings.SettingsParser();
                MessageBox.Show("Settings have been applied!", "SAP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Settings.Exp(ex);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The program supports several paths. To separate paths, add a \";\" at the end of the path.\n Paths must be in the format: disk:\\Folder (for example: C:\\Games;D:\\Games;D:\\Program\\Games)", "SAP", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult button = MessageBox.Show("You can leave this parameter empty, since the program can enter the API KEY, but perhaps the free API key may break and in case there are errors from Steam (403), then put your key. It can be done on the website: https://steamcommunity.com/dev/apikey.\nDo you want to go to the website for obtaining a Steam API key? (You will need a Nolimit account)", "SAP", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (button == DialogResult.Yes)
            {
                Process.Start("explorer.exe", "https://steamcommunity.com/dev/apikey");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is not the language of the program, it is the language of achievements. If the game supports the selected language, then the achievements will be in the selected language, but if the game does not support this language, then English will be used by default. If you want to change the language, then just change it here, save the settings and restart the program.", "SAP", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Choose your achievement notification style.", "SAP", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
