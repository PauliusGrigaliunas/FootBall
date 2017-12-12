using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;


namespace Football
{
    public class Commentator
    {
        public static bool isMuted = false; 
        public event EventHandler Sounds;
        private SoundPlayer[] sounds;
        private static int _lastPlayedSongIndex = 10;

        public Commentator()
        {
            sounds = new SoundPlayer[23];
            sounds[0] = new SoundPlayer(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\GitHub\FootBall\Football\Football\Sounds\multikill.wav");
            sounds[1] = new SoundPlayer(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\GitHub\FootBall\Football\Football\Sounds\damnson.wav");
            sounds[2] = new SoundPlayer(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\GitHub\FootBall\Football\Football\Sounds\darkness.wav");
            sounds[3] = new SoundPlayer(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\GitHub\FootBall\Football\Football\Sounds\eagleeye.wav");
            sounds[4] = new SoundPlayer(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\GitHub\FootBall\Football\Football\Sounds\headshot.wav");
            sounds[5] = new SoundPlayer(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\GitHub\FootBall\Football\Football\Sounds\johncena.wav");
            sounds[6] = new SoundPlayer(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\GitHub\FootBall\Football\Football\Sounds\niceshot.wav");
            sounds[7] = new SoundPlayer(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\GitHub\FootBall\Football\Football\Sounds\SitasTai.wav");
            sounds[8] = new SoundPlayer(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\GitHub\FootBall\Football\Football\Sounds\unstopabble.wav"); 
            // till this one all scoring sounds

            //End game, background music
            sounds[9] = new SoundPlayer(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\GitHub\FootBall\Football\Football\Sounds\victory.wav");                   // victory sound
            sounds[10] = new SoundPlayer(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\GitHub\FootBall\Football\Football\Sounds\startsong.wav");                  // startsong
            sounds[11] = new SoundPlayer(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\GitHub\FootBall\Football\Football\Sounds\background.wav");

            //Commentator's sounds
            sounds[12] = new SoundPlayer(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\GitHub\FootBall\Football\Football\Sounds\Vartininkas.wav");
            sounds[13] = new SoundPlayer(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\GitHub\FootBall\Football\Football\Sounds\VartininkasSU.wav");
            sounds[14] = new SoundPlayer(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\GitHub\FootBall\Football\Football\Sounds\Puolejas.wav");
            sounds[15] = new SoundPlayer(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\GitHub\FootBall\Football\Football\Sounds\PuolejasSU.wav");
            sounds[16] = new SoundPlayer(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\GitHub\FootBall\Football\Football\Sounds\Gynejas.wav");
            sounds[17] = new SoundPlayer(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\GitHub\FootBall\Football\Football\Sounds\GynejasSU.wav");
            sounds[18] = new SoundPlayer(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\GitHub\FootBall\Football\Football\Sounds\SuKamuoliu.wav");
            sounds[19] = new SoundPlayer(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\GitHub\FootBall\Football\Football\Sounds\Leisk.wav");
            sounds[20] = new SoundPlayer(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\GitHub\FootBall\Football\Football\Sounds\Smugis.wav");
            sounds[21] = new SoundPlayer(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\GitHub\FootBall\Football\Football\Sounds\Taikosi.wav");
            sounds[22] = new SoundPlayer(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\GitHub\FootBall\Football\Football\Sounds\Pasas.wav");

        }
        public void PlayRandomSound( int min, int max )
        {
            _lastPlayedSongIndex = GetRandomNumber( min, max );
            if (!isMuted) sounds[_lastPlayedSongIndex].Play();
        }
        private int GetRandomNumber( int min, int max )
        {
            Random rnd = new Random();
            int number = rnd.Next(min, max);
            return number;
        }
        public void PlayIndexedSound(int index)
        {
            if (!isMuted)
            {
                _lastPlayedSongIndex = index;
                sounds[index].Play();
            }
        }
        public void PlayIndexedSoundWithLoop (int index)
        {
            if (!isMuted)
            {
                _lastPlayedSongIndex = index;
                sounds[index].PlayLooping();
            }
        }
        public void StopAllTracks()
        {
            for (int i = 0; i <= 22; i++)
            {
                sounds[i].Stop();
            }
        }
        public void Mute()
        {
            if (isMuted)
            {
                isMuted = false;
                PlayIndexedSound(_lastPlayedSongIndex);
            }
            else
            {
                isMuted = true;
                StopAllTracks();
            }
        }
        internal bool CommentPlayGround(string Position, string ATeam, string BTeam, bool isRinged )
        {
            if (Position == ATeam + " Team Defenders" || Position == BTeam + " Team Defenders")
            {
                if (isRinged == false)
                {
                    StopAllTracks();
                    PlayRandomSound(16, 18);
                    isRinged = true;
                }
            }
            else if (Position == ATeam + " Team Attackers" || Position == BTeam + " Team Attackers")
            {
                if (isRinged == false)
                {
                    StopAllTracks();
                    PlayRandomSound(14, 16);
                    isRinged = true;
                }
            }
            else if (Position == ATeam + " Team Middle 5" || Position == BTeam + " Team Middle 5")
            {
                if (isRinged == false)
                {
                    StopAllTracks();
                    PlayRandomSound(14, 16);
                    isRinged = true;
                }
            }
            else
            {
                if (isRinged == false)
                {
                    StopAllTracks();
                    PlayRandomSound(12, 14);
                    isRinged = true;
                }
            }
            return isRinged;
        }
    }
}
