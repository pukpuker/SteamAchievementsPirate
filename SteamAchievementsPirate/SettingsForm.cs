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
            steamapiTextBox.Text = Settings.api_key == "1B01FE5A4ED8E822F7763B63F7A8D5FE" ? "FREE" : Settings.api_key;
            startup_checkbox.Checked = Settings.StartUP;
            start_threads_checkbox.Checked = Settings.StartThreads;
            locationoverlay_box.Text = GetTextOverlayLocation(Settings.overlay_location);
            pathBox.Text = string.Join(";", Settings.games_path);
            NotifStyleComboBox.Text = GetNotifStyleText(Settings.notif_style);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateSetting("language", LanguageComboBox.Text, "english");
                UpdateSetting("api_key", steamapiTextBox.Text, "FREE");
                UpdateSetting("games_path", pathBox.Text, "C:\\Games");
                UpdateSetting("notif_style", GetNotifStyle(NotifStyleComboBox.Text), "steamnew");
                UpdateSetting("overlay_location", GetOverlayLocation(locationoverlay_box.Text), "RD");
                UpdateSetting("startup", startup_checkbox.Checked.ToString(), Settings.StartUP.ToString());
                UpdateSetting("start_threads", start_threads_checkbox.Checked.ToString(), Settings.StartThreads.ToString());

                Settings.SettingsParser();
                MessageBox.Show("Settings have been applied!", "SAP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Settings.Exp(ex);
            }
        }

        private void UpdateSetting(string key, string value, string defaultValue)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                Settings.UpdateValue(key, defaultValue);
            }
            else
            {
                Settings.UpdateValue(key, value);
            }
        }

        private string GetNotifStyleText(string style)
        {
            return style == "steamold" ? "Steam Old" : "Steam New";
        }

        private string GetNotifStyle(string text)
        {
            if (text == "Steam Old")
            {
                return "steamold";
            }
            return "steamnew";
        }

        private string GetTextOverlayLocation(string text)
        {
            switch (text)
            {
                case "RU":
                    return "Right Up";
                case "LU":
                    return "Left Up";
                case "LD":
                    return "Left Down";
                default:
                    return "Right Down";
            }

        }
        private string GetOverlayLocation(string text)
        {
            switch (text)
            {
                case "Right Up":
                    return "RU";
                case "Left Up":
                    return "LU";
                case "Left Down":
                    return "LD";
                default:
                    return "RD";
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
            MessageBox.Show("This is not the language of the program, it is the language of achievements. If the game supports the selected language, then the achievements will be in the selected language, but if the game does not support this language, then English will be used by default. If you want to change the language, then just change it here, save the settings and restart the program.", "SAP", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Choose your achievement notification style.", "SAP", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void startup_checkbox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Select in which part of the screen the overlay will be displayed.", "SAP", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
