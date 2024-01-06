using Newtonsoft.Json.Linq;
using SteamAchievementsPirate;
using System.Net;

namespace SteamAchivmentsForPirates
{
    public static class Achievements
    {
        private static Dictionary<string, string> app_ids = new Dictionary<string, string>();
        public static WebClient ugar = new WebClient();

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

        public static void GetPercentAchievements()
        {
            // https://api.steampowered.com/ISteamUserStats/GetGlobalAchievementPercentagesForApp/v0002/?gameid=641990&format=json
        }

        public static bool DownloadAchievements(string appid)
        {
            try
            {
                string path_to_photos = Path.Combine(Settings.path, "AchiviementsPhotos");
                var massive_Files = Directory.GetFiles(path_to_photos);
                if (!Array.Exists(massive_Files, element => element.StartsWith(appid)))
                {
                    var path = Path.Combine(Settings.path, $"{appid}.txt");
                    string json = "";
                    if (File.Exists(path))
                    {
                        json = File.ReadAllText(path);
                    }
                    else
                    {
                        Achievements.CreateCheme(appid);
                    }
                    JObject obj = JObject.Parse(json);
                    JArray achievements = (JArray)obj["game"]["availableGameStats"]["achievements"];
                    foreach (JObject achievement in achievements)
                    {
                        string name = (string)achievement["name"];
                        string icon = (string)achievement["icon"];
                        string icongray = (string)achievement["icongray"];
                        Achievements.ugar.DownloadFile(icon, $"{path_to_photos}\\{appid}_{name}_default.jpg");
                        Achievements.ugar.DownloadFile(icongray, $"{path_to_photos}\\{appid}_{name}_gray.jpg");
                    }
                    return true;
                }
                else
                {
                    // добавить функцию в самой форме, чтобы можно было просто нажать кнопку "проверить файлы" и потом брать и чекать по getfiles есть ли такой файл или нет, то есть брать массив из json файла и если не найдено, то докачивать. 
                    // и добавить также кнопку "Перекачки" на случай если битые файлы или еще че нить.
                    return true;
                }
            }
            catch (Exception ex)
            {
                Settings.Exp(ex);
                return false;
            }
        }

        public static string GetAppName(string appid)
        {
            try
            {
                string json = ugar.DownloadString($"https://api.steampowered.com/ISteamUserStats/GetSchemaForGame/v2?appid={appid}&key={Settings.api_key}&l={Settings.language}");
                JObject obj = JObject.Parse(json);
                string game_name = (string)obj["game"]["gameName"];
                return game_name;
            }
            catch (WebException)
            {
                Console.WriteLine("403 From API Steam. Most likely incorrect SteamAPI Key (If u wanna change API_KEY, edit .env throw text editors). Exit.");
                Console.ReadKey();
                Environment.Exit(1);
                return null;
            }
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

        public static void ParsingGames()
        {
            Codex.Parse();
            FreeTP.Parse();
        }
    }
}
