using Newtonsoft.Json.Linq;
using SteamAchivmentsForPirates;

namespace SteamAchievementsPirate
{
    public partial class achievement_vitrina : Form
    {
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
            string json = "";
            if (File.Exists(path))
            {
                json = File.ReadAllText(path);
            }
            else
            {
                Achievements.CreateCheme(appid);
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
            //var path = Path.Combine(Settings.path, $"{appid}.txt");
            //string json = "";
            //if (File.Exists(path))
            //{
            //    json = File.ReadAllText(path);
            //}
            //else
            //{
            //    Achievements.CreateCheme(appid);
            //}
            //JObject obj = JObject.Parse(json);
            //JArray achievements = (JArray)obj["game"]["availableGameStats"]["achievements"];
            //int Count = achievements.Count;
            Panel panel = new Panel
            {
                AutoScroll = true,
                Location = new Point(100, 5),
                Size = new Size(680, 450)
            };
            this.Controls.Add(panel);
            var ultra = Sort(appid, panel, false, 0);
            Sort(appid, panel, true, ultra.Item1);
            //int i = 0;
            int count_hidden = ultra.Item2;
            int unlocked_ach = ultra.Item3;
            int locked_ach = ultra.Item4;
            //foreach (JObject achievement in achievements)
            //{
            //    int local_locked_or_no = 0;
            //    //
            //    string name = (string)achievement["name"];
            //    string displayName = (string)achievement["displayName"];
            //    string description = (string)achievement["description"];
            //    int hidden = (int)achievement["hidden"];
            //    //

            //    string photo_path = "";

            //    string local_path = Path.Combine(Settings.path, $"{appid}_achievements.txt");
            //    List<string> fck_line = new List<string>(File.ReadAllLines(local_path));
            //    if (fck_line.Contains(name))
            //    {
            //        photo_path = Path.Combine(Settings.path, $"AchiviementsPhotos", $"{appid}_{name}_gray.jpg");
            //        locked_ach++;
            //        local_locked_or_no = 1;
            //    }
            //    else
            //    {
            //        photo_path = Path.Combine(Settings.path, $"AchiviementsPhotos", $"{appid}_{name}_default.jpg");
            //        unlocked_ach++;
            //    }
            //    if (hidden == 0)
            //    {
            //        PictureBox newPictureBox = new PictureBox
            //        {
            //            Size = new Size(64, 64),
            //            Location = new Point(10, 10 + (74 * i)),
            //            Image = Image.FromFile(photo_path),
            //            SizeMode = PictureBoxSizeMode.StretchImage
            //        };
            //        Label newLabel = new Label
            //        {
            //            Text = $"{displayName}",
            //            Font = new Font("Arial", 14, FontStyle.Bold),
            //            Location = new Point(90, 13 + (74 * i)),
            //            AutoSize = true
            //        };
            //        Label newLabel2 = new Label
            //        {
            //            Text = $"{description}",
            //            Location = new Point(90, 43 + (74 * i)),
            //            Font = new Font("Arial", 8),
            //            AutoSize = true
            //        };
            //        panel.Controls.Add(newPictureBox);
            //        panel.Controls.Add(newLabel);
            //        panel.Controls.Add(newLabel2);
            //        i++;
            //    }
            //    else
            //    {
            //        count_hidden++;
            //    }
            //}
            label_information.Text = $"Hidden: {count_hidden}\nUnlocked: {unlocked_ach}\nLocked: {locked_ach}";
        }
    }
}
