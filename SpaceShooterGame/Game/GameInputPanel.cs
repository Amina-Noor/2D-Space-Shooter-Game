using System.Windows.Forms;

namespace SpaceShooterGame.Game
{
    /// <summary>
    /// Play area panel that accepts arrow keys and Space (standard Panel treats them as navigation keys).
    /// </summary>
    public class GameInputPanel : Panel
    {
        public GameInputPanel()
        {
            SetStyle(ControlStyles.Selectable, true);
            TabStop = true;
            Cursor = Cursors.Cross;
        }

        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData & Keys.KeyCode)
            {
                case Keys.Up:
                case Keys.Down:
                case Keys.Left:
                case Keys.Right:
                case Keys.Space:
                    return true;
            }
            return base.IsInputKey(keyData);
        }
    }
}
