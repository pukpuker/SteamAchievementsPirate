using Newtonsoft.Json.Linq;
using SteamAchivmentsForPirates;
using System.IO;
using System.Reflection.PortableExecutable;

namespace SteamAchievementsPirate
{
    public partial class achievement_vitrina : Form
    {
        public static string app_id = "";

        public achievement_vitrina()
        {
            InitializeComponent();
        }

        private void achievement_vitrina_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < SX.games_APPIDS.Count; i++)
            {
                Button newButton = new Button
                {
                    Text = SX.games_APPIDS[i],
                    Location = new Point(10, 10 + (30 * i))
                };
                newButton.Click += NewButton_Click;
                this.Controls.Add(newButton);
            }
        }

        private (int, int, int, int) Sort(string appid, Panel panel, bool locked, int i)
        {
            var path = Path.Combine(Settings.path, $"{appid}.txt");
            var pathik = Path.Combine(Settings.path, $"{appid}_percents.txt");
            string json = "";
            if (File.Exists(path) && File.Exists(pathik))
            {
                json = File.ReadAllText(path);
            }
            else
            {
                Achievements.CreateCheme(appid);
                json = File.ReadAllText(path);
            }
            JObject obj = JObject.Parse(json);
            JArray achievements = (JArray)obj["game"]["availableGameStats"]["achievements"];
            int Count = achievements.Count;
            int count_hidden = 0;
            int unlocked_ach = 0;
            int locked_ach = 0;
            foreach (JObject achievement in achievements)
            {
                int local_locked = 0;
                //
                string name = (string)achievement["name"];
                string displayName = (string)achievement["displayName"];
                string description = (string)achievement["description"];
                int hidden = (int)achievement["hidden"];
                //

                string photo_path = "";

                string local_path = Path.Combine(Settings.path, $"{appid}_achievements.txt");
                List<string> fck_line = new List<string>(File.ReadAllLines(local_path));
                if (fck_line.Contains(name))
                {
                    photo_path = Path.Combine(Settings.path, $"AchiviementsPhotos", $"{appid}_{name}_default.jpg");
                    unlocked_ach++;
                }
                else
                {
                    photo_path = Path.Combine(Settings.path, $"AchiviementsPhotos", $"{appid}_{name}_gray.jpg");
                    locked_ach++;
                    local_locked = 1;
                }
                if (hidden == 0)
                {
                    if (locked && local_locked == 1)
                    {
                        PictureBox newPictureBox = new PictureBox
                        {
                            Size = new Size(64, 64),
                            Location = new Point(10, 10 + (74 * i)),
                            Image = Image.FromFile(photo_path),
                            SizeMode = PictureBoxSizeMode.StretchImage
                        };
                        Label newLabel = new Label
                        {
                            Text = $"{displayName}",
                            Font = new Font("Arial", 14, FontStyle.Bold),
                            Location = new Point(90, 13 + (74 * i)),
                            AutoSize = true
                        };
                        Label newLabel2 = new Label
                        {
                            Text = $"{description}",
                            Location = new Point(90, 43 + (74 * i)),
                            Font = new Font("Arial", 8),
                            AutoSize = true
                        };
                        panel.Controls.Add(newPictureBox);
                        panel.Controls.Add(newLabel);
                        panel.Controls.Add(newLabel2);
                        i++;
                    }
                    else if (!locked && local_locked == 0)
                    {
                        string pathik_JSON = File.ReadAllText(pathik);
                        JObject objIK = JObject.Parse(pathik_JSON);
                        JArray achievements_percents = (JArray)objIK["achievementpercentages"]["achievements"];
                        JObject statistician_pizdos = (JObject)achievements_percents.FirstOrDefault(x => (string)x["name"] == name);
                        double displayNameFinoUgr = (double)statistician_pizdos["percent"];
                        displayNameFinoUgr = Math.Round(displayNameFinoUgr, 2);

                        PictureBox newPictureBox = new PictureBox
                        {
                            Size = new Size(64, 64),
                            Location = new Point(10, 10 + (74 * i)),
                            Image = Image.FromFile(photo_path),
                            SizeMode = PictureBoxSizeMode.StretchImage
                        };
                        Label newLabel = new Label
                        {
                            Text = $"{displayName}",
                            Font = new Font("Arial", 14, FontStyle.Bold),
                            Location = new Point(90, 13 + (74 * i)),
                            AutoSize = true
                        };
                        Label newLabel2 = new Label
                        {
                            Text = $"{description}",
                            Location = new Point(90, 43 + (74 * i)),
                            Font = new Font("Arial", 9),
                            ForeColor = Color.Gray,
                            AutoSize = true
                        };
                        Label percent = new Label
                        {
                            Text = $"{displayNameFinoUgr}% of players have this achievement",
                            Location = new Point(90, 58 + (74 * i)),
                            Font = new Font("Arial", 9),
                            ForeColor = Color.Gray,
                            AutoSize = true
                        };
                        panel.Controls.Add(newPictureBox);
                        panel.Controls.Add(newLabel);
                        panel.Controls.Add(newLabel2);
                        panel.Controls.Add(percent);
                        i++;
                    }
                }
                else
                {
                    count_hidden++;
                }
            }
            return (i, count_hidden, unlocked_ach, locked_ach);
        }

        private void NewButton_Click(object sender, EventArgs e)
        {
            Panel oldPanel = this.Controls.OfType<Panel>().FirstOrDefault();
            if (oldPanel != null)
            {
                this.Controls.Remove(oldPanel);
                oldPanel.Dispose();
            }
            Button button = (Button)sender;
            string appid = button.Text;
            app_id = appid;
            Panel panel = new Panel
            {
                AutoScroll = true,
                Location = new Point(100, 5),
                Size = new Size(680, 450)
            };
            this.Controls.Add(panel);
            var ultra = Sort(appid, panel, false, 0);
            Sort(appid, panel, true, ultra.Item1);
            int count_hidden = ultra.Item2;
            int unlocked_ach = ultra.Item3;
            int locked_ach = ultra.Item4;
            label_information.Text = $"Hidden: {count_hidden}\nUnlocked: {unlocked_ach}\nLocked: {locked_ach}";
        }

        private void button1_Click(object sender, EventArgs e) // redownload
        {
            if (!string.IsNullOrWhiteSpace(app_id))
            {
                string path = Path.Combine(Settings.path, $"AchiviementsPhotos");
                var question = MessageBox.Show($"Are you sure you want to download files for the game number: {app_id}", "Achievements", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (question == DialogResult.Yes)
                {
                    var directory = Directory.GetFiles(path);
                    foreach (var file in directory)
                    {
                        if (Path.GetFileName(file).StartsWith(app_id))
                        {
                            File.Delete(file);
                        }
                    }
                    Achievements.DownloadAchievements(app_id);
                    MessageBox.Show("All pictures have been re-uploaded", "Achievements", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("First select a game", "Achievements", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
