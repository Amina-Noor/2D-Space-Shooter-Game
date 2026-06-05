namespace SpaceShooterGame.Game
{
    /// <summary>
    /// Enemy spaceship that moves downward toward the player.
    /// </summary>
    public class EnemyShip : GameObject
    {
        public float Speed { get; set; }
        public int Points { get; set; }

        public EnemyShip(float x, float y, float speed, int points)
            : base(x, y, 40, 40)
        {
            Speed = speed;
            Points = points;
        }

        public override void Update()
        {
            Y += Speed;
        }
    }
}
