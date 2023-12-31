using System;
using System.Drawing;
using System.Media;
using System.Net;
using System.Numerics;
using System.Windows.Forms;

public class OverlayForm : Form
{
    System.Windows.Forms.Timer timer, timerHide;
    int endPosY;
    public OverlayForm(string name, string description, string url)
    {
        this.FormBorderStyle = FormBorderStyle.None;
        this.ShowInTaskbar = false;
        this.TopMost = true;
        this.BackColor = ColorTranslator.FromHtml("#21252D");
        this.Opacity = 1;
        this.Size = new Size(282, 69);
        this.StartPosition = FormStartPosition.Manual;
        endPosY = Screen.PrimaryScreen.Bounds.Height - this.Height - 30;
        this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - this.Width, Screen.PrimaryScreen.Bounds.Height - this.Height - -50); // Уменьшите значение Y, чтобы поднять окно выше


        WebRequest request = WebRequest.Create(url);
        Image image;
        using (WebResponse response = request.GetResponse())
        using (Stream stream = response.GetResponseStream())
        {
            image = Image.FromStream(stream);
        }
        SoundPlayer player = new SoundPlayer();
        player.SoundLocation = "desktop_toast_default.wav";
        player.Load();
        player.Play();
        PictureBox pictureBox = new PictureBox
        {
            Image = image,
            SizeMode = PictureBoxSizeMode.StretchImage,
            Dock = DockStyle.Left,
            Padding = new Padding(10, 12, 10, 12),
            Width = 64
        };

        Label label = new Label
        {
            Text = $"{name}\n{description}",
            ForeColor = Color.White,
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleLeft,
            Padding = new Padding(69, 0, 0, 0)
        };
        this.Controls.Add(pictureBox);
        this.Controls.Add(label);
        timer = new System.Windows.Forms.Timer();
        timer.Interval = 1;
        timer.Tick += Timer_Tick;
        timer.Start();
    }
    private void Timer_Tick(object sender, EventArgs e)
    {
        if (this.Location.Y > endPosY)
        {
            this.Location = new Point(this.Location.X, this.Location.Y - 5);
        }
        else
        {
            timer.Stop();
            Task.Delay(5000).Wait();
            timerHide = new System.Windows.Forms.Timer();
            timerHide.Interval = 1;
            timerHide.Tick += TimerHide_Tick;
            timerHide.Start();
        }
    }

    private void TimerHide_Tick(object sender, EventArgs e)
    {
        if (this.Location.Y < Screen.PrimaryScreen.Bounds.Height)
        {
            this.Location = new Point(this.Location.X, this.Location.Y + 5);
        }
        else
        {
            timerHide.Stop();
            Application.Exit();
        }
    }
}
