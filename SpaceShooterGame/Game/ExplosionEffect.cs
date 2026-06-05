namespace SpaceShooterGame.Game
{
    /// <summary>
    /// Short-lived explosion animation when an enemy is destroyed.
    /// </summary>
    public class ExplosionEffect
    {
        public float X { get; set; }
        public float Y { get; set; }
        public int Frame { get; set; }
        public int MaxFrames { get; set; }
        public bool IsFinished { get; private set; }

        public ExplosionEffect(float x, float y)
        {
            X = x;
            Y = y;
            Frame = 0;
            MaxFrames = 12;
            IsFinished = false;
        }

        public void Update()
        {
            Frame++;
            if (Frame >= MaxFrames)
            {
                IsFinished = true;
            }
        }
    }
}
