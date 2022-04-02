using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueWorld.Utilities
{
    internal static class Util
    {

        /// <summary>
        /// A helper function to beginning writing characters of a text
        /// at a specified col and row in a specified FgColor and BgColor;
        /// </summary>
        /// <param name="text"></param>
        /// <param name="col">Col of the console to begin writing at.</param>
        /// <param name="row">Row of the console to begin writing at.</param>
        /// <param name="FgColor"><i>Optional. Color to write the character with. White by default.</i></param>
        /// <param name="BgColor"><i>Optional. Color to print in the background of the character. Black by default.</i></param>
        /// <param name="reset"><i>Optional. Boolean to reset the console Fg and Bg colors after the function executes.</i></param>
        public static void Write(object text,
        int col, int row,
        ConsoleColor FgColor = ConsoleColor.White, ConsoleColor BgColor = ConsoleColor.Black,
        bool reset = true)
        {
            Console.SetCursorPosition(col, row);
            Console.ForegroundColor = FgColor;
            Console.BackgroundColor = BgColor;
            Console.Write("{0}", text);
            if (reset)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }

    }
}
