using Newtonsoft.Json.Linq;
using SteamAchivmentsForPirates;

namespace SteamAchievementsPirate
{
    public partial class AchievementsForm : Form
    {
        public static string app_id = "";

        private bool isMouseDown = false;
        private Point mouseOffset;
        Panel panel_suka;
        Button button_suka;
        Label label_suka;

        public AchievementsForm()
        {
            InitializeComponent();
            this.MouseDown += new MouseEventHandler(Form1_MouseDown);
            this.MouseMove += new MouseEventHandler(Form1_MouseMove);
            this.MouseUp += new MouseEventHandler(Form1_MouseUp);
        }

        private void BackButton(object sender, EventArgs e)
        {
            this.Controls.Remove(button_suka);
            this.Controls.Remove(panel_suka);
            this.Controls.Remove(label_suka);
            panel1.Enabled = true;
            panel1.Visible = true;
        }

        private void AddedButon()
        {
            try
            {
                int sloy = 0;
                int list_pos = 0;
                for (int i = 0; i < SX.games_APPIDS.Count; i++)
                {
                    var path_combine = Path.Combine(Settings.path, "AchiviementsPhotos", $"{SX.games_APPIDS[i]}_header.jpg");
                    if (!File.Exists(path_combine))
                    {
                        Achievements.ugar.DownloadFile($"https://steamcdn-a.akamaihd.net/steam/apps/{SX.games_APPIDS[i]}/header.jpg", path_combine);
                    }
                    if (!File.Exists($"{Settings.path}\\{SX.games_APPIDS[i]}.txt"))
                    {
                        Achievements.CreateCheme(SX.games_APPIDS[i]);
                    }
                    //button
                    Button button = new Button();
                    button.BackgroundImage = Image.FromFile(path_combine);
                    button.BackgroundImageLayout = ImageLayout.Stretch;
                    button.FlatStyle = FlatStyle.Flat;
                    button.FlatAppearance.BorderSize = 0;
                    button.BackColor = Color.Transparent;
                    button.Click += NewButton_Click;
                    button.Tag = SX.games_APPIDS[i];
                    button.Size = new Size(200, 93);
                    //button
                    //label
                    Label label = new Label();
                    string game_name = File.ReadAllText($"{Settings.path}\\{SX.games_APPIDS[i]}_info.txt").Split('|')[1];
                    label.Text = $"{game_name}";
                    label.ForeColor = Color.White;
                    label.BackColor = Color.Transparent;
                    label.AutoSize = true;
                    //label
                    if ((i % 4 == 0) && i != 0)
                    {
                        sloy++;
                        list_pos = 0;
                    }
                    if (sloy == 0)
                    {
                        button.Location = new Point(210 * list_pos, sloy);
                        label.Location = new Point(210 * list_pos, 100);
                        list_pos++;
                    }
                    else
                    {
                        button.Location = new Point(210 * list_pos, sloy + 130);
                        label.Location = new Point(210 * list_pos, sloy + 230);
                        list_pos++;
                    }
                    panel1.Controls.Add(button);
                    panel1.Controls.Add(label);
                }
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Unknown Appid (file in 'games' folder", "SAP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                Settings.Exp(ex);
            }
        }
        private void NewButton_Click(object sender, EventArgs e)
        {
            Button SenderButton = (Button)sender;
            var appid = SenderButton.Tag.ToString();
            app_id = appid;
            // off
            panel1.Enabled = false;
            panel1.Visible = false;
            // off
            Panel panel = new Panel
            {
                AutoScroll = true,
                Location = new Point(10, 40),
                Size = new Size(830, 400),
                BorderStyle = BorderStyle.None
            };
            panel_suka = panel;
            //button
            Button back_button = new Button();
            back_button.Text = "Back to menu";
            back_button.ForeColor = Color.White;
            back_button.Location = new Point(670, 5);
            back_button.AutoSize = true;
            back_button.Click += BackButton;
            button_suka = back_button;
            //button

            //label_information
            Label information_label = new Label();
            information_label.ForeColor = Color.White;
            information_label.Location = new Point(10, 10);
            information_label.AutoSize = true;  
            label_suka = information_label;
            //label_information
            this.Controls.Add(back_button);
            this.Controls.Add(panel);
            this.Controls.Add(information_label);
            label_suka.MouseDown += new MouseEventHandler(Form1_MouseDown);
            label_suka.MouseMove += new MouseEventHandler(Form1_MouseMove);
            label_suka.MouseUp += new MouseEventHandler(Form1_MouseUp);
            var ultra = Sort(appid, panel, false, 0);
            Sort(appid, panel, true, ultra.Item1);
            int count_hidden = ultra.Item2;
            int unlocked_ach = ultra.Item3;
            int locked_ach = ultra.Item4;
            label_suka.Text = $"{File.ReadAllText($"{Settings.path}\\{app_id}_info.txt").Split('|')[1]}                                                           Hidden: {count_hidden} | Unlocked: {unlocked_ach} | Locked: {locked_ach}"; // lol
        }

        private (int, int, int, int) Sort(string appid, Panel panel, bool locked, int i)
        {
            try
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
                    Achievements.DownloadAchievements(appid);
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
                    if (File.Exists(local_path))
                    {
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
                    }
                    else
                    {
                        photo_path = Path.Combine(Settings.path, $"AchiviementsPhotos", $"{appid}_{name}_gray.jpg");
                        locked_ach++;
                        local_locked = 1;
                    }
                    if (hidden == 0 || (!locked && local_locked == 0))
                    {
                        if (locked && local_locked == 1)
                        {
                            PictureBox newPictureBox = new PictureBox
                            {
                                Size = new Size(64, 64),
                                Location = new Point(10, 10 + (100 * i)),
                                Image = Image.FromFile(photo_path),
                                SizeMode = PictureBoxSizeMode.StretchImage
                            };
                            Label newLabel = new Label
                            {
                                Text = $"{displayName}",
                                Font = new Font("Arial", 14),
                                Location = new Point(90, 13 + (100 * i)),
                                ForeColor = Color.FromArgb(203, 205, 207),
                                BackColor = Color.FromArgb(35, 38, 46),
                                AutoSize = true
                            };
                            Label newLabel2 = new Label
                            {
                                Text = $"{description}",
                                Location = new Point(90, 43 + (100 * i)),
                                Font = new Font("Arial", 9),
                                ForeColor = Color.FromArgb(184, 188, 191),
                                BackColor = Color.FromArgb(35, 38, 46),
                                AutoSize = true
                            };
                            Label ACHIVEMENTBOX = new Label
                            {
                                Location = new Point(0, 0 + (100* i)),
                                Size = new Size(800, 83),
                                BackColor = Color.FromArgb(35, 38, 46),
                            };
                            panel.Controls.Add(newPictureBox);
                            panel.Controls.Add(newLabel);
                            panel.Controls.Add(newLabel2);
                            panel.Controls.Add(ACHIVEMENTBOX);
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
                            if (displayNameFinoUgr <= 10)
                            {
                                // rare
                            }
                            //PictureBox Rare = new PictureBox
                            //{
                            //    Size = new Size(80, 80),
                            //    Location = new Point(0, 10 + (74 * i)),
                            //    Image = Image.FromFile("final.gif"),
                            //    Padding = new Padding(0, 0, 0, 0),
                            //    SizeMode = PictureBoxSizeMode.AutoSize
                            //};

                            Label ACHIVEMENTBOX = new Label
                            {
                                Location = new Point(0, 0 + (100 * i)),
                                Size = new Size(800, 83),
                                BackColor = Color.FromArgb(35, 38, 46),
                            };
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
                                ForeColor = Color.FromArgb(203, 205, 207),
                                Font = new Font("Arial", 14, FontStyle.Bold),
                                Location = new Point(90, 13 + (74 * i)),
                                AutoSize = true
                            };
                            Label newLabel2 = new Label
                            {
                                Text = $"{description}",
                                Location = new Point(90, 43 + (74 * i)),
                                Font = new Font("Arial", 9),
                                ForeColor = Color.FromArgb(184, 188, 191),
                                AutoSize = true
                            };
                            Label percent = new Label
                            {
                                Text = $"{displayNameFinoUgr}% of players have this achievement",
                                Location = new Point(90, 58 + (74 * i)),
                                Font = new Font("Arial", 9),
                                ForeColor = Color.FromArgb(139, 146, 154),
                                AutoSize = true
                            };
                            panel.Controls.Add(newPictureBox);
                            panel.Controls.Add(newLabel);
                            panel.Controls.Add(newLabel2);
                            panel.Controls.Add(percent);
                            panel.Controls.Add(ACHIVEMENTBOX);
                            //panel.Controls.Add(Rare);
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
            catch (FileNotFoundException ex)
            {
                Achievements.DownloadAchievements(appid);
                Sort(appid, panel, locked, i);
                return (0, 0, 0, 0);
            }
            catch (Exception ex)
            {
                Settings.Exp(ex);
                return (0, 0, 0, 0);
            }
        }

        private void AchievementsForm_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(14, 20, 27);
            //exitbutton
            exitbutton.FlatStyle = FlatStyle.Flat;
            exitbutton.FlatAppearance.BorderSize = 0;
            exitbutton.BackColor = Color.Transparent;
            exitbutton.ForeColor = Color.FromArgb(182, 185, 189);
            exitbutton.FlatAppearance.MouseOverBackColor = exitbutton.BackColor;
            exitbutton.FlatAppearance.MouseDownBackColor = exitbutton.BackColor;
            //exitbutton
            //minimizebutton
            minimizebutton.FlatStyle = FlatStyle.Flat;
            minimizebutton.FlatAppearance.BorderSize = 0;
            minimizebutton.BackColor = Color.Transparent;
            minimizebutton.ForeColor = Color.FromArgb(182, 185, 189);
            minimizebutton.FlatAppearance.MouseOverBackColor = exitbutton.BackColor;
            minimizebutton.FlatAppearance.MouseDownBackColor = exitbutton.BackColor;
            //minimizebutton
            //mainpanel
            panel1.Focus();
            panel1.BorderStyle = BorderStyle.None;
            panel1.AutoScroll = true;
            //mainpanel
            AddedButon();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int xOffset = -e.X;
                int yOffset = -e.Y;
                mouseOffset = new Point(xOffset, yOffset);
                isMouseDown = true;
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouseOffset.X, mouseOffset.Y);
                Location = mousePos;
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void minimizebutton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
