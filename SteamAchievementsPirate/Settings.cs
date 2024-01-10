using SteamAchievementsPirate;
using System.Diagnostics;

namespace SteamAchivmentsForPirates
{
    public static class Settings
    {
        public static readonly string version = "0.2.0";
        static readonly string env_file = ".env";
        public static readonly string[] Path_Def_Games = { "C:\\Games", "D:\\Games" };

        public static bool HaveGames = false;
        public static bool ThreadIsStart = false;
        // settings from .env
        public static string api_key = "";
        public static string language = "";
        public static string path = "games\\";
        public static string freetp_path = "";
        public static bool StartUP = false;
        public static bool StartThreads = false;
        // settins from .env

        //public static void SetUp()
        //{
        //    string? api_key = null;
        //    Console.Write("This is your first time running the application, do you want to customize the configuration file? You can do this later by simply editing the \".env\" file through any text editor. Type 'y' or 'n' (Def Y): ");
        //    string console_stage1 = Console.ReadLine();
        //    if (console_stage1.Contains("y") || console_stage1 == "")
        //    {
        //        Console.Write("\nThe application cannot work without the Steam Web Api Key. Do you want to register an API key?\nYou can also try using a free key, but there is a chance that it will not work. For it, Type 'f'\n\nType 'y', Type 'n' or Type 'f' (Def Y): ");
        //        string? console_stage2 = Console.ReadLine();
        //        if (console_stage2.Contains("y") || console_stage2 == "")
        //        {
        //            Process.Start("explorer.exe", "https://steamcommunity.com/dev/apikey");
        //            Console.Clear();
        //            Console.Write("Enter your Steam Web Api key: ");
        //            api_key = Console.ReadLine();
        //        }
        //        else if (console_stage2.Contains("f"))
        //        {
        //            api_key = "FREE";
        //        }
        //        else
        //        {
        //            Console.WriteLine("The application cannot work without the Steam Web Api Key. Closing...");
        //            Environment.Exit(0);
        //        }
        //        UpdateValue("api_key", api_key);
        //        Console.Write("\nEnter your language For achiev and etc. (e.g russian | english. Def english): ");
        //        string? language = Console.ReadLine();
        //        if (language == "")
        //        {
        //            UpdateValue("language", "english");
        //        }
        //        else
        //        {
        //            UpdateValue("language", language);
        //        }
        //        Console.Write("\nAre you using repacks from the FreeTP site? Enter 'y' or 'n' (Def N): ");
        //        string console_stage3 = Console.ReadLine();
        //        if (console_stage3.Contains("y"))
        //        {
        //            Console.Write("\nWrite Folder Path where u install FreeTP Repacks. (e.g. C:\\Games): ");
        //            string path = Console.ReadLine();
        //            UpdateValue("freetp_path", path);
        //        }
        //    }
        //    else
        //    {
        //        Console.Clear();
        //        Console.WriteLine("Edit the .env file and run the application, or run it again and select 'y' for automatic editing. Press ENTER to close.");
        //        Console.ReadKey();
        //        Environment.Exit(0);
        //    }
        //    Console.Clear();
        //    SettingsParser();
        //}

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

        public static void ChangeTitle()
        {
#if READYTORELEASE
            Console.Title = $"SteamAchievements. Version: {version}";
#elif DEBUG
            Console.Title = $"SteamAchievements Debug {Settings.version}";
#endif
        }
    }
}
