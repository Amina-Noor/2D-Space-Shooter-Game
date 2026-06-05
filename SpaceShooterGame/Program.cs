using System;
using System.Windows.Forms;
using SpaceShooterGame.Game;

namespace SpaceShooterGame
{
    /// <summary>
    /// Application entry point. Starts the main menu form.
    /// </summary>
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Create placeholder PNG/WAV files in output folder if missing (first run).
            AssetGenerator.EnsureAssetsExist();

            Application.Run(new MainMenuForm());
        }
    }
}
