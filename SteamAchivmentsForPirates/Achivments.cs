using INIParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SteamAchivmentsForPirates
{
    public static class Achivments
    {
        private static Dictionary<string, string> app_ids = new Dictionary<string, string>();
        static List<string> achivments = new System.Collections.Generic.List<string>();
        static List<string> achivments_old = new System.Collections.Generic.List<string>();

        public static void ShowAchivment()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Thread t = new Thread(() =>
            {
                Application.Run(new OverlayForm());
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
            SetValue_Kolvo("641990", value);
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
                        }
                    }
                }
            }
        }
    }
}
