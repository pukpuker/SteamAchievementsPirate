using INIParser;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SteamAchivmentsForPirates
{
    public static class Achivments
    {
        private static Dictionary<string, string> app_ids = new Dictionary<string, string>();
        static List<string> achivments = new System.Collections.Generic.List<string>();
        static List<string> achivments_old = new System.Collections.Generic.List<string>();

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

        public static void ShowAchivment(string appid, string achivka)
        {
            var path = Path.Combine(Settings.path, $"{appid}.txt");

            WebClient ugar = new WebClient();
            string json = "";
            if (File.Exists(path))
            {
                json = File.ReadAllText(path);
            }
            else
            {
                json = ugar.DownloadString($"https://api.steampowered.com/ISteamUserStats/GetSchemaForGame/v2?appid={appid}&key={Settings.api_key}&l={Settings.language}");
                File.WriteAllText(path, json);
            }
            JObject obj = JObject.Parse(json);
            JArray achievements = (JArray)obj["game"]["availableGameStats"]["achievements"];
            JObject statistician = (JObject)achievements.FirstOrDefault(x => (string)x["name"] == achivka);

            if (statistician != null)
            {
                string displayName = (string)statistician["displayName"];
                string description = (string)statistician["description"];
                string icon = (string)statistician["icon"];
                Console.WriteLine(displayName);
                StartAchivment(displayName, description, icon);
            }
        }

        public static (int, List<string>) GetCount()
        {
            try
            {
                int count = 0;
                List<string> Local_Achivments = new System.Collections.Generic.List<string>();

                string xer = null;
                string file = File.ReadAllText("C:\\Users\\Public\\Documents\\Steam\\CODEX\\641990\\achievements.ini");
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
                achivments = Local_Achivments;
                return (count, Local_Achivments);
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.ToString());
                return (0, null);
            }            
        }

        public static void FirstStart(string appid)
        {
            var CounterDristos = GetCount();
            int value = CounterDristos.Item1;
            achivments_old = CounterDristos.Item2;
            SetValue_Kolvo(appid, value);
        }

        public static void InfinityParser(string appid)
        {
            while (true) 
            {
                Task.Delay(1000).Wait();
                var CounterDristos = GetCount();
                int value = CounterDristos.Item1;
                if (value > GetValue_Kolvo("641990"))
                {
                    foreach (var one_achivment in CounterDristos.Item2)
                    {
                        if (!achivments_old.Contains(one_achivment))
                        {
                            Console.WriteLine($"Получено достижение: {one_achivment}");
                            achivments_old.Add(one_achivment);
                            ShowAchivment(appid, one_achivment);
                        }
                    }
                }
            }
        }
    }
}
