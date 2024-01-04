using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace SteamAchivmentsForPirates
{
    public static class Settings
    {
        static readonly string env_file = ".env";
        public static readonly string[] Path_Def_Games = { "C:\\Games", "D:\\Games" };
        // settings from .env
        public static string api_key = "";
        public static string language = "";
        public static string path = "games\\";
        public static string freetp_path = "";
        // settins from .env

        public static void SetUp()
        {
            Console.Write("This is your first time running the application, do you want to customize the configuration file? You can do this later by simply editing the \".env\" file through any text editor. Type 'y' or 'n': ");
            if (Console.ReadLine().Contains("y"))
            {
                Console.Write("\nThe application cannot work without the Steam Web Api Key. Do you want to register an API key? Type 'y' - for yes or 'n' for no: ");
                if (Console.ReadLine().Contains("y"))
                {
                    Process.Start("explorer.exe", "https://steamcommunity.com/dev/apikey");
                    Console.Clear();
                    Console.Write("Enter your Steam Web Api key: ");
                    string? api_key = Console.ReadLine();
                    UpdateValue("api_key", api_key);
                    Console.Write("\nEnter your language For achiev and etc. (e.g russian | english): ");
                    string? language = Console.ReadLine();
                    UpdateValue("language", language);
                    Console.Write("\nAre you using repacks from the FreeTP site? Enter 'y' or 'n': ");
                    if (Console.ReadLine().Contains("y"))
                    {
                        Console.Write("\nWrite Folder Path where u install FreeTP Repacks. (e.g. C:\\Games): ");
                        string path = Console.ReadLine();
                        UpdateValue("freetp_path", path);
                    }
                }
                else
                {
                    Console.WriteLine("The application cannot work without the Steam Web Api Key. Closing...");
                    Environment.Exit(0);
                }
            }
            else
            {
                Console.WriteLine("Closing...");
                Environment.Exit(0);
            }
            Console.Clear();
            SettingsParser();
        }

        public static void CreateDirectory()
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public static void SettingsParser()
        {
            try
            {
                CreateDirectory();
                if (File.Exists(env_file))
                {
                    var lines = File.ReadAllLines(env_file);
                    if (!(lines.Length < 1))
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
                                        api_key = value;
                                    }
                                    else if (parametr == "language")
                                    {
                                        language = value;
                                    }
                                    else if (parametr == "freetp_path")
                                    {
                                        freetp_path = value;
                                    }
                                }
                                else
                                {
                                    if (parametr == "api_key")
                                    {
                                        Console.Write("The application cannot work without the Steam Web Api Key. Do you want to register an API key? Type 'y' - for yes or 'n' for no: ");
                                        if (Console.ReadLine().Contains("y"))
                                        {
                                            Process.Start("explorer.exe", "https://steamcommunity.com/dev/apikey");
                                            Console.Clear();
                                            Console.Write("Enter your Steam Web Api key: ");
                                            string api_key = Console.ReadLine();
                                            UpdateValue("api_key", api_key);
                                            SettingsParser();
                                        }
                                        else
                                        {
                                            Environment.Exit(0);
                                        }
                                    }
                                    else if (parametr == "language")
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
            Console.WriteLine(ex.ToString());
            Console.ReadKey();
#elif READYTORELEASE
            Console.WriteLine(ex.Message);
            Task.Delay(10000).Wait();
            Environment.Exit(1488);
#endif
        }

        public static void ChangeTitle()
        {
#if READYTORELEASE
            Console.Title = $"SteamAchievements";
#elif DEBUG
            Console.Title = $"SteamAchievements Debug";
#endif
        }
    }
}
