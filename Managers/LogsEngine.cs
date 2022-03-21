using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueWorld.Managers
{
    internal class LogsEngine
    {
        public void PrintLog(string logContent)
        {
            Console.SetCursorPosition(0, GameManager.ROWS - 1);
            Console.Write("Log: " + logContent);
        }
    }
}
