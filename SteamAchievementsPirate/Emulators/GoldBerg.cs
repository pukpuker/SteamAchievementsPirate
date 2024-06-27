using Newtonsoft.Json.Linq;
using SteamAchivmentsForPirates;
using System.IO;
using System.Text.RegularExpressions;

namespace SteamAchievementsPirate.Emulators
{
    public static class GoldBerg
    {
        public static (int, List<string>) GetCount(string path)
        {
            try
            {
                int count = 0;
                List<string> Local_Achivments = new List<string>();

                string xer = null;

                JObject achievements = JObject.Parse(File.ReadAllText(path));

                int earnedCount = 0;
                foreach (var achievement in achievements)
                {
                    if ((bool)achievement.Value["earned"])
                    {
                        earnedCount++;
                        Local_Achivments.Add(achievement.Key);
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
            var CounterDristos = GetCount(File.ReadAllText(path_combin).Split('|')[0]);
            string local_path = Path.Combine(Settings.path, $"{appid}_achievements.txt");
            File.WriteAllText(local_path, string.Join("\r\n", CounterDristos.Item2));
        }

        public static void InfinityParser(string appid)
        {
            while (Settings.ThreadIsStart)
            {
                Console.WriteLine("[GoldBerg] +");
                Task.Delay(1000).Wait();
                string path_combin = Path.Combine(Settings.path, $"{appid}_info.txt");
                string local_path = Path.Combine(Settings.path, $"{appid}_achievements.txt");
                List<string> fck_line = new List<string>(File.ReadAllLines(local_path));
                var CounterDristos = GetCount(File.ReadAllText(path_combin).Split('|')[0]);
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
                        Console.WriteLine($"[GoldBerg] Получено достижение: {one_achivment}");
#endif
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
                string[] folders = Directory.GetFiles(directory_from, "achievements.json", SearchOption.AllDirectories);

                foreach (var folder in folders)
                {
                    if (folder.Contains("local_saves")) // сделать это в InfinityParser, а сюда поставить поиск по другим файлам голдберга.
                    {
                        string pattern = @"\\local_saves\\(\d+)\\achievements\.json"; // smotryashiy NA g0vn0

                        Regex regex = new Regex(pattern);
                        Match match = regex.Match(folder);

                        if (match.Success)
                        {
                            string appid = match.Groups[1].Value;
                            var path = Path.Combine(Settings.path, $"{appid}_info.txt");
                            string game = Achievements.GetAppName(appid);
                            string path_to_achivment = Path.Combine(folder);
                            Achievements.CreateCheme(appid);
                            File.WriteAllText(path, $"{path_to_achivment}|{game}|GoldBerg|{appid}|{Settings.language}");
                            stage = true;
                        }
                        else
                        {
                            Console.WriteLine("[GoldBerg] Number not found");
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
