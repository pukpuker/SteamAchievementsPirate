using SteamAchivmentsForPirates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SteamAchievementsPirate
{
    public static class Actions
    {
        public static void FreeTP_Path()
        {
            Console.Write("\nEnter FreeTP Folder: ");
            string? path = Console.ReadLine();
            Settings.UpdateValue("freetp_path", path);
            SX.Main();
        }
        public static void Parse()
        {
            Achivments.ParsingGames();
        }

        public static void MyAchivment()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Thread t = new Thread(() =>
            {
                Application.Run(new achievement_vitrina());
            });
            t.Start();
        }

        public static void Hours()
        {

        }
    }
}
