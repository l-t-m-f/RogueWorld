using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueWorld.Managers
{
    internal class LogsEngine
    {
        public void PrintLog(string logContent, ConsoleColor FgColor, ConsoleColor BgColog)
        {
            ClearLog();
            GameManager.Instance.DrawEngine.Write("Log: " + logContent,
                0, GameManager.ROWS - 1,
                FgColor, BgColog);
        }

        public void ClearLog()
        {
            for(var i = 0; i < GameManager.COLS; i++)
            {
                GameManager.Instance.DrawEngine.Write(" ",
               i, GameManager.ROWS - 1,
               ConsoleColor.White, ConsoleColor.Black);
            }
        }
    }
}
