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

            while (GameState != GameState.GameOver) {

                if(GameState == GameState.Menu) {

                    GameManager.Instance.GUIManager.DrawMenu();
                    GameManager.Instance.TakeUserInput();

                } else if(GameState == GameState.Continue) {

                    GameManager.Instance.DrawAllScenery();
                    GameManager.Instance.DrawAllItems();
                    GameManager.Instance.DrawAllUnits();
                    GameManager.Instance.GUIManager.Update();              
                    GameManager.Instance.TakeUserInput();
                    GameManager.Instance.PlayPlayerTurn(); 
                    GameManager.Instance.PlayAITurn();
                }
            }
        }

        static void Init() {
            Console.CursorVisible = false;
            GameState = GameState.Menu;
            
            if (OperatingSystem.IsWindows())
            {
                Console.SetWindowSize(GameManager.WINDOW_W, GameManager.WINDOW_H);
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
        None,
        Up,
        Left,
        Down,
        Right
    }
}
