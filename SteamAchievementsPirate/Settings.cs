using SteamAchievementsPirate;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SteamAchivmentsForPirates
{
    public static class Settings
    {
        public static readonly string version = "0.2.0";
        static readonly string env_file = ".env";
        public static readonly string[] Path_Def_Games = { "C:\\Games", "D:\\Games" };

        public static bool HaveGames = false;
        public static bool ThreadIsStart = false;
        public static bool debug = false;
        // settings from .env
        public static string api_key = "";
        public static string language = "";
        public static string path = "games\\";
        public static string freetp_path = "";
        public static bool StartUP = false;
        public static bool StartThreads = false;
        // settins from .env

        public static void CreateDirectory()
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public static void SetUp()
        {
            MessageBox.Show("You have launched the application for the first time. To configure the .env file click 'OK'.\n!!! After saving the settings, restart the application. !!!", "SAP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SettingsForm());
        }

        public static void SettingsParser()
        {
            try
            {
                DebugOrNo();
                CreateDirectory();
                if (File.Exists(env_file))
                {
                    var lines = File.ReadAllLines(env_file);
                    if (lines.Length > 2)
                    {
                        foreach (var line in lines)
                        {
                            if (line.Contains("="))
                            {
                                var fix = line.Split('=');
                                string parametr = fix[0];
                                string value = fix[1];
                                if (!string.IsNullOrWhiteSpace(value))
                                {
                                    if (parametr == "api_key")
                                    {
                                        if (value == "FREE")
                                        {
                                            api_key = "1B01FE5A4ED8E822F7763B63F7A8D5FE";
                                        }
                                        else
                                        {
                                            api_key = value;
                                        }
                                    }
                                    else if (parametr == "language")
                                    {
                                        language = value;
                                    }
                                    else if (parametr == "freetp_path")
                                    {
                                        freetp_path = value;
                                    }
                                    else if (parametr == "startup")
                                    {
                                        StartUP = bool.Parse(value);
                                    }
                                    else if (parametr == "start_threads")
                                    {
                                        StartThreads = bool.Parse(value);
                                    }
                                }
                                else
                                {
                                    if (parametr == "language")
                                    {
                                        language = "english";
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        SetUp();
                    }
                }
                else
                {
                    SetUp();
                }
            }
            catch (Exception ex)
            {
                Settings.Exp(ex);
            }
        }

        public static void UpdateValue(string parametr, string value)
        {
            try
            {
                List<string> new_lines = new List<string>();
                var lines = File.ReadLines(env_file);
                bool naideno = false;
                foreach (var line in lines)
                {
                    if (line.Contains(parametr))
                    {
                        var temp_line = $"{parametr}={value}";
                        new_lines.Add(temp_line);
                        naideno = true;
                    }
                    else
                    {
                        new_lines.Add(line);
                    }
                }
                if (!naideno)
                {
                    new_lines.Add($"{parametr}={value}");
                }
                File.WriteAllLines(env_file, new_lines);
            }
            catch (FileNotFoundException)
            {
                var status = File.Create(env_file);
                status.Close();
                UpdateValue(parametr, value);
            }
            catch (Exception ex)
            {
                Settings.Exp(ex);
            }
        }

        public static void Exp(Exception ex)
        {
#if DEBUG
            MessageBox.Show(ex.ToString());
            Console.ReadKey();
#elif READYTORELEASE
            MessageBox.Show(ex.Message);
            Task.Delay(10000).Wait();
            Environment.Exit(1488);
#endif
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AllocConsole();
        public static void ChangeTitle()
        {
            Console.Title = $"SteamAchievements Debug {Settings.version}";
        }

        [Conditional("DEBUG")]
        public static void DebugOrNo()
        {
            debug = true;
        }
    }
}
