using Newtonsoft.Json.Linq;
using SteamAchievementsPirate.Emulators;
using System.Net;

namespace SteamAchivmentsForPirates
{
    public static class Achievements
    {
        private static Dictionary<string, string> app_ids = new Dictionary<string, string>();
        public static WebClient ugar = new WebClient();

        public static void StartAchivment(string name, string description, string url)
        {
            if (Settings.notif_style == "steamold")
            {
                Thread t = new Thread(() =>
                {
                    Application.Run(new OverlayForm(name, description, url));
                });
                t.Start();
            }   
            else 
            {
                Thread t = new Thread(() =>
                {
                    Application.Run(new OverlaySteamNewForm(name, description, url));
                });
                t.Start();
            }
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
                    return true;
                }
            }
            catch (NullReferenceException)
            {
                // achievements = 0 
                return false;
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
                string json_first_attempt = ugar.DownloadString($"https://store.steampowered.com/api/appdetails?appids={appid}");
                JObject obj_first_attempt = JObject.Parse(json_first_attempt);
                bool xuina = (bool)obj_first_attempt[appid]["success"];
                if (xuina)
                {
                    string game_name = (string)obj_first_attempt[appid]["data"]["name"];
                    return game_name;
                }
                else
                {
                    string json = ugar.DownloadString($"https://api.steampowered.com/ISteamUserStats/GetSchemaForGame/v2?appid={appid}&key={Settings.api_key}&l={Settings.language}");
                    JObject obj = JObject.Parse(json);
                    string game_name = (string)obj["game"]["gameName"];
                    return game_name;
                }
            }
            catch (WebException)
            {
                MessageBox.Show("403 From API Steam. RateLimited. Wait 10 seconds and Press OK to try again.", "SAP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                string ugar = GetAppName(appid);
                return ugar;
            }
            catch (Exception ex)
            {
                Settings.Exp(ex);
                return null;
            }
        }

        public static void CreateCheme(string appid)
        {
            try
            {
                var path = Path.Combine(Settings.path, $"{appid}.txt");
                if (!File.Exists(path))
                {
                    string json = ugar.DownloadString($"https://api.steampowered.com/ISteamUserStats/GetSchemaForGame/v2?appid={appid}&key={Settings.api_key}&l={Settings.language}");
                    File.WriteAllText(path, json);
                }
                else
                {
                    string language_from_file = File.ReadAllText(Path.Combine(Settings.path, $"{appid}_info.txt")).Split('|')[4];
                    if (language_from_file != Settings.language)
                    {
                        string json = ugar.DownloadString($"https://api.steampowered.com/ISteamUserStats/GetSchemaForGame/v2?appid={appid}&key={Settings.api_key}&l={Settings.language}");
                        File.WriteAllText(path, json);
                    }
                }
                string percenet_file = $"{Settings.path}\\{appid}_percents.txt";
                if (!File.Exists(percenet_file))
                {
                    string json = ugar.DownloadString($"https://api.steampowered.com/ISteamUserStats/GetGlobalAchievementPercentagesForApp/v0002/?gameid={appid}&format=json");
                    File.WriteAllText(percenet_file, json);
                }
            }
            catch (WebException)
            {
                Task.Delay(500).Wait();
                CreateCheme(appid);
            }
            catch (Exception ex)
            {
                Settings.Exp(ex);
            }
        }

        public static void ShowTestAchivment()
        {
            string icon = "https://steamcdn-a.akamaihd.net/steamcommunity/public/images/apps/480/winner.jpg";
            StartAchivment("Winner", "Win one game.", icon);
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

        public static bool ParsingGames()
        {
            bool codex = Codex.Parse();
            bool rune = Rune.Parse();
            bool freetp = FreeTP.Parse();
            if ((codex || freetp || rune) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
