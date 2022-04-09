global using System;
global using System.Collections.Generic;

global using RogueWorld.Managers;
global using RogueWorld.GameObjects;
global using RogueWorld.GameObjects.Units;
global using RogueWorld.GameObjects.Items;
global using RogueWorld.GameObjects.Scenery;
global using RogueWorld.Utilities;

namespace RogueWorld
{

    internal class Program
    {

        public static GameState GameState;

        static void Main(string[] args) {
            Init();

            var GUI = GameManager.Instance.GUIManager;

            while (GameState != GameState.GameOver) {

                if(GameState == GameState.Menu) {

                    GUI.DrawMenu();
                    GameManager.Instance.TakeUserInput();

                } else if(GameState == GameState.Continue) {

                    GameManager.Instance.DrawAllScenery();
                    GameManager.Instance.DrawAllItems();
                    GameManager.Instance.DrawAllUnits();
                    GUI.statsGUI.DrawGUI();
                    GameManager.Instance.TakeUserInput();
                    GameManager.Instance.PlayPlayerTurn();
                    /*
                    if (T.FightResolution())
                    {
                        U.DrawUnits();
                    }
                    */
                    GameManager.Instance.PlayAITurn();
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
