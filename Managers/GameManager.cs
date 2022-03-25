using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RogueWorld.GameObjects;
using RogueWorld.GameObjects.Scenery;
using RogueWorld.GameObjects.Units;

namespace RogueWorld.Managers
{
    internal class GameManager
    {

        private static readonly Lazy<GameManager> lazy =
            new Lazy<GameManager>(() => new GameManager());

        public static GameManager Instance { get { return lazy.Value; } }

        public const int COLS = 90;
        public const int ROWS = 35;
       
        public UnitEngine UnitEngine;
        public DrawEngine DrawEngine;
        public TurnEngine TurnEngine;
        public LogsEngine LogsEngine;

        public List<GameObject> GameObjects;

        public static ConsoleKey LastKey;

        private GameManager()
        {
            GameObjects = new List<GameObject>();

            UnitEngine = new UnitEngine();
            DrawEngine = new DrawEngine();
            TurnEngine = new TurnEngine();
            LogsEngine = new LogsEngine();

            DrawEngine.InitMap();

            DrawEngine.AddObject(DrawEngine.UnitMap, UnitEngine.Rogue);
            DrawEngine.AddObject(DrawEngine.UnitMap, UnitEngine.Kobold, GameObjects);
            DrawEngine.AddObject(DrawEngine.SceneryMap, new Scenery_Boulder(10, 20));
            DrawEngine.AddObject(DrawEngine.SceneryMap, new Scenery_Boulder(17, 21));
            DrawEngine.AddObject(DrawEngine.SceneryMap, new Scenery_Boulder(20, 15));
        }

        DateTime lastPressedTime = DateTime.MinValue;

        public void TakeUserInput()
        {
            LastKey = Console.ReadKey(true).Key;

            if (DateTime.Now > lastPressedTime.AddSeconds(.01))
            {
                if (Program.GameState == GameState.Menu)
                {

                    if (LastKey == ConsoleKey.UpArrow)
                    {
                        GameManager.Instance.DrawEngine.DecrementButtonSelection();
                    }
                    else if (LastKey == ConsoleKey.DownArrow)
                    {
                        GameManager.Instance.DrawEngine.IncrementButtonSelection();
                    }
                    else if (LastKey == ConsoleKey.Spacebar)
                    {
                        GameManager.Instance.DrawEngine.ActivateButtonSelection();
                    }

                }
                else if (Program.GameState == GameState.Continue)
                {
                    if (LastKey == ConsoleKey.UpArrow)
                    {
                        UnitEngine.Rogue.Direction = Directions.Up;
                    }
                    else if (LastKey == ConsoleKey.LeftArrow)
                    {
                        UnitEngine.Rogue.Direction = Directions.Left;
                    }
                    else if (LastKey == ConsoleKey.DownArrow)
                    {
                        UnitEngine.Rogue.Direction = Directions.Down;
                    }
                    else if (LastKey == ConsoleKey.RightArrow)
                    {
                        UnitEngine.Rogue.Direction = Directions.Right;
                    }
                }

                lastPressedTime = DateTime.Now;
            }
        }
    }
}
