using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Rogue.GameObjects;
using Rogue.GameObjects.Scenery;
using Rogue.GameObjects.Units;

namespace Rogue.Managers
{
    public sealed class GameManager
    {
        // Singleton

        private static readonly Lazy<GameManager> lazy = 
            new Lazy<GameManager>(() => new GameManager());

        public static GameManager Instance { get { return lazy.Value; } }

        //

        public const int COLS = 160;
        public const int ROWS = 45;

        private Scenery[,] _sceneryMap;
        private Unit[,] _unitMap;

        public UnitManager UnitManager;

        private GameManager()
        {
            InitMap();
            UnitManager = new UnitManager();
            // Put the Rogue player in the map (it's created as part of new UnitManager(); ) 
            UpdateUnitMap(UnitManager.Rogue);
        }

        private int InitMap()
        {

            _sceneryMap = new Scenery[COLS, ROWS];
            _unitMap = new Unit[COLS, ROWS];

            for (int x = 0; x < COLS; x++)
            {
                for (int y = 0; y < ROWS; y++)
                {
                    UpdateSceneryMap(new Scenery_Ground(x, y));
                }
            }

            return 0;
        }

        public int UpdateSceneryMap(Scenery scenery)
        {

            _sceneryMap[scenery.PositionX, scenery.PositionY] = scenery;

            return 0;
        }

        public int UpdateUnitMap(Unit unit)
        {

            _unitMap[unit.PositionX, unit.PositionY] = unit;

            return 0;
        }

        public int ClearUnitMap(Unit unit)
        {
            _unitMap[unit.PositionX, unit.PositionY] = null;

            return 0;
        }

        public int DrawMap()
        {
            for (int x = 0; x < COLS; x++)
            {
                for (int y = 0; y < ROWS-1; y++)
                {
                    Console.SetCursorPosition(x, y);

                    if (_unitMap[x, y] != null)
                    {
                        Console.Write(_unitMap[x, y].Symbol);
                    }
                    else if (_sceneryMap[x, y] != null)
                    {
                        Console.Write(_sceneryMap[x, y].Symbol);
                    }
                    
                }
            }

            return 0;
        }

        DateTime lastPressedTime = DateTime.MinValue;
        public static ConsoleKey LastKey;

        public void TakeUserInput()
        {
            LastKey = Console.ReadKey(true).Key;

            if (DateTime.Now > lastPressedTime.AddSeconds(.1))
            {
                
                if (LastKey == ConsoleKey.UpArrow)
                {
                    UnitManager.MoveUnitBy(UnitManager.Rogue, 0, -1);
                    PrintLog("Player moved to " + UnitManager.Rogue.PositionX + ", " + UnitManager.Rogue.PositionY);
                }
                else if (LastKey == ConsoleKey.LeftArrow)
                {
                    UnitManager.MoveUnitBy(UnitManager.Rogue, -1, 0);
                    PrintLog("Player moved to " + UnitManager.Rogue.PositionX + ", " + UnitManager.Rogue.PositionY);
                }
                else if (LastKey == ConsoleKey.DownArrow)
                {
                    UnitManager.MoveUnitBy(UnitManager.Rogue, 0, 1);
                    PrintLog("Player moved to " + UnitManager.Rogue.PositionX + ", " + UnitManager.Rogue.PositionY);
                }
                else if (LastKey == ConsoleKey.RightArrow)
                {
                    UnitManager.MoveUnitBy(UnitManager.Rogue, 1, 0);
                    PrintLog("Player moved to " + UnitManager.Rogue.PositionX + ", " + UnitManager.Rogue.PositionY);
                }

                lastPressedTime = DateTime.Now;
            }
        }


        public void PrintLog(string logContent)
        {
            Console.SetCursorPosition(0, ROWS - 1);
            Console.Write("Log: " + logContent);
        }
    }

    public enum GameState
    {
        Menu,
        Continue,
        GameOver
    }
}
