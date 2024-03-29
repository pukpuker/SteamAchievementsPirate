﻿using SteamAchivmentsForPirates;

namespace SteamAchievementsPirate.Emulators
{
    public static class Rune
    {
        public static (int, List<string>) GetCountRune(string path)
        {
            try
            {
                int count = 0;
                List<string> Local_Achivments = new List<string>();

                string xer = null;
                string file = File.ReadAllText(path);
                var xuina = file.Split('[');
                foreach (var xuina14 in xuina)
                {
                    if (xuina14.StartsWith("SteamAchievements"))
                    {
                        foreach (var pizdos in xuina14.Split('\n'))
                        {
                            if (!pizdos.StartsWith("SteamAchievements]"))
                            {
                                xer = pizdos;
                                foreach (var xerchik in xer.Split('\n'))
                                {
                                    if (xerchik.StartsWith("Count="))
                                    {
                                        count = int.Parse(xerchik.Split('=')[1]);
                                    }
                                    else if (!string.IsNullOrWhiteSpace(xerchik))
                                    {
                                        if (!string.IsNullOrWhiteSpace(xerchik.Split('=')[1]))
                                        {
                                            var x = xerchik.Split('=')[1];
                                            string new_x = x.Replace("\r", "").Replace("\n", "").Trim();
                                            if (!string.IsNullOrEmpty(new_x))
                                            {
                                                Local_Achivments.Add(new_x);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                return (count, Local_Achivments);
            }
            catch (Exception ex)
            {
                Settings.Exp(ex);
                return (0, null);
            }
        }

        public static void FirstStart(string appid)
        {
            string path_combin = Path.Combine(Settings.path, $"{appid}_info.txt");
            var CounterDristos = GetCountRune(File.ReadAllText(path_combin).Split('|')[0]);
            string local_path = Path.Combine(Settings.path, $"{appid}_achievements.txt");
            File.WriteAllText(local_path, string.Join("\r\n", CounterDristos.Item2));
        }

        public static void InfinityParserRune(string appid)
        {
            while (Settings.ThreadIsStart)
            {
                Task.Delay(1000).Wait();
                string path_combin = Path.Combine(Settings.path, $"{appid}_info.txt");
                string local_path = Path.Combine(Settings.path, $"{appid}_achievements.txt");
                List<string> fck_line = new List<string>(File.ReadAllLines(local_path));
                var CounterDristos = GetCountRune(File.ReadAllText(path_combin).Split('|')[0]);
                foreach (var one_achivment in CounterDristos.Item2)
                {
                    if (!fck_line.Contains(one_achivment))
                    {
                        string eblatoriy = one_achivment;
                        if (eblatoriy.Contains("\r"))
                        {
                            eblatoriy = eblatoriy.Replace("\r", "");
                        }
#if DEBUG
                        Console.WriteLine($"[debug] Получено достижение: {one_achivment}");
#endif
                        fck_line.Add(one_achivment);
                        File.WriteAllLines(local_path, fck_line);
                        Achievements.ShowAchivment(appid, eblatoriy);
                    }
                }
            }
        }

        public static bool Parse()
        {
            try
            {
                string rune = Path.Combine(Environment.GetEnvironmentVariable("PUBLIC"), "Documents\\Steam\\RUNE");
                if (Directory.Exists(rune))
                {
                    bool sto_proc = false;
                    foreach (var folder in Directory.GetDirectories(rune))
                    {
                        string appid = folder.Split('\\')[6];
                        var path = Path.Combine(Settings.path, $"{appid}_info.txt");
                        foreach (var file in Directory.GetFiles(folder))
                        {
                            if (file.Contains("achievements.ini"))
                            {
                                string game = Achievements.GetAppName(appid);
                                Achievements.CreateCheme(appid);
                                File.WriteAllText(path, $"{file}|{game}|RUNE|{appid}|{Settings.language}");
                                sto_proc = true;
                            }
                        }
                    }
                    return sto_proc;
                }
                else
                {
                    return false;
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
