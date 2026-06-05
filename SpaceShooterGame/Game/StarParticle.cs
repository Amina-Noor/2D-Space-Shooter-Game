using System;

namespace SpaceShooterGame.Game
{
    /// <summary>
    /// Single star used for scrolling space background effect.
    /// </summary>
    public class StarParticle
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Speed { get; set; }
        public int Size { get; set; }
        public int Brightness { get; set; }

        private readonly Random _random;
        private readonly int _areaWidth;
        private readonly int _areaHeight;

        public StarParticle(Random random, int areaWidth, int areaHeight)
        {
            _random = random;
            _areaWidth = areaWidth;
            _areaHeight = areaHeight;
            Reset(true);
        }

        public void Reset(bool randomY)
        {
            X = (float)(_random.NextDouble() * _areaWidth);
            Y = randomY ? (float)(_random.NextDouble() * _areaHeight) : -Size;
            Speed = 1f + (float)(_random.NextDouble() * 4);
            Size = 1 + _random.Next(3);
            // Max 235 so DrawStar can add +20 to blue without exceeding 255.
            Brightness = 120 + _random.Next(116);
        }

        public void Update(float speedMultiplier)
        {
            Y += Speed * speedMultiplier;
            if (Y > _areaHeight)
            {
                Reset(false);
            }
        }
    }
}
