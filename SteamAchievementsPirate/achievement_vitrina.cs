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
    public partial class achievement_vitrina : Form
    {
        public achievement_vitrina()
        {
            InitializeComponent();
        }

        private void achievement_vitrina_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < SX.Codex_appids.Count; i++)
            {
                Button newButton = new Button
                {
                    Text = SX.Codex_appids[i],
                    Location = new Point(10, 10 + (30 * i))
                };
                newButton.Click += NewButton_Click;
                this.Controls.Add(newButton);
            }
        }
        private void NewButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (button.Text == "")
            {
                // Создание списка с путями к изображениям
                List<string> imagePaths = new List<string>
{
    "C:\\путь\\к\\изображению1.jpg",
    "C:\\путь\\к\\изображению2.jpg",
    "C:\\путь\\к\\изображению3.jpg"
    // добавьте сюда все ваши пути к изображениям
};

                // Создание списка с текстом для каждого изображения
                List<string> imageTexts = new List<string>
{
    "Текст 1",
    "Текст 2",
    "Текст 3"
    // добавьте сюда все ваши тексты
};

                // Создание Panel с автопрокруткой
                Panel panel = new Panel
                {
                    AutoScroll = true,
                    Location = new Point(10, 10),
                    Size = new Size(200, 200) // Размеры Panel
                };
                this.Controls.Add(panel);

                // Создание и добавление PictureBox и Label на Panel
                for (int i = 0; i < imagePaths.Count; i++)
                {
                    PictureBox newPictureBox = new PictureBox
                    {
                        Size = new Size(100, 100), // Размеры PictureBox
                        Location = new Point(10, 10 + (110 * i)), // Расположение PictureBox друг под другом с отступом в 10 пикселей
                        Image = Image.FromFile(imagePaths[i]), // Загрузка изображения
                        SizeMode = PictureBoxSizeMode.StretchImage // Растягивание изображения для заполнения PictureBox
                    };
                    panel.Controls.Add(newPictureBox);

                    Label newLabel = new Label
                    {
                        Text = imageTexts[i],
                        Location = new Point(120, 10 + (110 * i)) // Расположение Label рядом с PictureBox
                    };
                    panel.Controls.Add(newLabel);
                }

            }
        }
    }
}
