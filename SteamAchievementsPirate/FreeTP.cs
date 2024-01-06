using SteamAchivmentsForPirates;

namespace SteamAchievementsPirate
{
    public static class FreeTP
    {
        public static List<string> GetCountFreeTP(string path)
        {
            try
            {
                List<string> Local_Achivments = new System.Collections.Generic.List<string>();

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
            while (true)
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

        public static void Parse() // добавить возможность массив путей, т.е. несколько путей в параметрах.
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Settings.freetp_path))
                {
                    foreach (var directory in Directory.GetDirectories(Settings.freetp_path))
                    {
                        foreach (var directory_games in Directory.GetDirectories(directory))
                        {
                            if (directory_games.Contains("FreeTP"))
                            {
                                string appid = File.ReadAllText(Path.Combine(directory, "steam_appid.txt"));
                                var path = Path.Combine(Settings.path, $"{appid}_info.txt");
                                string game = Achievements.GetAppName(appid);
                                string path_to_achivments = Path.Combine(directory_games, "Achievements");
                                File.WriteAllText(path, $"{path_to_achivments}|{game}|FreeTP|{appid}");
                                Achievements.CreateCheme(appid);
                            }
                        }
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("FreeTP Path is NULL. The search will only be performed on the paths: \"C:\\Games\" and \"D:\\Games\". Do you want to specify the search path for FreeTP games?");
                    Console.Write("\nInput 'y' or 'n': ");
                    string ugar = Console.ReadLine();
                    if (ugar.Contains("y"))
                    {
                        Actions.FreeTP_Path();
                    }
                    else
                    {
                        foreach (var one_path in Settings.Path_Def_Games)
                        {
                            foreach (var directory in Directory.GetDirectories(one_path))
                            {
                                foreach (var directory_games in Directory.GetDirectories(directory))
                                {
                                    if (directory_games.Contains("FreeTP"))
                                    {
                                        string appid = File.ReadAllText(Path.Combine(directory, "steam_appid.txt"));
                                        var path = Path.Combine(Settings.path, $"{appid}_info.txt");
                                        string game = Achievements.GetAppName(appid);
                                        string path_to_achivments = Path.Combine(directory_games, "Achievements");
                                        File.WriteAllText(path, $"{path_to_achivments}|{game}|FreeTP|{appid}");
                                        Achievements.CreateCheme(appid);
                                    }
                                }
                            }
                        }
                    }
                    Console.Clear();
                }
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine("U set incorrect folder. Trying to setup default path... You can change FreeTP path in .env");
                Settings.UpdateValue("freetp_path", "");
                Console.WriteLine("Setup to default. After reboot application, type 'parse' to parse FreeTP Games. Press Enter to Exit.");
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                Settings.Exp(ex);
            }
        }
    }
}
