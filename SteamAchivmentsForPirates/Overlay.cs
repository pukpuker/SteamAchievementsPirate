using System;
using System.Drawing;
using System.Windows.Forms;

public class OverlayForm : Form
{
    System.Windows.Forms.Timer timer, timerHide;
    int endPosY;
    public OverlayForm()
    {
        this.FormBorderStyle = FormBorderStyle.None;
        this.ShowInTaskbar = false;
        this.TopMost = true;
        this.BackColor = ColorTranslator.FromHtml("#21252D");
        this.Opacity = 1;
        this.Size = new Size(282, 69); // Установите размер окна
        this.StartPosition = FormStartPosition.Manual;
        endPosY = Screen.PrimaryScreen.Bounds.Height - this.Height - 30;
        this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - this.Width, Screen.PrimaryScreen.Bounds.Height - this.Height - -50); // Уменьшите значение Y, чтобы поднять окно выше

        PictureBox pictureBox = new PictureBox
        {
            Image = Image.FromFile("1.jpg"), // Замените на путь к вашему изображению
            SizeMode = PictureBoxSizeMode.StretchImage,
            Dock = DockStyle.Left,
            Padding = new Padding(10, 12, 10, 12),
            Width = 64 // Ширина изображения
        };

        Label label = new Label
        {
            Text = "Новое начало\nCounter-Strike? Дайте два.", // Замените на ваш текст
            ForeColor = Color.White,
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleLeft,
            Padding = new Padding(69, 0, 0, 0)
        };
        this.Controls.Add(pictureBox);
        this.Controls.Add(label);
        timer = new System.Windows.Forms.Timer();
        timer.Interval = 1; // Установите интервал таймера
        timer.Tick += Timer_Tick;
        timer.Start();
    }
    private void Timer_Tick(object sender, EventArgs e)
    {
        if (this.Location.Y > endPosY)
        {
            this.Location = new Point(this.Location.X, this.Location.Y - 5); // Поднимите окно на 5 пикселей
        }
        else
        {
            timer.Stop();
            Task.Delay(5000).Wait();
            timerHide = new System.Windows.Forms.Timer();
            timerHide.Interval = 1; // Установите интервал таймера
            timerHide.Tick += TimerHide_Tick;
            timerHide.Start();
        }
    }

    private void TimerHide_Tick(object sender, EventArgs e)
    {
        if (this.Location.Y < Screen.PrimaryScreen.Bounds.Height)
        {
            this.Location = new Point(this.Location.X, this.Location.Y + 5); // Опустите окно на 5 пикселей
        }
        else
        {
            timerHide.Stop();
            Application.Exit();
        }
    }
}
