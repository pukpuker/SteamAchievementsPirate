﻿using SteamAchievementsPirate;

namespace SteamAchivmentsForPirates
{
    public static class SX
    {
        static List<string> games_APPIDS = new List<string>();
        public static int CODEX = 0;
        public static int FreeTP = 0;

        public static string Games()
        {
            string games = "";

            foreach (var game in Directory.GetFiles(Settings.path))
            {
                if (game.Contains("info"))
                {
                    string file_info = File.ReadAllText(game);
                    var splitted = file_info.Split('|');
                    string app_name = splitted[1];
                    string emu = splitted[2];
                    string app_id = splitted[3];
                    games_APPIDS.Add(app_id);
                    games = games + $"{app_name} - {emu}\n";
                    if (emu == "CODEX")
                    {
                        CODEX++;
                    }
                    else if (emu == "FreeTP")
                    {
                        FreeTP++;
                    }
                }
            }
            return games;
        }

        public static void StartThreads()
        {
            foreach (var app_id in games_APPIDS)
            {
                Achivments.FirstStart(app_id);
                Task.Delay(300).Wait();
                var thread_InfinityParser = new Thread(() => Achivments.InfinityParserCodex(app_id));
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
            Console.Write($"Actions: \n[parse] - parse games in PC\n[freetp] - uppdate freetp games folder \n\nGames:\n{games}\nInput: ");
            string? action = Console.ReadLine();
            if (action == "parse")
            {
                Actions.Parse();
            }
            else if (action == "freetp")
            {
                Actions.FreeTP_Path();
            }
            else if (action == "demo")
            {
                Achivments.FreeTP();
            }
            //Achivments.ShowAchivment("641990", "OpenPrison");
        }
    }
}
