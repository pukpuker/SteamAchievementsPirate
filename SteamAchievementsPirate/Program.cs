using SteamAchievementsPirate;
using System.Runtime.InteropServices;

namespace SteamAchivmentsForPirates
{
    public static class SX
    {
        public static List<string> games_APPIDS = new List<string>();
        public static List<string> Codex_appids = new List<string>();
        public static List<string> FreeTP_appids = new List<string>();
        public static Thread StartThreadsThread = new Thread(StartThreads);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

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

        public static void Main()
        {
#if DEBUG
            AllocConsole();
            Settings.ChangeTitle();
#endif
            Settings.SettingsParser();
            string games = Games();
            if (string.IsNullOrWhiteSpace(games))
            {
                Settings.HaveGames = Achievements.ParsingGames();
                if (Settings.HaveGames)
                {
                    Settings.HaveGames = true;
                    StartParser();
                }
                else
                {
                    MessageBox.Show("The program could not detect the games. The program may not support the Steam game emulator. Try downloading another game repack (or crack) or try setting the path to the games in the settings", "SAP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                Settings.HaveGames = true;
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
