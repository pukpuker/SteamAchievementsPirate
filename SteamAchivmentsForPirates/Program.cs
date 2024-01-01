using SteamAchivmentsForPirates;
using System.Drawing;

namespace SteamAchivmentsForPirates
{
    public static class SX
    {
        public static void Main()
        {
            Settings.SettingsParser();
            Achivments.ShowAchivment("641990", "OpenPrison");
            Achivments.FirstStart("641990");
            int vera = Achivments.GetCount().Item1;
            var thread_InfinityParser = new Thread(() => Achivments.InfinityParser("641990"));
            thread_InfinityParser.Start();
        }
    }
}
