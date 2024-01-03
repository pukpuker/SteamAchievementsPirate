using Newtonsoft.Json.Linq;
using SteamAchievementsPirate;
using System.DirectoryServices.ActiveDirectory;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;

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

        public static void ParsingGames()
        {
            Codex.Parse();
            FreeTP.Parse();
        }
    }
}
