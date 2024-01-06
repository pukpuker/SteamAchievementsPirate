using Newtonsoft.Json.Linq;
using SteamAchivmentsForPirates;
using System.Reflection.Metadata;

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

        private List<string> GettingUnlockedAchievements()
        {
            try
            {
                List<string> images = new List<string>();
                return images;
            }
            catch (Exception ex)
            {
                Settings.Exp(ex);
                return null;
            }
        }

        private void NewButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            List<string> imagePaths = new List<string>
                {
                    "1.jpg",
                    "1.jpg",
                    "1.jpg"
                };

            List<string> imageTexts = new List<string>
                {
                    "Текст 1",
                    "Текст 2",
                    "Текст 3"
                };

            Panel panel = new Panel
            {
                AutoScroll = true,
                Location = new Point(10, 10),
                Size = new Size(200, 200)
            };
            this.Controls.Add(panel);

            for (int i = 0; i < imagePaths.Count; i++)
            {
                PictureBox newPictureBox = new PictureBox
                {
                    Size = new Size(100, 100), // Размеры PictureBox
                    Location = new Point(10, 10 + (110 * i)), // Расположение PictureBox друг под другом с отступом в 10 пикселей
                    Image = Image.FromFile(imagePaths[i]),
                    SizeMode = PictureBoxSizeMode.StretchImage
                };
                panel.Controls.Add(newPictureBox);

                Label newLabel = new Label
                {
                    Text = imageTexts[i],
                    Location = new Point(120, 10 + (110 * i))
                };
                panel.Controls.Add(newLabel);
            }
        }
    }
}
