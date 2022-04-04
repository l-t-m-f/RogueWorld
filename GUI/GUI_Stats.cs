using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueWorld.Managers;

using RogueWorld.Utilities;

namespace RogueWorld.GUI
{ 
    internal class GUI_Stats : GUI 
    {
        public GUI_Stats(int guiOriginX, int guiOriginY,
            int guiWidth, int guiHeight)
        {
            Origin = new Point(guiOriginX, guiOriginY);
            Window = new Rectangle(Origin.X, Origin.Y, guiWidth, guiHeight);
            BorderChar = '#';
            FillingChar = '%';
            BorderColor = ConsoleColor.DarkGreen;
            FillingColor = ConsoleColor.DarkGray;

        }

        public override void ClearGUI() {
            throw new NotImplementedException();
        }

        public override void DrawGUI() {
            Point firstCell = Origin;
            int x = firstCell.X;
            int y = firstCell.Y;

            for (int i = 0; i < Window.Width; i++)
            {
                for (int j = 0; j < Window.Height; j++)
                {
                    if(CheckIfDrawingBorder(i, j)) {
                        Util.Write(BorderChar, x + i, y + j,
                            BorderColor);
                    } else {
                        Util.Write(FillingChar, x + i, y + j,
                            FillingColor);
                    }
                }
            }
        }

        public override bool CheckIfDrawingBorder(int i, int j) {

            if (i == 0 || i == Window.Width-1) {

                return true;

            } else if (j == 0 || j == Window.Height-1) {

                return true;

            }

            return false;
        }

        public override void PrepareGUI()
        {
            throw new NotImplementedException();
        }
    }
}
