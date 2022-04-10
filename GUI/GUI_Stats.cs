using System.Drawing;

namespace RogueWorld.GUI
{
    internal class GUI_Stats : GUI 
    {
       
        public GUI_Stats(int guiOriginX, int guiOriginY,
            int guiWidth, int guiHeight,
            char borderChar, char fillingChar,
            ConsoleColor borderColor, ConsoleColor fillingColor) {

            Origin = new Point(guiOriginX, guiOriginY);
            Window = new Rectangle(Origin.X, Origin.Y, guiWidth, guiHeight);
            BorderChar = borderChar;
            FillingChar = fillingChar;
            BorderColor = borderColor;
            FillingColor = fillingColor;
            Title = "Stats";

        }

        public override void DrawContent()
        {
            Util.Write("Health " + GameManager.Instance.Rogue.Attributes.CurrentHealth +
                " / " + GameManager.Instance.Rogue.Attributes.MaxHealth, Origin.X + 1, Origin.Y + 1);

            Util.Write("Strength " + GameManager.Instance.Rogue.Attributes.CurrentStrength, Origin.X + 1, Origin.Y + 2);

            Util.Write("Speed " + GameManager.Instance.Rogue.Attributes.CurrentSpeed, Origin.X + 1, Origin.Y + 3);

            Util.Write("Intelligence " + GameManager.Instance.Rogue.Attributes.CurrentIntelligence, Origin.X + 1, Origin.Y + 4);
        }
    }
}
