using System;
using System.Windows.Forms;

namespace SpaceShooterGame
{
    /// <summary>
    /// Shown when the player runs out of lives. Offers restart or return to menu.
    /// </summary>
    public partial class GameOverForm : Form
    {
        public bool RestartRequested { get; private set; }

        public GameOverForm(int score, int level)
        {
            InitializeComponent();
            lblFinalScore.Text = "Final Score: " + score;
            lblLevelReached.Text = "Level Reached: " + level;
            RestartRequested = false;
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            RestartRequested = true;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            RestartRequested = false;
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
