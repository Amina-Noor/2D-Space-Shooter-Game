using System.Drawing;

namespace SpaceShooterGame.Game
{
    /// <summary>
    /// Player-controlled spaceship. Movement is handled in GameForm using arrow keys.
    /// </summary>
    public class PlayerShip : GameObject
    {
        public int Lives { get; set; }
        public float Speed { get; set; }

        public PlayerShip(float x, float y)
            : base(x, y, 48, 48)
        {
            Lives = 3;
            Speed = 6f;
        }

        public override void Update()
        {
            // Movement is applied externally from keyboard input in GameForm.
        }

        /// <summary>Keeps the ship inside the play area.</summary>
        public void ClampToBounds(int minX, int minY, int maxX, int maxY)
        {
            if (X < minX) X = minX;
            if (Y < minY) Y = minY;
            if (X + Width > maxX) X = maxX - Width;
            if (Y + Height > maxY) Y = maxY - Height;
        }
    }
}
