using Newtonsoft.Json.Linq;
using SteamAchievementsPirate;
using System.DirectoryServices.ActiveDirectory;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SteamAchivmentsForPirates
{
    public static class Achivments
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool IsIconic(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern bool IsZoomed(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern IntPtr FindWindow(string lp1, string lp2);


        private static Dictionary<string, string> app_ids = new Dictionary<string, string>();
        static List<string> achivments_old = new System.Collections.Generic.List<string>();
        static WebClient ugar = new WebClient();

        public static void StartAchivment(string name, string description, string url)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Thread t = new Thread(() =>
            {
                Application.Run(new OverlayForm(name, description, url));
            });
            t.Start();
        }

        public static bool GameIsZoomed()
        {
            IntPtr instance = FindWindow(null, "The Escapists 2");
            return IsIconic(instance);
        }

        public static void SetValue(string key, string value)
        {
            app_ids[key] = value;
        }

        public static string GetValue(string key)
        {
            return app_ids.ContainsKey(key) ? app_ids[key] : null;
        }

        private static Dictionary<string, int> Kolvo = new Dictionary<string, int>();

        public static void SetValue_Kolvo(string key, int value)
        {
            Kolvo[key] = value;
        }

        public static int GetValue_Kolvo(string key)
        {
            return Kolvo.ContainsKey(key) ? Kolvo[key] : 0;
        }

        public static string GetAppName(string appid)
        {
            string json = ugar.DownloadString($"https://api.steampowered.com/ISteamUserStats/GetSchemaForGame/v2?appid={appid}&key={Settings.api_key}&l={Settings.language}");
            JObject obj = JObject.Parse(json);
            string game_name = (string)obj["game"]["gameName"];
            return game_name;
        }

        public static void CreateCheme(string appid)
        {
            var path = Path.Combine(Settings.path, $"{appid}.txt");
            if (!File.Exists(path))
            {
                string json = ugar.DownloadString($"https://api.steampowered.com/ISteamUserStats/GetSchemaForGame/v2?appid={appid}&key={Settings.api_key}&l={Settings.language}");
                File.WriteAllText(path, json);
            }
        }

        public static void ShowAchivment(string appid, string achivka)
        {
            var path = Path.Combine(Settings.path, $"{appid}.txt");
            string json = "";
            if (File.Exists(path))
            {
                json = File.ReadAllText(path);
            }
            else
            {
                CreateCheme(appid);
            }
            JObject obj = JObject.Parse(json);
            JArray achievements = (JArray)obj["game"]["availableGameStats"]["achievements"];
            JObject statistician = (JObject)achievements.FirstOrDefault(x => (string)x["name"] == achivka);
            if (statistician != null)
            {
                string displayName = (string)statistician["displayName"];
                string description = (string)statistician["description"];
                string icon = (string)statistician["icon"];
                StartAchivment(displayName, description, icon);
            }
        }

        public static void Codex()
        {
            string codex_NEW = Path.Combine(System.Environment.GetEnvironmentVariable("PUBLIC"), "Documents\\Steam\\CODEX");
            foreach (var folder in Directory.GetDirectories(codex_NEW))
            {
                string appid = folder.Split('\\')[6];
                var path = Path.Combine(Settings.path, $"{appid}_info.txt");
                foreach (var file in Directory.GetFiles(folder))
                {
                    if (file.Contains("achievements.ini"))
                    {
                        string game = GetAppName(appid);
                        File.WriteAllText(path, $"{file}|{game}|CODEX|{appid}");
                        CreateCheme(appid);
                    }
                }
            }
        }

        public static void FreeTP() // добавить возможность массив путей, т.е. несколько путей в параметрах.
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
                            string game = GetAppName(appid);
                            string path_to_achivments = Path.Combine(directory_games, "Achievements");
                            File.WriteAllText(path, $"{path_to_achivments}|{game}|FreeTP|{appid}");
                            CreateCheme(appid);
                        }
                    }
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("FreeTP Path is NULL. The search will only be performed on the paths: \"C:\\Games\" and \"D:\\Games\". Do you want to specify the search path for FreeTP games?");
                Console.Write("\nInput 'yes' or 'no': ");
                string ugar = Console.ReadLine();
                if (ugar.Contains("yes"))
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
                                    string game = GetAppName(appid);
                                    string path_to_achivments = Path.Combine(directory_games, "Achievements");
                                    File.WriteAllText(path, $"{path_to_achivments}|{game}|FreeTP|{appid}");
                                    CreateCheme(appid);
                                }
                            }
                        }
                    }
                }
            }
        }


        public static void ParsingGames()
        {
            Codex();
            FreeTP();
        }

        public static (int, List<string>) GetCountCodex(string path)
        {
            try
            {
                int count = 0;
                List<string> Local_Achivments = new System.Collections.Generic.List<string>();

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
                                        Local_Achivments.Add(xerchik.Split('=')[1]);
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
            var CounterDristos = GetCountCodex(File.ReadAllText(path_combin).Split('|')[0]);
            int value = CounterDristos.Item1;
            achivments_old = CounterDristos.Item2;
            SetValue_Kolvo(appid, value);
        }

        public static void InfinityParserCodex(string appid)
        {
            while (true) 
            {
                Task.Delay(1000).Wait();
                string path_combin = Path.Combine(Settings.path, $"{appid}_info.txt");
                var CounterDristos = GetCountCodex(File.ReadAllText(path_combin).Split('|')[0]);
                int value = CounterDristos.Item1;
                if (value > GetValue_Kolvo(appid))
                {
                    foreach (var one_achivment in CounterDristos.Item2)
                    {
                        if (!achivments_old.Contains(one_achivment))
                        {
                            string eblatoriy = one_achivment;
                            if (eblatoriy.Contains("\r"))
                            {
                                eblatoriy = eblatoriy.Replace("\r", "");
                            }
                            Console.WriteLine($"[debug] Получено достижение: {one_achivment}");
                            achivments_old.Add(one_achivment);
                            ShowAchivment(appid, eblatoriy);
                        }
                    }
                }
            }
        }
    }
}
