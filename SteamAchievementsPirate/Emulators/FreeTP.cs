using SteamAchivmentsForPirates;

namespace SteamAchievementsPirate.Emulators
{
    public static class FreeTP
    {
        public static List<string> GetCountFreeTP(string path)
        {
            try
            {
                List<string> Local_Achivments = new List<string>();

                foreach (var achivment in Directory.GetFiles(path))
                {
                    Local_Achivments.Add(Path.GetFileName(achivment));
                }
                return Local_Achivments;
            }
            catch (Exception ex)
            {
                Settings.Exp(ex);
                return null;
            }
        }

        public static void FirstStart(string appid)
        {
            string path_combin = Path.Combine(Settings.path, $"{appid}_info.txt");
            var CounterDristos = GetCountFreeTP(File.ReadAllText(path_combin).Split('|')[0]);
            string local_path = Path.Combine(Settings.path, $"{appid}_achievements.txt");
            File.WriteAllText(local_path, string.Join("\r\n", CounterDristos));
        }

        public static void InfinityParserFreeTP(string appid)
        {
            while (Settings.ThreadIsStart)
            {
                Task.Delay(1000).Wait();
                string path_combin = Path.Combine(Settings.path, $"{appid}_info.txt");
                string local_path = Path.Combine(Settings.path, $"{appid}_achievements.txt");
                List<string> fck_line = new List<string>(File.ReadAllLines(local_path));
                var CounterDristos = GetCountFreeTP(File.ReadAllText(path_combin).Split('|')[0]);
                foreach (var one_achivment in CounterDristos)
                {
                    if (!fck_line.Contains(one_achivment))
                    {
                        string eblatoriy = one_achivment;
                        if (eblatoriy.Contains("\r"))
                        {
                            eblatoriy = eblatoriy.Replace("\r", "");
                        }
                        Console.WriteLine($"[debug] Получено достижение: {one_achivment}");
                        fck_line.Add(one_achivment);
                        File.WriteAllLines(local_path, fck_line);
                        Achievements.ShowAchivment(appid, eblatoriy);
                    }
                }
            }
        }

        public static bool ParseStage(string directory_from)
        {
            bool stage = false;
            if (Directory.Exists(directory_from))
            {
                foreach (var directory in Directory.GetDirectories(directory_from))
                {
                    foreach (var directory_games in Directory.GetDirectories(directory))
                    {
                        if (directory_games.Contains("FreeTP"))
                        {
                            string appid = File.ReadAllText(Path.Combine(directory, "steam_appid.txt"));
                            var path = Path.Combine(Settings.path, $"{appid}_info.txt");
                            string game = Achievements.GetAppName(appid);
                            string path_to_achivments = Path.Combine(directory_games, "Achievements");
                            Achievements.CreateCheme(appid);
                            File.WriteAllText(path, $"{path_to_achivments}|{game}|FreeTP|{appid}|{Settings.language}");
                            stage = true;
                        }
                    }
                }
                return true;
            }
            else
            {
                return false; // путь не найден
            }
        }

        public static bool Parse()
        {
            try
            {
                if (Settings.games_path.Count > 0)
                {
                    bool sovok = false;
                    foreach (var path in Settings.games_path)
                    {
                        if (!string.IsNullOrWhiteSpace(path))
                        {
                            bool ponos = ParseStage(path);
                            if (!sovok && ponos)
                            {
                                sovok = true;
                            }
                        }
                    }
                    return sovok;
                }
                else
                {
                    bool sovok = false;
                    foreach (var one_path in Settings.Path_Def_Games)
                    {
                        if (!sovok)
                        {
                            sovok = ParseStage(one_path);
                        }
                        else
                        {
                            ParseStage(one_path);
                        }
                    }
                    return sovok;
                }
            }
            catch (Exception ex)
            {
                Settings.Exp(ex);
                return false;
            }
        }
    }
}
