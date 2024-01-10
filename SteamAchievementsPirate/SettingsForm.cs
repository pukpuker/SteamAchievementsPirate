using SteamAchivmentsForPirates;

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
                else if (string.IsNullOrWhiteSpace(steamapiTextBox.Text))
                {
                    Settings.UpdateValue("api_key", "FREE");
                }
                Settings.SettingsParser();
                MessageBox.Show("Settings have been applied!", "SAP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Settings.Exp(ex);
            }
        }
    }
}
