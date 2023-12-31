using SteamAchivmentsForPirates;

namespace SteamAchivmentsForPirates
{
    public static class SX
    {
        public static void Main()
        {
            Achivments.ShowAchivment();
            Console.WriteLine("Привет\n");
            Settings.SettingsParser();
            Achivments.FirstStart("");
            int vera = Achivments.GetCount().Item1;
            var thread_InfinityParser = new Thread(() => Achivments.InfinityParser(""));
            thread_InfinityParser.Start();
        }
    }
}
