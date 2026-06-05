using System;
using System.Windows.Forms;

namespace SpaceShooterGame
{
    /// <summary>
    /// Start menu screen with Start and Exit buttons.
    /// </summary>
    public partial class MainMenuForm : Form
    {
        public MainMenuForm()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Hide();
            using (GameForm game = new GameForm())
            {
                game.ShowDialog();
            }
            Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
