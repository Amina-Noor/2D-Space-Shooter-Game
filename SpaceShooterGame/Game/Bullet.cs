namespace SpaceShooterGame.Game
{
    /// <summary>
    /// Projectile fired by the player. Moves upward each frame.
    /// </summary>
    public class Bullet : GameObject
    {
        public float Speed { get; set; }

        public Bullet(float x, float y, float speed)
            : base(x, y, 6, 16)
        {
            Speed = speed;
        }

        public override void Update()
        {
            Y -= Speed;
            if (Y + Height < 0)
            {
                IsAlive = false;
            }
        }
    }
}
