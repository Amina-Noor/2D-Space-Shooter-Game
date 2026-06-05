using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using SpaceShooterGame.Game;

namespace SpaceShooterGame
{
    /// <summary>
    /// Main gameplay form. Handles input, spawning, collisions, scoring, and rendering.
    /// </summary>
    public partial class GameForm : Form
    {
        // Game state
        private PlayerShip _player;
        private readonly List<Bullet> _bullets = new List<Bullet>();
        private readonly List<EnemyShip> _enemies = new List<EnemyShip>();
        private readonly List<ExplosionEffect> _explosions = new List<ExplosionEffect>();
        private readonly List<StarParticle> _stars = new List<StarParticle>();
        private readonly Random _random = new Random();
        private SoundHelper _sounds;

        // Difficulty
        private int _score;
        private int _level = 1;
        private float _difficultySpeed = 1f;
        private int _framesSinceSpawn;
        private int _spawnInterval = 90;
        private int _shootCooldown;
        private const int ShootCooldownFrames = 12;

        // Input flags
        private bool _keyLeft;
        private bool _keyRight;
        private bool _keyUp;
        private bool _keyDown;
        private bool _keySpace;
        private bool _isPaused;
        private bool _gameRunning = true;

        // Optional images from Assets folder
        private Image _imgPlayer;
        private Image _imgEnemy;
        private Image _imgBullet;
        private Image _imgBackground;

        public GameForm()
        {
            InitializeComponent();
            EnableDoubleBuffering(gamePanel);
        }

        /// <summary>Ensures key-release is always detected even when ProcessCmdKey handled key-down.</summary>
        protected override void OnKeyUp(KeyEventArgs e)
        {
            ApplyKeyUp(e.KeyCode);
            base.OnKeyUp(e);
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            _sounds = new SoundHelper();
            LoadOptionalAssets();
            ResetGame();
            FocusGamePanel();
            gameTimer.Start();
        }

        private void GameForm_Activated(object sender, EventArgs e)
        {
            if (_gameRunning && !_isPaused)
            {
                FocusGamePanel();
            }
        }

        /// <summary>Gives keyboard focus to the play area so arrow keys and Space work.</summary>
        private void FocusGamePanel()
        {
            gamePanel.Focus();
        }

        private void gamePanel_MouseDown(object sender, MouseEventArgs e)
        {
            FocusGamePanel();
        }

        private void GameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            gameTimer.Stop();
            DisposeImages();
            if (_sounds != null)
            {
                _sounds.Dispose();
            }
        }

        /// <summary>Reduces flicker by enabling double buffering on the draw panel.</summary>
        private static void EnableDoubleBuffering(Control control)
        {
            typeof(Control).InvokeMember(
                "DoubleBuffered",
                System.Reflection.BindingFlags.SetProperty |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.NonPublic,
                null,
                control,
                new object[] { true });
        }

        private void LoadOptionalAssets()
        {
            string assets = Path.Combine(Application.StartupPath, "Assets");
            _imgPlayer = TryLoadImage(Path.Combine(assets, "player.png"));
            _imgEnemy = TryLoadImage(Path.Combine(assets, "enemy.png"));
            _imgBullet = TryLoadImage(Path.Combine(assets, "bullet.png"));
            _imgBackground = TryLoadImage(Path.Combine(assets, "background.png"));
        }

