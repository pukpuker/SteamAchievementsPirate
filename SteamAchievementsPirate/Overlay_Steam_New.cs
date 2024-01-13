﻿using SteamAchievementsPirate.Properties;
using SteamAchivmentsForPirates;
using System.Media;
using System.Net;
using System.Runtime.InteropServices;
using Label = System.Windows.Forms.Label;

public class OverlaySteamNewForm : Form
{
    public static int location = 0;

    [DllImport("user32.dll")]
    static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
    const uint SWP_NOSIZE = 0x0001;
    const uint SWP_NOMOVE = 0x0002;
    Image image;

    System.Windows.Forms.Timer timer, timerHide;
    int endPosY;
    int DesktopPos = 30; // 30
    int WithOverlay = 0;

    public void ebatoriaya()
    {
        var local = Settings.overlay_location;
        if (local == "RU")
        {
            endPosY = Screen.PrimaryScreen.Bounds.Top;
            location = Screen.PrimaryScreen.Bounds.Top -50;
            this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - this.Width, location);
        }
        else if (local == "LU")
        {
            endPosY = Screen.PrimaryScreen.Bounds.Top;
            location = Screen.PrimaryScreen.Bounds.Top - 50;
            this.Location = new Point(Screen.PrimaryScreen.Bounds.Top - this.Top, location);
        }
        else if (local == "LD")
        {
            endPosY = Screen.PrimaryScreen.Bounds.Height - this.Height - WithOverlay;
            location = Screen.PrimaryScreen.Bounds.Height - this.Height - -50;
            this.Location = new Point(0, location);
        }
        else
        {
            endPosY = Screen.PrimaryScreen.Bounds.Height - this.Height - WithOverlay;
            location = Screen.PrimaryScreen.Bounds.Height - this.Height - -70;
            this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - this.Width, location);
        }
    }
    protected override CreateParams CreateParams
    {
        get
        {
            var cp = base.CreateParams;
            cp.ExStyle |= 0x02000000;    // Turn on WS_EX_COMPOSITED
            return cp;
        }
    }

    public OverlaySteamNewForm(string name, string description, string url)
    {
        Console.WriteLine("[+] Overlay: SteamNew");
        this.FormBorderStyle = FormBorderStyle.None;
        this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        this.ShowInTaskbar = false;
        this.TopMost = true;
        this.TopLevel = true;
        this.BackColor = Color.Transparent;
        this.Opacity = 1;
        this.Size = new Size(283, 70);
        this.StartPosition = FormStartPosition.Manual;
        this.DoubleBuffered = true;
        this.BackgroundImage = SteamAchievementsPirate.Properties.Resources.steam_photo;

        bool swinarnik = false;
        ebatoriaya();
        WebRequest request = WebRequest.Create(url);
        using (WebResponse response = request.GetResponse())
        using (Stream stream = response.GetResponseStream())
        {
            image = Image.FromStream(stream);
        }

        SoundPlayer audio = new SoundPlayer(SteamAchievementsPirate.Properties.Resources.desktop_toast_default);
        audio.Play();
        PictureBox pictureBoxBackground = new PictureBox
        {
            Image = SteamAchievementsPirate.Properties.Resources.steam_photo as Bitmap,
            SizeMode = PictureBoxSizeMode.AutoSize
        };
        PictureBox pictureBox = new PictureBox
        {
            Image = image,
            SizeMode = PictureBoxSizeMode.StretchImage,
            Dock = DockStyle.Left,
            Padding = new Padding(10, 12, 10, 12),
            Width = 64
        };
        this.Controls.Add(pictureBoxBackground);
        if (string.IsNullOrWhiteSpace(description))
        {
            Label labelik = new Label
            {
                Text = $"{name}",
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(69, 0, 0, 0)
            };
            this.Controls.Add(labelik);
        }
        else
        {
            Label label1 = new Label
            {
                Text = $"{name}",
                ForeColor = Color.White,
                Location = new Point(pictureBox.Width, 0), // Расположение label1 справа от pictureBox
                Size = new Size(this.Width - pictureBox.Width, this.Height / 2 - 8), // Уменьшаем высоту label1
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(0, 15, 0, 0)
            };
            Label label2 = new Label
            {
                Text = $"{description}",
                ForeColor = Color.Gray,
                Location = new Point(pictureBox.Width, label1.Height), // Расположение label2 ниже label1
                Size = new Size(this.Width - pictureBox.Width, this.Height / 2 + 10), // Увеличиваем высоту label2
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(0, 0, 0, 8)
            };
            this.Controls.Add(label2);
            this.Controls.Add(label1);
        }
        this.Controls.Add(pictureBox);
        pictureBoxBackground.SendToBack();
        timer = new System.Windows.Forms.Timer();
        timer.Interval = 1;
        if (Settings.overlay_location == "RU" || Settings.overlay_location == "LU")
        {
            timer.Tick += TimerUP_Tick;
        }
        else
        {
            timer.Tick += Timer_Tick;
        }
        timer.Start();
        this.Load += (sender, e) => {
            SetWindowPos(this.Handle, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE);
        };
    }

    private void TimerUP_Tick(object sender, EventArgs e)
    {
        if (this.Location.Y < endPosY)
        {
            MoveForm(5);
        }
        else
        {
            StopTimerAndStartHideTimer(TimerHideUP_Tick);
        }
    }

    private void TimerHideUP_Tick(object sender, EventArgs e)
    {
        if (this.Location.Y > Screen.PrimaryScreen.Bounds.Top - 50)
        {
            MoveForm(-5);
        }
        else
        {
            StopTimerAndCloseForm();
        }
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
        if (this.Location.Y > endPosY)
        {
            MoveForm(-5);
        }
        else
        {
            StopTimerAndStartHideTimer(TimerHide_Tick);
        }
    }

    private void TimerHide_Tick(object sender, EventArgs e)
    {
        if (this.Location.Y < Screen.PrimaryScreen.Bounds.Height)
        {
            MoveForm(5);
        }
        else
        {
            StopTimerAndCloseForm();
        }
    }

    private void MoveForm(int pixels)
    {
        this.Location = new Point(this.Location.X, this.Location.Y + pixels);
    }

    private void StopTimerAndStartHideTimer(EventHandler tickEvent)
    {
        timer.Stop();
        Task.Delay(5000).Wait();
        timerHide = new System.Windows.Forms.Timer();
        timerHide.Interval = 1;
        timerHide.Tick += tickEvent;
        timerHide.Start();
    }

    private void StopTimerAndCloseForm()
    {
        timerHide.Stop();
        this.Close();
    }

}
