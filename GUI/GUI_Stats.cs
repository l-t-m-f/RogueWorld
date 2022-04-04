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
<<<<<<< HEAD
    internal class GUI_Stats : GUI
=======
    internal class GUI_Stats : IGUI
>>>>>>> c69185d1dafffea79cb5321aae18ff5a368b0f9b
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

<<<<<<< HEAD
        public override void ClearGUI() {
            throw new NotImplementedException();
        }

        public override void DrawGUI() {
=======
        // Position and size
        private Point _origin;
        public Point Origin {
            get { return _origin; } 
            set { _origin = value; }
        }
        private Rectangle _window;
        public Rectangle Window {
            get { return _window; }
            set { _window = value; }

        }

        // Apperance
        private char _borderChar;
        public char BorderChar {
            get { return _borderChar; }
            set { _borderChar = value; }
        }
        private char _fillingChar;
        public char FillingChar {
            get { return _fillingChar; }
            set { _fillingChar = value; }
        }
        
        // Color
        private ConsoleColor _borderColor;
        public ConsoleColor BorderColor {
            get { return _borderColor; }
            set { _borderColor = value; }
        }
        private ConsoleColor _fillingColor;
        public ConsoleColor FillingColor {
            get { return _fillingColor; }
            set { _fillingColor = value; }
        }

        public void ClearGUI() {
            throw new NotImplementedException();
        }

        public void DrawGUI() {
>>>>>>> c69185d1dafffea79cb5321aae18ff5a368b0f9b
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

<<<<<<< HEAD
        public override bool CheckIfDrawingBorder(int i, int j) {
=======
        public bool CheckIfDrawingBorder(int i, int j) {
>>>>>>> c69185d1dafffea79cb5321aae18ff5a368b0f9b

            if (i == 0 || i == Window.Width-1) {

                return true;

            } else if (j == 0 || j == Window.Height-1) {

                return true;

            }

            return false;
        }

<<<<<<< HEAD
        public override void PrepareGUI()
=======
        public void PrepareGUI()
>>>>>>> c69185d1dafffea79cb5321aae18ff5a368b0f9b
        {
            throw new NotImplementedException();
        }
    }
}
