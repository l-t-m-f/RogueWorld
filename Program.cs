using System;
using Rogue.Managers;

namespace Rogue
{
    internal class Program
    {
        private static GameState _gameState;

        static void Main()
        {
           Init();
           while (_gameState == GameState.Continue)
            {
                GameManager.Instance.DrawMap();
                GameManager.Instance.TakeUserInput();
            }
        }

        static void Init()
        {
            Console.CursorVisible = false;
            _gameState = GameState.Continue;
            
            if (OperatingSystem.IsWindows())
            {
                Console.SetWindowSize(GameManager.COLS, GameManager.ROWS);
                //Console.WriteLine("Set window {0}, {1}", GameManager.COLS, GameManager.ROWS);
            }

            GameManager.Instance.PrintLog(" ");
        }
    } 
}