        private static Image TryLoadImage(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    return Image.FromFile(path);
                }
            }
            catch
            {
            }
            return null;
        }

        private void DisposeImages()
        {
            if (_imgPlayer != null) _imgPlayer.Dispose();
            if (_imgEnemy != null) _imgEnemy.Dispose();
            if (_imgBullet != null) _imgBullet.Dispose();
            if (_imgBackground != null) _imgBackground.Dispose();
        }

        private void ResetGame()
        {
            _bullets.Clear();
            _enemies.Clear();
            _explosions.Clear();
            _stars.Clear();

            int w = gamePanel.ClientSize.Width;
            int h = gamePanel.ClientSize.Height;

            _player = new PlayerShip(w / 2f - 24, h - 70);
            _score = 0;
            _level = 1;
            _difficultySpeed = 1f;
            _framesSinceSpawn = 0;
            _spawnInterval = 90;
            _shootCooldown = 0;
            _isPaused = false;
            _gameRunning = true;
            lblPause.Visible = false;
            btnPause.Text = "PAUSE";

            for (int i = 0; i < 80; i++)
            {
                _stars.Add(new StarParticle(_random, w, h));
            }

            UpdateHud();
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            if (!_gameRunning || _isPaused)
            {
                gamePanel.Invalidate();
                return;
            }

            UpdateStars();
            UpdatePlayerMovement();
            TryShoot();
            UpdateBullets();
            SpawnEnemies();
            UpdateEnemies();
            CheckCollisions();
            UpdateExplosions();
            IncreaseDifficulty();
            UpdateHud();
            gamePanel.Invalidate();
        }

        private void UpdateStars()
        {
            foreach (StarParticle star in _stars)
            {
                star.Update(_difficultySpeed * 0.5f);
            }
        }

        private void UpdatePlayerMovement()
        {
            float speed = _player.Speed * (_difficultySpeed * 0.3f + 0.9f);
            if (_keyLeft) _player.X -= speed;
            if (_keyRight) _player.X += speed;
            if (_keyUp) _player.Y -= speed;
            if (_keyDown) _player.Y += speed;

            _player.ClampToBounds(0, 0, gamePanel.ClientSize.Width, gamePanel.ClientSize.Height);
        }

        private void TryShoot()
        {
            if (_shootCooldown > 0)
            {
                _shootCooldown--;
            }

            if (_keySpace && _shootCooldown <= 0)
            {
                float bx = _player.X + _player.Width / 2f - 3;
                float by = _player.Y - 10;
                _bullets.Add(new Bullet(bx, by, 10f + _level));
                _shootCooldown = ShootCooldownFrames;
                _sounds.PlayShoot();
            }
        }

        private void UpdateBullets()
        {
            for (int i = _bullets.Count - 1; i >= 0; i--)
            {
                _bullets[i].Update();
                if (!_bullets[i].IsAlive)
                {
                    _bullets.RemoveAt(i);
                }
            }
        }

        private void SpawnEnemies()
        {
            _framesSinceSpawn++;
            if (_framesSinceSpawn >= _spawnInterval)
            {
                _framesSinceSpawn = 0;
                int w = gamePanel.ClientSize.Width;
                float x = _random.Next(20, Math.Max(40, w - 60));
                float speed = 2f + _level * 0.4f + (float)_random.NextDouble();
                int points = 10 + _level * 2;
                _enemies.Add(new EnemyShip(x, -45, speed, points));
            }
        }

        private void UpdateEnemies()
        {
            int h = gamePanel.ClientSize.Height;
            for (int i = _enemies.Count - 1; i >= 0; i--)
            {
                _enemies[i].Update();
                if (_enemies[i].Y > h + 50 || !_enemies[i].IsAlive)
                {
                    _enemies.RemoveAt(i);
                }
            }
        }

        private void CheckCollisions()
        {
            // Bullets vs enemies
            for (int b = _bullets.Count - 1; b >= 0; b--)
            {
                if (!_bullets[b].IsAlive) continue;

                for (int e = _enemies.Count - 1; e >= 0; e--)
                {
                    if (!_enemies[e].IsAlive) continue;

                    if (CollisionHelper.Intersects(_bullets[b], _enemies[e]))
                    {
                        _score += _enemies[e].Points;
                        float ex = _enemies[e].X + _enemies[e].Width / 2f;
                        float ey = _enemies[e].Y + _enemies[e].Height / 2f;
                        _explosions.Add(new ExplosionEffect(ex, ey));
                        _enemies[e].IsAlive = false;
                        _bullets[b].IsAlive = false;
                        _sounds.PlayExplosion();
                        break;
                    }
                }
            }

            // Player vs enemies
            for (int e = _enemies.Count - 1; e >= 0; e--)
            {
                if (!_enemies[e].IsAlive) continue;

                if (CollisionHelper.Intersects(_player, _enemies[e]))
                {
                    _player.Lives--;
                    _enemies[e].IsAlive = false;
                    _explosions.Add(new ExplosionEffect(
                        _enemies[e].X + _enemies[e].Width / 2f,
                        _enemies[e].Y + _enemies[e].Height / 2f));
                    _sounds.PlayHit();

                    if (_player.Lives <= 0)
                    {
                        EndGame();
                    }
                }
            }
        }

        private void UpdateExplosions()
        {
            for (int i = _explosions.Count - 1; i >= 0; i--)
            {
                _explosions[i].Update();
                if (_explosions[i].IsFinished)
                {
                    _explosions.RemoveAt(i);
                }
            }
        }

        private void IncreaseDifficulty()
        {
            // Level increases every 200 points
            int newLevel = 1 + _score / 200;
            if (newLevel != _level)
            {
                _level = newLevel;
                _difficultySpeed = 1f + (_level - 1) * 0.15f;
                _spawnInterval = Math.Max(35, 90 - _level * 5);
            }
        }

        private void UpdateHud()
        {
            lblScore.Text = "Score: " + _score;
            lblHealth.Text = "Lives: " + _player.Lives;
            lblLevel.Text = string.Format("Level: {0} | Spd: {1:0.0}", _level, _difficultySpeed);
        }

        private void EndGame()
        {
            _gameRunning = false;
            gameTimer.Stop();

            using (GameOverForm over = new GameOverForm(_score, _level))
            {
                over.ShowDialog(this);

                if (over.RestartRequested)
                {
                    ResetGame();
                    FocusGamePanel();
                    gameTimer.Start();
                }
                else
                {
                    Close();
                }
            }
        }

        private void gamePanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            Rectangle bounds = gamePanel.ClientRectangle;

            // Dark space background
            using (SolidBrush bg = new SolidBrush(Color.FromArgb(5, 8, 20)))
            {
                g.FillRectangle(bg, bounds);
            }

            // Optional tiled background image
            if (_imgBackground != null)
            {
                g.DrawImage(_imgBackground, bounds);
                using (SolidBrush overlay = new SolidBrush(Color.FromArgb(120, 0, 0, 0)))
                {
                    g.FillRectangle(overlay, bounds);
                }
            }

            // Scrolling stars
            foreach (StarParticle star in _stars)
            {
                GameRenderer.DrawStar(g, star);
            }

            if (_player == null) return;

            // Draw game entities
            GameRenderer.DrawPlayer(g, _player, _imgPlayer);

            foreach (Bullet bullet in _bullets)
            {
                if (bullet.IsAlive)
                {
                    GameRenderer.DrawBullet(g, bullet, _imgBullet);
                }
            }

            foreach (EnemyShip enemy in _enemies)
            {
                if (enemy.IsAlive)
                {
                    GameRenderer.DrawEnemy(g, enemy, _imgEnemy);
                }
            }

            foreach (ExplosionEffect explosion in _explosions)
            {
                GameRenderer.DrawExplosion(g, explosion);
            }

            if (_isPaused)
            {
                using (SolidBrush dim = new SolidBrush(Color.FromArgb(160, 0, 0, 0)))
                {
                    g.FillRectangle(dim, bounds);
                }
            }
        }

        /// <summary>
        /// Catches movement and shoot keys before buttons consume them (e.g. Space on a button).
        /// </summary>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (!_gameRunning)
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }

            Keys key = keyData & Keys.KeyCode;

            if (!_isPaused)
            {
                if (key == Keys.Left || key == Keys.Right || key == Keys.Up || key == Keys.Down ||
                    key == Keys.Space || key == Keys.A || key == Keys.D || key == Keys.W || key == Keys.S)
                {
                    ApplyKeyDown(key);
                    return true;
                }
            }

            if (key == Keys.P)
            {
                TogglePause();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void GameInput_KeyDown(object sender, KeyEventArgs e)
        {
            ApplyKeyDown(e.KeyCode);

            if (e.KeyCode == Keys.P)
            {
                TogglePause();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                if (MessageBox.Show("Return to main menu?", "Exit Game",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Close();
                }
            }
        }

        private void GameInput_KeyUp(object sender, KeyEventArgs e)
        {
            ApplyKeyUp(e.KeyCode);
        }

        private void ApplyKeyDown(Keys key)
        {
            switch (key)
            {
                case Keys.Left:
                case Keys.A:
                    _keyLeft = true;
                    break;
                case Keys.Right:
                case Keys.D:
                    _keyRight = true;
                    break;
                case Keys.Up:
                case Keys.W:
                    _keyUp = true;
                    break;
                case Keys.Down:
                case Keys.S:
                    _keyDown = true;
                    break;
                case Keys.Space:
                    _keySpace = true;
                    break;
            }
        }

        private void ApplyKeyUp(Keys key)
        {
            switch (key)
            {
                case Keys.Left:
                case Keys.A:
                    _keyLeft = false;
                    break;
                case Keys.Right:
                case Keys.D:
                    _keyRight = false;
                    break;
                case Keys.Up:
                case Keys.W:
                    _keyUp = false;
                    break;
                case Keys.Down:
                case Keys.S:
                    _keyDown = false;
                    break;
                case Keys.Space:
                    _keySpace = false;
                    break;
            }
        }

        private void TogglePause()
        {
            if (!_gameRunning) return;

            _isPaused = !_isPaused;
            lblPause.Visible = _isPaused;
            btnPause.Text = _isPaused ? "RESUME" : "PAUSE";

            if (_isPaused)
            {
                ClearInputKeys();
            }
            else
            {
                FocusGamePanel();
            }
        }

        private void ClearInputKeys()
        {
            _keyLeft = false;
            _keyRight = false;
            _keyUp = false;
            _keyDown = false;
            _keySpace = false;
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            TogglePause();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Quit current game?", "Exit",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Close();
            }
        }
    }
}
