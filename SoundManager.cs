using System;
using System.Windows.Media;

namespace kjtStore
{
    static class SoundManager
    {
        private static MediaPlayer mpPlayer;
        private static string clickSoundPath;
        private static string clickContextMenuSound;

        static SoundManager()
        {
            Preparing();
        }

        public static void PlayClick()
        {
            mpPlayer.Open(new Uri(clickSoundPath, UriKind.Relative));
            mpPlayer.Play();
        }

        public static void PlayClickContextMenu()
        {
            mpPlayer.Open(new Uri(clickContextMenuSound, UriKind.Relative));
            mpPlayer.Play();
        }

        private static void Preparing()
        {
            if(mpPlayer == null)
            {
                mpPlayer = new MediaPlayer();
            }

            clickSoundPath = @"pd\\Resources\\ClickSound.wav";
            clickContextMenuSound = @"pd\Resources\ClickContextMenuSound.wav";
        }
    }
}