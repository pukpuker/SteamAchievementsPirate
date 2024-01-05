using SteamAchievementsPirate;

namespace SteamAchivmentsForPirates
{
    public static class SX
    {
        public static List<string> games_APPIDS = new List<string>();
        public static List<string> Codex_appids = new List<string>();
        public static List<string> FreeTP_appids = new List<string>();

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
                    games = games + $"{app_name} - {emu}\n";
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
        }

        public static void Main()
        {
            Settings.ChangeTitle();
            Settings.SettingsParser();
            string games = Games();
            if (string.IsNullOrWhiteSpace(games))
            {
                Achivments.ParsingGames();
                Main();
            }
            Thread Start = new Thread(StartThreads);
            Start.Start();
            Console.Write($"Actions: \n[parse] - parse games in PC\n[freetp_path] - update freetp.org games folder\n[achiv] - activate 'form' with all achievement\n\nGames:\n{games}\nInput: ");
            string? action = Console.ReadLine();
            if (action == "parse")
            {
                Actions.Parse();
            }
            else if (action == "freetp_path")
            {
                Actions.FreeTP_Path();
            }
            else if (action == "achiv")
            {
                Actions.MyAchivment();
            }
            //Achivments.ShowAchivment("641990", "OpenPrison");
        }
    }
}
