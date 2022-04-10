using System.Drawing;

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


        public void DrawGUI()
        {

            Point firstCell = Origin;

            int x = firstCell.X;
            int y = firstCell.Y;

            int currentTitleChar = 0;

            for (int i = 0; i < Window.Width; i++)
            {
                for (int j = 0; j < Window.Height; j++)
                {
                    if (CheckIfDrawingBorder(i, j))
                    {

                        if (CheckIfDrawingTitle(i, j))
                        {

                            Util.Write(Title.Substring(currentTitleChar), x + i, y + j, ConsoleColor.Red);
                            currentTitleChar += 1;

                        } else
                        {

                            Util.Write(BorderChar, x + i, y + j,
                                BorderColor);
                        }
                    } else
                    {
                        Util.Write(FillingChar, x + i, y + j,
                            FillingColor);
                    }
                }
            }

            DrawContent();

        }
        public bool CheckIfDrawingBorder(int i, int j)
        {

            if (i == 0 || i == Window.Width - 1)
            {

                return true;

            } else if (j == 0 || j == Window.Height - 1)
            {

                return true;

            }

            return false;
        }

        public bool CheckIfDrawingTitle(int i, int j)
        {

            double titleLength = Title.Length;

            if (j == 0 &&
               i >= (Window.Width / 2) - Math.Round(titleLength / 2) &&
               i <= (Window.Width / 2) + Math.Round(titleLength / 2)
               )
            {
                return true;
            }

            return false;
        }

        public abstract void DrawContent();

    }
}