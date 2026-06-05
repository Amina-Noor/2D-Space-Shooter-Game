using System.Drawing;
using System.Drawing.Drawing2D;

namespace SpaceShooterGame.Game
{
    /// <summary>
    /// Draws vector-style spaceships and effects using GDI+ (no external art required).
    /// Optional PNG images from Assets folder are used when available.
    /// </summary>
    public static class GameRenderer
    {
        public static void DrawPlayer(Graphics g, PlayerShip player, Image optionalImage)
        {
            if (optionalImage != null)
            {
                g.DrawImage(optionalImage, player.X, player.Y, player.Width, player.Height);
                return;
            }

            float cx = player.X + player.Width / 2f;
            float cy = player.Y + player.Height / 2f;
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddPolygon(new[]
                {
                    new PointF(cx, player.Y),
                    new PointF(player.X, player.Y + player.Height),
                    new PointF(cx, player.Y + player.Height - 8),
                    new PointF(player.X + player.Width, player.Y + player.Height)
                });
                using (SolidBrush body = new SolidBrush(Color.FromArgb(0, 200, 255)))
                using (Pen outline = new Pen(Color.FromArgb(180, 240, 255), 2f))
                {
                    g.FillPath(body, path);
                    g.DrawPath(outline, path);
                }
            }
            using (SolidBrush cockpit = new SolidBrush(Color.FromArgb(255, 255, 120)))
            {
                g.FillEllipse(cockpit, cx - 6, cy - 4, 12, 10);
            }
        }

        public static void DrawEnemy(Graphics g, EnemyShip enemy, Image optionalImage)
        {
            if (optionalImage != null)
            {
                g.DrawImage(optionalImage, enemy.X, enemy.Y, enemy.Width, enemy.Height);
                return;
            }

            float cx = enemy.X + enemy.Width / 2f;
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddPolygon(new[]
                {
                    new PointF(cx, enemy.Y + enemy.Height),
                    new PointF(enemy.X, enemy.Y),
                    new PointF(cx, enemy.Y + 8),
                    new PointF(enemy.X + enemy.Width, enemy.Y)
                });
                using (SolidBrush body = new SolidBrush(Color.FromArgb(255, 80, 100)))
                using (Pen outline = new Pen(Color.FromArgb(255, 150, 160), 2f))
                {
                    g.FillPath(body, path);
                    g.DrawPath(outline, path);
                }
            }
        }

        public static void DrawBullet(Graphics g, Bullet bullet, Image optionalImage)
        {
            if (optionalImage != null)
            {
                g.DrawImage(optionalImage, bullet.X, bullet.Y, bullet.Width, bullet.Height);
                return;
            }

            using (SolidBrush brush = new SolidBrush(Color.FromArgb(255, 255, 100)))
            {
                g.FillRectangle(brush, bullet.X, bullet.Y, bullet.Width, bullet.Height);
            }
        }

        public static void DrawExplosion(Graphics g, ExplosionEffect explosion)
        {
            float progress = explosion.Frame / (float)explosion.MaxFrames;
            int radius = (int)(8 + progress * 28);
            int alpha = (int)(255 * (1f - progress));
            using (Pen pen = new Pen(Color.FromArgb(alpha, 255, 180, 60), 3f))
            using (SolidBrush core = new SolidBrush(Color.FromArgb(alpha / 2, 255, 120, 40)))
            {
                g.DrawEllipse(pen, explosion.X - radius, explosion.Y - radius, radius * 2, radius * 2);
                g.FillEllipse(core, explosion.X - radius / 2f, explosion.Y - radius / 2f, radius, radius);
            }
        }

        public static void DrawStar(Graphics g, StarParticle star)
        {
            // Clamp RGB to 0–255; blue is slightly brighter than red/green for a cool star tint.
            int r = star.Brightness;
            int gVal = star.Brightness;
            int b = star.Brightness + 20;
            if (b > 255) b = 255;

            using (SolidBrush brush = new SolidBrush(Color.FromArgb(r, gVal, b)))
            {
                g.FillEllipse(brush, star.X, star.Y, star.Size, star.Size);
            }
        }
    }
}
