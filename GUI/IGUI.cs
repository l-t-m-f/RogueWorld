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
    interface IGUI
    {
        public char BorderChar { get; set; }
        public char FillingChar { get; set; }
        public ConsoleColor BorderColor { get; set; }
        public ConsoleColor FillingColor { get; set; }

        public Point Origin { get; set; }
        public Rectangle Window { get; set; }
        public void ClearGUI();
        public void DrawGUI();
        public bool CheckIfDrawingBorder(int i, int j);
        public void PrepareGUI();
    }
}