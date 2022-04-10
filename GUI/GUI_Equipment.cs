using System.Drawing;

namespace RogueWorld.GUI
{
    internal class GUI_Equipment : GUI
    {
        public GUI_Equipment(int guiOriginX, int guiOriginY,
            int guiWidth, int guiHeight,
            char borderChar, char fillingChar,
            ConsoleColor borderColor, ConsoleColor fillingColor)
        {

            Origin = new Point(guiOriginX, guiOriginY);
            Window = new Rectangle(Origin.X, Origin.Y, guiWidth, guiHeight);
            BorderChar = borderChar;
            FillingChar = fillingChar;
            BorderColor = borderColor;
            FillingColor = fillingColor;
            Title = "Equipment";

        }

        public override void DrawContent()
        {
            //
        }

    }
}
