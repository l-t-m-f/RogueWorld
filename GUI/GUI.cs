using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RogueWorld.GUI
{
    internal abstract class GUI
    {
        // Window title
        public string Title { get; set; }

        // Position and size
        public Point Origin { get; set; }
        public Rectangle Window { get; set; }

        // Appearance
        public char BorderChar { get; set; }
        public char FillingChar { get; set; }

        // Color
        public ConsoleColor BorderColor { get; set; }
        public ConsoleColor FillingColor { get; set; }

        public abstract void ClearGUI();
        public abstract void DrawGUI();
        public abstract bool CheckIfDrawingBorder(int i, int j);
        public abstract void PrepareGUI();
    }
}