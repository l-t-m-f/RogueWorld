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
        /// <summary>
        /// Constructor for GUI_Stats. What to do when
        /// creating this GUI.
        /// </summary>
        /// <param name="guiOriginX">X-origin of the window.</param>
        /// <param name="guiOriginY">Y-origin of the window</param>
        /// <param name="guiWidth">Window's width</param>
        /// <param name="guiHeight">Window's height</param>
        /// <param name="borderChar">Character to draw the border of the window with.</param>
        /// <param name="fillingChar">Character to the inside of the border of the window with.</param>
        /// <param name="borderColor">Color to draw the border with.</param>
        /// <param name="fillingColor">Color to draw the inside with.</param>
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

        public override void ClearGUI() {
            throw new NotImplementedException();
        }

        public override void DrawGUI() {

            Point firstCell = Origin;

            int x = firstCell.X;
            int y = firstCell.Y;

            int currentTitleChar = 0;

            for (int i = 0; i < Window.Width; i++)
            {
                for (int j = 0; j < Window.Height; j++)
                {
                    if(CheckIfDrawingBorder(i, j)) {

                        if(CheckIfDrawingTitle(i, j)) {

                            Util.Write(Title.Substring(currentTitleChar), x + i, y + j, ConsoleColor.Red);
                            currentTitleChar += 1;

                        } else {

                            Util.Write(BorderChar, x + i, y + j,
                                BorderColor);
                        }
                    } else {
                        Util.Write(FillingChar, x + i, y + j,
                            FillingColor);
                    }
                }
            }   
        }

        /// <summary>
        /// Method to check if the current loop i, j is a border.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public override bool CheckIfDrawingBorder(int i, int j) {

            if (i == 0 || i == Window.Width-1) {

                return true;

            } else if (j == 0 || j == Window.Height-1) {

                return true;

            }

            return false;
        }

        /// <summary>
        /// Method to check if current loop i, j is window title.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public bool CheckIfDrawingTitle(int i, int j) {

            double titleLength = Title.Length;

            if(j == 0 &&
               i >= (Window.Width / 2) - Math.Round(titleLength / 2) &&
               i <= (Window.Width / 2) + Math.Round(titleLength / 2)
               ) {
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
