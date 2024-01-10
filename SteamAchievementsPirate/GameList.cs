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
    public partial class GameList : Form
    {
        public GameList()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void GameList_Load(object sender, EventArgs e)
        {
            try
            {
                foreach (var file in Directory.GetFiles($"{Settings.path}"))
                {
                    if (file.Contains("info"))
                    {
                        string[] all_massive_file = File.ReadAllText(file).Split('|');
                        string path = all_massive_file[0];
                        string appname = all_massive_file[1];
                        string emulator = all_massive_file[2];
                        string appid = all_massive_file[3];
                        string language = all_massive_file[4];
                        dataGridView1.Rows.Add(appid, appname, emulator, path, language);
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("An error has occurred. There are not enough arguments. Try clicking the \"Parse\" button in the main menu. If this does not help, then try deleting the “games” folder in the program folder.", "SAP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                Settings.Exp(ex);
            }
        }
    }
}
