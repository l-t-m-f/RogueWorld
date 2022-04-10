using System.Drawing;

namespace RogueWorld.GUI
{
    internal class GUI_ActiveItem : GUI
    {
        public GUI_ActiveItem(int guiOriginX, int guiOriginY,
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
            Title = "";

        }

        public override void DrawContent()
        {
            
        }
    }
}