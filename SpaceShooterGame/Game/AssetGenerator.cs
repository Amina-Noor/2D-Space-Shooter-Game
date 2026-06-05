using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace SpaceShooterGame.Game
{
    /// <summary>
    /// Creates placeholder PNG and WAV files on first run if they are missing.
    /// Beginners can also run Scripts\GenerateAssets.ps1 manually before building.
    /// </summary>
    public static class AssetGenerator
    {
        public static void EnsureAssetsExist()
        {
            string basePath = Application.StartupPath;
            string assetsPath = Path.Combine(basePath, "Assets");
            string soundsPath = Path.Combine(basePath, "Sounds");

            Directory.CreateDirectory(assetsPath);
            Directory.CreateDirectory(soundsPath);

            CreatePngIfMissing(Path.Combine(assetsPath, "player.png"), DrawPlayer, 48, 48);
            CreatePngIfMissing(Path.Combine(assetsPath, "enemy.png"), DrawEnemy, 40, 40);
            CreatePngIfMissing(Path.Combine(assetsPath, "bullet.png"), DrawBullet, 6, 16);
            CreatePngIfMissing(Path.Combine(assetsPath, "background.png"), DrawBackground, 640, 400);

            CreateWavIfMissing(Path.Combine(soundsPath, "shoot.wav"), 880, 80);
            CreateWavIfMissing(Path.Combine(soundsPath, "explosion.wav"), 120, 200);
            CreateWavIfMissing(Path.Combine(soundsPath, "hit.wav"), 200, 150);
        }

        private static void CreatePngIfMissing(string path, Action<Graphics, int, int> draw, int w, int h)
        {
            if (File.Exists(path)) return;

            using (Bitmap bmp = new Bitmap(w, h))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    draw(g, w, h);
                }
                bmp.Save(path, ImageFormat.Png);
            }
        }

        private static void DrawPlayer(Graphics g, int w, int h)
        {
            g.Clear(Color.Transparent);
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(0, 200, 255)))
            {
                g.FillPolygon(brush, new[]
                {
                    new Point(w / 2, 4),
                    new Point(4, h - 4),
                    new Point(w - 4, h - 4)
                });
            }
        }

        private static void DrawEnemy(Graphics g, int w, int h)
        {
            g.Clear(Color.Transparent);
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(255, 80, 100)))
            {
                g.FillPolygon(brush, new[]
                {
                    new Point(w / 2, h - 4),
                    new Point(4, 4),
                    new Point(w - 4, 4)
                });
            }
        }

        private static void DrawBullet(Graphics g, int w, int h)
        {
            g.Clear(Color.Transparent);
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(255, 255, 100)))
            {
                g.FillRectangle(brush, 0, 0, w, h);
            }
        }

        private static void DrawBackground(Graphics g, int w, int h)
        {
            g.Clear(Color.FromArgb(8, 12, 28));
            Random rand = new Random(42);
            for (int i = 0; i < 120; i++)
            {
                int b = 100 + rand.Next(155);
                using (SolidBrush star = new SolidBrush(Color.FromArgb(b, b, Math.Min(255, b + 20))))
                {
                    g.FillEllipse(star, rand.Next(w), rand.Next(h), 2, 2);
                }
            }
        }

        private static void CreateWavIfMissing(string path, int frequency, int durationMs)
        {
            if (File.Exists(path)) return;

            int sampleRate = 22050;
            int samples = sampleRate * durationMs / 1000;
            byte[] data = new byte[samples * 2];

            for (int i = 0; i < samples; i++)
            {
                double t = (double)i / sampleRate;
                double fade = 1.0;
                if (i < samples * 0.05) fade = i / (samples * 0.05);
                if (i > samples * 0.85) fade = 1.0 - (i - samples * 0.85) / (samples * 0.15);

                short sample = (short)(Math.Sin(2 * Math.PI * frequency * t) * 0.35 * fade * 32767);
                data[i * 2] = (byte)(sample & 0xFF);
                data[i * 2 + 1] = (byte)((sample >> 8) & 0xFF);
            }

            using (MemoryStream stream = new MemoryStream())
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                writer.Write(new[] { 'R', 'I', 'F', 'F' });
                writer.Write(36 + data.Length);
                writer.Write(new[] { 'W', 'A', 'V', 'E' });
                writer.Write(new[] { 'f', 'm', 't', ' ' });
                writer.Write(16);
                writer.Write((short)1);
                writer.Write((short)1);
                writer.Write(sampleRate);
                writer.Write(sampleRate * 2);
                writer.Write((short)2);
                writer.Write((short)16);
                writer.Write(new[] { 'd', 'a', 't', 'a' });
                writer.Write(data.Length);
                writer.Write(data);
                File.WriteAllBytes(path, stream.ToArray());
            }
        }
    }
}
