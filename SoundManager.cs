using System;
using System.Windows.Media;

namespace kjtStore
{
    static class SoundManager
    {
        private static MediaPlayer mpPlayer;
        private static string clickSoundPath;

        static SoundManager()
        {
            Preparing();
        }

        public static void PlayClick()
        {
            mpPlayer.Open(new Uri(clickSoundPath, UriKind.Relative));
            mpPlayer.Play();
        }

        public static void PlaySound(string SoundPath)
        {
            mpPlayer.Open(new Uri(SoundPath, UriKind.Relative));
            mpPlayer.Play();
        }

        private static void Preparing()
        {
            if(mpPlayer == null)
            {
                mpPlayer = new MediaPlayer();
            }
            clickSoundPath = @"pd\\Resources\\ClickSound.wav";
        }
    }
}