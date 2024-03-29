using System.Media;
using System.Net;
using System.Runtime.InteropServices;
using Label = System.Windows.Forms.Label;

public class OverlayForm : Form
{

    [DllImport("user32.dll")]
    static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
    const uint SWP_NOSIZE = 0x0001;
    const uint SWP_NOMOVE = 0x0002;

    System.Windows.Forms.Timer timer, timerHide;
    int endPosY;
    int DesktopPos = 30; // 30
    int WithOverlay = 0;
    public OverlayForm(string name, string description, string url)
    {
        Console.WriteLine("[+] Overlay: SteamOld");
        this.FormBorderStyle = FormBorderStyle.None;
        this.ShowInTaskbar = false;
        this.TopMost = true;
        this.TopLevel = true;
        this.BackColor = ColorTranslator.FromHtml("#21252D");
        this.Opacity = 1;
        this.Size = new Size(282, 69);
        this.StartPosition = FormStartPosition.Manual;
        bool swinarnik = false;
        //try
        //{
        //    swinarnik = Achivments.GameIsZoomed();
        //}
        //catch (Exception ex)
        //{
        //    swinarnik = false;
        //    Console.WriteLine(ex.Message);
        //}
        //if (swinarnik)
        //{
        //    endPosY = Screen.PrimaryScreen.Bounds.Height - this.Height - DesktopPos;
        //}
        endPosY = Screen.PrimaryScreen.Bounds.Height - this.Height - WithOverlay;

        //endPosY = Screen.PrimaryScreen.Bounds.Height - this.Height - 30;
        this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - this.Width, Screen.PrimaryScreen.Bounds.Height - this.Height - -50); // ��������� �������� Y, ����� ������� ���� ����


        WebRequest request = WebRequest.Create(url);
        Image image;
        using (WebResponse response = request.GetResponse())
        using (Stream stream = response.GetResponseStream())
        {
            image = Image.FromStream(stream);
        }
        SoundPlayer audio = new SoundPlayer(SteamAchievementsPirate.Properties.Resources.desktop_toast_default);
        audio.Play();
        PictureBox pictureBox = new PictureBox
        {
            Image = image,
            SizeMode = PictureBoxSizeMode.StretchImage,
            Dock = DockStyle.Left,
            Padding = new Padding(10, 12, 10, 12),
            Width = 64
        };
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
                Location = new Point(pictureBox.Width, 0), // ������������ label1 ������ �� pictureBox
                Size = new Size(this.Width - pictureBox.Width, this.Height / 2 - 10), // ��������� ������ label1
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(0, 10, 0, 0)
            };
            Label label2 = new Label
            {
                Text = $"{description}",
                ForeColor = Color.Gray,
                Location = new Point(pictureBox.Width, label1.Height), // ������������ label2 ���� label1
                Size = new Size(this.Width - pictureBox.Width, this.Height / 2 + 10), // ����������� ������ label2
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(0, 0, 0, 5)
            };
            this.Controls.Add(label2);
            this.Controls.Add(label1);
        }
        this.Controls.Add(pictureBox);
        timer = new System.Windows.Forms.Timer();
        timer.Interval = 1;
        timer.Tick += Timer_Tick;
        timer.Start();
        this.Load += (sender, e) => {
            SetWindowPos(this.Handle, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE);
        };
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
            this.Close();
        }
    }
}
