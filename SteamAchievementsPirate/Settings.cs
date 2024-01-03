using System;
using System.Collections.Generic;
using System.Configuration;
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
        public static void SettingsParser()
        {
            try
            {
                var lines = File.ReadAllLines(env_file);
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
                                if (string.IsNullOrWhiteSpace(value))
                                {
                                    language = "english";
                                }
                                else
                                {
                                    language = value;
                                }
                            }
                            else if (parametr == "freetp_path")
                            {
                                freetp_path = value;
                            }
                        }
                    }
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
            catch (Exception ex)
            {
                Settings.Exp(ex);
            }
        }

        public static void Exp(Exception ex)
        {
#if DEBUG
            Console.WriteLine(ex.ToString());
#endif
//#elif RELEASE
//            Console.WriteLine(ex.Message);
//#endif
        }

        public static void ChangeTitle()
        {
            Console.Title = $"SteamAchievements Debug";
        }
    }
}
