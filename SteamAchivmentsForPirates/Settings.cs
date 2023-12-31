using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamAchivmentsForPirates
{
    public static class Settings
    {
        public static string api_key = "";
        public static string language = "";
        public static string path = "games\\";

        public static void SettingsParser()
        {
            try
            {
                var lines = File.ReadAllLines(".env");
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
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
