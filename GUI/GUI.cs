using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RogueWorld.GUI
{
    /// <summary>
    /// This is the contract that all our GUI classes need to implement.
    /// It defines a basic window and origin and a set of methods which all GUI can implement.
    /// </summary>
    internal abstract class GUI
    {

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

        public abstract void ClearGUI();
        public abstract void DrawGUI();
        public abstract bool CheckIfDrawingBorder(int i, int j);
        public abstract void PrepareGUI();
    }
}