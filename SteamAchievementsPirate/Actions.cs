using SteamAchivmentsForPirates;

namespace SteamAchievementsPirate
{
    public static class Actions
    {

        public static bool DoUWantUseIt()
        {
            if (!Directory.Exists(Path.Combine(Settings.path, "AchiviementsPhotos")))
            {
                var result = MessageBox.Show("To use this function, you need to download all the achievement images. This will happen automatically, but will take some time. This can take quite a long time if you have a slow Internet connection or a lot of games. Do you want to start downloading?", "Achiviements", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    string path_to_photos = Path.Combine(Settings.path, "AchiviementsPhotos");
                    if (!Directory.Exists(path_to_photos))
                    {
                        Directory.CreateDirectory(path_to_photos);
                    }
                    foreach (var one_app_id in SX.games_APPIDS)
                    {
                        bool uspeh = Achievements.DownloadAchievements(one_app_id);
                        if (!uspeh)
                        {
                            var oooops = MessageBox.Show("Something went wrong... Do you want to try again?", "Achiviements", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                            if (oooops == DialogResult.Yes)
                            {
                                if (!Achievements.DownloadAchievements(one_app_id))
                                {
                                    MessageBox.Show("The attempt was unsuccessful. Try again later or turn the VPN connection on/off. Also check your internet connection. Click OK to exit.", "Achiviements", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                                    return false;
                                }
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        //actions
        public static void FreeTP_Path()
        {
            Console.Write("\nEnter FreeTP Folder: ");
            string? path = Console.ReadLine();
            Settings.UpdateValue("freetp_path", path);
            SX.Main();
        }
        public static void Parse()
        {
            Achievements.ParsingGames();
        }

        public static void MyAchivment()
        {
            if (DoUWantUseIt())
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Thread t = new Thread(() =>
                {
                    Application.Run(new achievement_vitrina());
                });
                t.Start();
            }
        }
        //actions
    }
}
