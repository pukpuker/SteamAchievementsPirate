using Microsoft.Win32;
using SteamAchievementsPirate;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SteamAchivmentsForPirates
{
    public static class Settings
    {
        public static readonly string version = "0.3.0";
        static readonly string env_file = ".env";
        public static string path = "games\\";
        public static readonly string[] Path_Def_Games = { "C:\\Games", "D:\\Games" };

        public static bool HaveGames = false;
        public static bool ThreadIsStart = false;
        public static bool debug = false;
        public static bool FromAutoRun = false;
        // settings from .env
        public static string api_key = "";
        public static string language = "";
        public static List<string> games_path = new List<string>();
        public static bool StartUP = false;
        public static bool StartThreads = false; // from start app
        public static bool StartParserFromStart = false;
        public static string notif_style = "";
        public static string overlay_location = "";
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
            MessageBox.Show("You have launched the application for the first time. To configure the .env file click 'OK'.\n!!! After you clicked the save button, close the settings form and the program will open. !!!", "SAP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Application.Run(new SettingsForm());
            SX.Main();
        }

        public static void SettingsParser()
        {
            games_path = new List<string>();
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
                                    else if (parametr == "games_path")
                                    {
                                        var splitted = value.Split(";");
                                        foreach (var split in splitted)
                                        {
                                            games_path.Add(split);
                                        }
                                    }
                                    else if (parametr == "startup")
                                    {
                                        bool.TryParse(value, out StartUP);
                                    }
                                    else if (parametr == "start_threads")
                                    {
                                        bool.TryParse(value, out StartThreads);
                                    }
                                    else if (parametr == "notif_style")
                                    {
                                        notif_style = value;
                                    }
                                    else if (parametr == "overlay_location")
                                    {
                                        overlay_location = value;
                                    }
                                    else if (parametr == "autoparser")
                                    {
                                        bool.TryParse(value, out StartParserFromStart);
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
        public static bool SetAutorunValue(bool autorun)
        {
            string arg = "autostart";
            string executablePath = Application.ExecutablePath + " " + arg;
            var nameApp = "SAP";
            RegistryKey reg;
            reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (autorun)
            {
                if (reg.GetValue(nameApp) == null)
                {
                    reg.SetValue(nameApp, executablePath);
                    reg.Close();
                    return true;
                }
                else
                    return false;
            }
            else
            {
                if (reg.GetValue(nameApp) != null)
                {
                    reg.DeleteValue(nameApp);
                    reg.Close();
                    return true;
                }
                else
                    return false;
            }
        }

        public static void Exp(Exception ex)
        {
#if DEBUG
            MessageBox.Show(ex.ToString());
#elif READYTORELEASE
            MessageBox.Show(ex.Message);
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
