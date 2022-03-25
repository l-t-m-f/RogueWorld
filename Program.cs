using System;
using RogueWorld.Managers;

namespace RogueWorld
{
    internal class Program
    {

        public static GameState GameState;

        static void Main(string[] args)
        {
            Init();

            while (GameState != GameState.GameOver)
            {
                if(GameState == GameState.Menu)
                {
                    GameManager.Instance.DrawEngine.DrawMenu();
                    GameManager.Instance.TakeUserInput();
                }
                else if(GameState == GameState.Continue)
                {
                    GameManager.Instance.DrawEngine.DrawScenery();
                    GameManager.Instance.DrawEngine.DrawUnits();
                    GameManager.Instance.TakeUserInput();
                    GameManager.Instance.TurnEngine.PlayPlayerTurn();
                    /*
                    if (GameManager.Instance.TurnEngine.FightResolution())
                    {
                        GameManager.Instance.DrawEngine.DrawUnits();
                    }
                    */
                    GameManager.Instance.TurnEngine.PlayAITurn();
                }
            }
        }

        static void Init()
        {
            Console.CursorVisible = false;
            GameState = GameState.Menu;
            
            if (OperatingSystem.IsWindows())
            {
                Console.SetWindowSize(GameManager.COLS, GameManager.ROWS);
                Console.WriteLine("Set window {0}, {1}", GameManager.COLS, GameManager.ROWS);
            }

            //GameManager.Instance.LogsEngine.PrintLog(" ");
        }
    }

    public enum GameState
    {
        Menu,
        Continue,
        GameOver
    }

    public enum Directions
    {
        Up,
        Left,
        Down,
        Right
    }
}
