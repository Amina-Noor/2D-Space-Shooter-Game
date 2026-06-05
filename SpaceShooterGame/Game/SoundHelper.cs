using System;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace SpaceShooterGame.Game
{
    /// <summary>
    /// Plays sound effects from the Sounds folder. Falls back to system beeps if files are missing.
    /// </summary>
    public class SoundHelper : IDisposable
    {
        private SoundPlayer _shootPlayer;
        private SoundPlayer _explosionPlayer;
        private SoundPlayer _hitPlayer;
        private bool _enabled = true;

        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        public SoundHelper()
        {
            TryLoadSounds();
        }

        private void TryLoadSounds()
        {
            string basePath = Application.StartupPath;
            _shootPlayer = LoadSound(Path.Combine(basePath, "Sounds", "shoot.wav"));
            _explosionPlayer = LoadSound(Path.Combine(basePath, "Sounds", "explosion.wav"));
            _hitPlayer = LoadSound(Path.Combine(basePath, "Sounds", "hit.wav"));
        }

        private static SoundPlayer LoadSound(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    return new SoundPlayer(path);
                }
            }
            catch
            {
                // Ignore load errors; fallback beeps will be used.
            }
            return null;
        }

        public void PlayShoot()
        {
            if (!_enabled) return;
            if (_shootPlayer != null)
            {
                _shootPlayer.Play();
            }
            else
            {
                SystemSounds.Asterisk.Play();
            }
        }

        public void PlayExplosion()
        {
            if (!_enabled) return;
            if (_explosionPlayer != null)
            {
                _explosionPlayer.Play();
            }
            else
            {
                SystemSounds.Exclamation.Play();
            }
        }

        public void PlayHit()
        {
            if (!_enabled) return;
            if (_hitPlayer != null)
            {
                _hitPlayer.Play();
            }
            else
            {
                SystemSounds.Hand.Play();
            }
        }

        public void Dispose()
        {
            if (_shootPlayer != null) _shootPlayer.Dispose();
            if (_explosionPlayer != null) _explosionPlayer.Dispose();
            if (_hitPlayer != null) _hitPlayer.Dispose();
        }
    }
}
