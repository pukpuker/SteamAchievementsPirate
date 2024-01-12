using Newtonsoft.Json.Linq;
using SteamAchievementsPirate;
using System.Diagnostics;
using System.Net;

namespace SteamAchivmentsForPirates
{
    public static class SX
    {
        public static List<string> games_APPIDS = new List<string>();
        public static List<string> Codex_appids = new List<string>();
        public static List<string> Rune_appids = new List<string>();
        public static List<string> FreeTP_appids = new List<string>();

        public static Thread StartThreadsThread = new Thread(StartThreads);

        public static void GetLastVersion()
        {
            try
            {
                if (!Settings.debug)
                {
                    WebClient client = new WebClient();
                    client.Headers.Set("User-Agent", "request");
                    string json = client.DownloadString("https://api.github.com/repos/pukpuker/SteamAchievementsPirate/releases/latest");
                    JObject obj = JObject.Parse(json);
                    string git_version_latest = (string)obj["tag_name"];
                    if (git_version_latest != Settings.version)
                    {
                        var result = MessageBox.Show("A new version of the program has been released. Do you want to upgrade?", "SAP", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (result == DialogResult.Yes)
                        {
                            MessageBox.Show(Settings.debug.ToString());
                            Process.Start("explorer.exe", "https://github.com/pukpuker/SteamAchievementsPirate/releases/");
                            Environment.Exit(0);
                        }
                    }
                }
            }
            catch (WebException) // if ratelimit (403)
            {

            }
            catch (Exception ex)
            {
                Settings.Exp(ex);
            }
        }

        public static string Games()
        {
            string games = "";

            foreach (var game in Directory.GetFiles(Settings.path))
            {
                if (game.Contains("info"))
                {
                    string file_info = File.ReadAllText(game);
                    var splitted = file_info.Split('|');
                    string pathik = splitted[0];
                    string app_name = splitted[1];
                    string emu = splitted[2];
                    string app_id = splitted[3];
                    games_APPIDS.Add(app_id);
                    games = games + $"{app_name} - {emu} - {app_id}\n";
                    if (emu == "CODEX")
                    {
                        Codex_appids.Add(app_id);
                    }
                    else if (emu == "RUNE")
                    {
                        Rune_appids.Add(app_id);
                    }
                    else if (emu == "FreeTP")
                    {
                        FreeTP_appids.Add(app_id);
                    }
                }
            }
            return games;
        }

        public static void StartThreads()
        {
            foreach (var app_id in Codex_appids)
            {
                Codex.FirstStart(app_id);
                Task.Delay(300).Wait();
                var thread_InfinityParser = new Thread(() => Codex.InfinityParserCodex(app_id));
                thread_InfinityParser.Start();
            }
            foreach (var app_id in Rune_appids)
            {
                Codex.FirstStart(app_id);
                Task.Delay(300).Wait();
                var thread_InfinityParser = new Thread(() => Rune.InfinityParserRune(app_id));
                thread_InfinityParser.Start();
            }
            foreach (var app_id in FreeTP_appids)
            {
                FreeTP.FirstStart(app_id);
                Task.Delay(300).Wait();
                var thread_InfinityParser = new Thread(() => FreeTP.InfinityParserFreeTP(app_id));
                thread_InfinityParser.Start();
            }
            Settings.StartThreads = true;
        }

        public static void StartParser()
        {
            if (Settings.StartThreads && Settings.HaveGames)
            {
                StartThreadsThread.Start();
            }
        }

        public static void Main(string[] args = null)
        {
#if DEBUG
            Settings.AllocConsole();
            Settings.ChangeTitle();
#endif
            if (args.Length > 0 && args[0] == "autostart")
            {
                Settings.FromAutoRun = true;
            }
            System.IO.Directory.SetCurrentDirectory(System.AppDomain.CurrentDomain.BaseDirectory);
            Settings.SettingsParser();
            GetLastVersion();
            Achievements.ParsingGames();
            string games = Games();
            if (string.IsNullOrWhiteSpace(games))
            {
                Settings.HaveGames = Achievements.ParsingGames();
                if (!Settings.HaveGames)
                {
                    MessageBox.Show("The program could not detect the games. The program may not support the Steam game emulator. Try downloading another game repack (or crack) or try setting the path to the games in the settings", "SAP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                Settings.HaveGames = true;
            }
            if (Settings.StartThreads)
            {
                StartParser();
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Thread t = new Thread(() =>
            {
                Application.Run(new MainForm());
            });
            t.Start();
        }
    }
}
