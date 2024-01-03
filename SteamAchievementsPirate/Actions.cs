using SteamAchivmentsForPirates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
