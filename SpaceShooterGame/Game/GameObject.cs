using System.Drawing;

namespace SpaceShooterGame.Game
{
    /// <summary>
    /// Base class for all moving game entities (player, enemies, bullets).
    /// </summary>
    public abstract class GameObject
    {
        public float X { get; set; }
        public float Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool IsAlive { get; set; }

        public GameObject(float x, float y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            IsAlive = true;
        }

        /// <summary>Returns a rectangle used for collision detection.</summary>
        public RectangleF GetBounds()
        {
            return new RectangleF(X, Y, Width, Height);
        }

        public abstract void Update();
    }
}
