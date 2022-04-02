using System;
using RogueWorld.Managers;

namespace RogueWorld {
    internal class Program {

        public static GameState GameState;

        static void Main(string[] args) {
            Init();

            var GUI = GameManager.Instance.GUIManager;
            var U = GameManager.Instance.UnitManager;
            var T = GameManager.Instance.TurnManager;

            while (GameState != GameState.GameOver) {

                if(GameState == GameState.Menu) {

                    GUI.DrawMenu();
                    T.TakeUserInput();

                } else if(GameState == GameState.Continue) {

                    GameManager.Instance.DrawStaticObjects();
                    U.DrawUnits();
                    GUI.statsGUI.DrawGUI();
                    T.TakeUserInput();
                    T.PlayPlayerTurn();
                    /*
                    if (T.FightResolution())
                    {
                        U.DrawUnits();
                    }
                    */ 
                    T.PlayAITurn();
                }
            }
        }

        static void Init() {
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
