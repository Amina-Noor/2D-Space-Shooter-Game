using System.Drawing;

namespace SpaceShooterGame.Game
{
    /// <summary>
    /// Simple axis-aligned rectangle collision checks for game objects.
    /// </summary>
    public static class CollisionHelper
    {
        public static bool Intersects(GameObject a, GameObject b)
        {
            return Intersects(a.GetBounds(), b.GetBounds());
        }

        public static bool Intersects(RectangleF a, RectangleF b)
        {
            return a.IntersectsWith(b);
        }
    }
}
