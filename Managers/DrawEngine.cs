using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RogueWorld.GameObjects;
using RogueWorld.GameObjects.Scenery;
using RogueWorld.GameObjects.Units;

namespace RogueWorld.Managers
{
    internal class DrawEngine
    {

        /* Write-to-console helper function 
        Supports colors and cursor position. Use the reset bool
        to reset console colors after use. */
        public void Write(object text, 
        int x, int y, 
        ConsoleColor FgColor = ConsoleColor.White, ConsoleColor BgColor = ConsoleColor.Black, 
        bool reset = true)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = FgColor;
            Console.BackgroundColor = BgColor;
            Console.Write("{0}", text);
            if(reset)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }

        private int _currentButton = -1;
        readonly int ButtonMax = 2;
        readonly int xOffset = 10;

        public List<MenuButton> buttons;

        public struct MenuButton {
            public int Id;
            public string Label;
            public int yOffset;
        }
        public void InitMenu() {
            buttons = new List<MenuButton>();
        }
        public void DrawMenu()
        {
            Console.Clear();
            DrawLogo();
            DrawButtons(0, "PLAY");
            DrawButtons(1, "TROPHIES");
            DrawButtons(2, "CREDITS");
            if(_currentButton == -1) { _currentButton = 0; }
            DrawButtonSelector();

        }
        private void DrawLogo()
        {

            Write("ROGUE WORLD", 
                GameManager.COLS / 2 - xOffset, 5, 
                ConsoleColor.DarkRed, ConsoleColor.Black);
        }
        
        private void CreateButton(int buttonId, string buttonText, int buttonYOffset = 0) {
            MenuButton newButton = new MenuButton();
            newButton.Id = buttonId;
            newButton.Label = buttonText;
            newButton.yOffset = buttonYOffset;
            buttons.Add(newButton);

        }

        private void DrawButtons(int buttonId, string buttonText, int buttonYOffset = 0)
        {

            if (buttonId == _currentButton)
            {
                Write(buttonText,
                      GameManager.COLS / 2 - xOffset, GameManager.ROWS - 10 + buttonId + buttonYOffset,
                      ConsoleColor.Black, ConsoleColor.White);

            } else
            {
                Write(buttonText,
                    GameManager.COLS / 2 - xOffset, GameManager.ROWS - 10 + buttonId + buttonYOffset,
                    ConsoleColor.DarkGray, ConsoleColor.Black);
            }
        }

        private void DrawButtonSelector()
        {
            Write(">",
                GameManager.COLS / 2 - xOffset - 2, GameManager.ROWS - 10 + _currentButton,
                ConsoleColor.Red, ConsoleColor.Black);
        }

        public void IncrementButtonSelection()
        {
            if(_currentButton < ButtonMax)
            {
                _currentButton++;
            } else
            {
                _currentButton = 0;
            }
        }

        public void DecrementButtonSelection()
        {
            if (_currentButton > 0)
            {
                _currentButton--;
            }
            else
            {
                _currentButton = ButtonMax;
            }
        }

        public void ActivateButtonSelection()
        {
            if (_currentButton == 0)
            {
                Program.GameState = GameState.Continue;
                _currentButton = -1;
            }
        }

        // World state (in arrays)

        /// <summary>
        /// Two pair of private fields / public properties for story our scenery and units.
        /// </summary>
        /// 
        private Scenery[,] _sceneryMap;
        public Scenery[,] SceneryMap
        {
            get { return _sceneryMap; }
            set { _sceneryMap = value;
                _sceneryIsDrawn = false; }
        }

        private Unit[,] _unitMap;
        public Unit[,] UnitMap
        {
            get { return _unitMap; }
            set { _unitMap = value; }
        }

        /// <summary>
        /// Initializes two new 2-D arrays named SceneryMap and UnitMap.
        /// Fills the SceneryMap with Scenery_Ground objects.
        /// </summary>
        /// <returns></returns>
        internal void InitMap()
        {

            SceneryMap = new Scenery[GameManager.COLS, GameManager.ROWS];
            UnitMap = new Unit[GameManager.COLS, GameManager.ROWS];

            for (int x = 0; x < GameManager.COLS; x++)
            {
                for (int y = 0; y < GameManager.ROWS; y++)
                {
                    AddObject(SceneryMap, new Scenery_Ground(x, y));
                }
            }
        }

        internal int AddObject(GameObject[,] objectMap, GameObject gameObject)
        {

            objectMap[gameObject.PositionX, gameObject.PositionY] = gameObject;

            return 0;
        }

        internal int AddObject(GameObject[,] objectMap, GameObject gameObject, List<GameObject> objectList)
        {

            objectMap[gameObject.PositionX, gameObject.PositionY] = gameObject;
            objectList.Add(gameObject);

            return 0;
        }

        internal int EraseObject(GameObject[,] objectMap, GameObject gameObject)
        {

            objectMap[gameObject.PositionX, gameObject.PositionY] = null;
            return 0;
        }

        // Draw scenery -- NEW FOR CLASS: Now uses the Write helper method we made earlier.

        /// <summary>
        /// Boolean to prevent drawing the whole scenery when its unnecessary.
        /// Set to true when we use the regular DrawScenery method.
        /// Set to false when setting the SceneryMap property written a few lines above.
        /// </summary>
        private bool _sceneryIsDrawn;

        /// <summary>
        /// Method to draw the whole scenery.
        /// </summary>
        internal void DrawScenery()
        {
            if (!_sceneryIsDrawn)
            {
                for (int x = 0; x < GameManager.COLS; x++)
                {
                    for (int y = 0; y < GameManager.ROWS - 1; y++)
                    {
                        if (SceneryMap[x, y] != null)
                        {
                            Write(SceneryMap[x, y].Symbol,
                                x, y,
                                SceneryMap[x, y].FgColor, SceneryMap[x, y].BgColor);
                        }
                    }
                }

                _sceneryIsDrawn = true;
            }
        }

        /// <summary>
        /// Overloaded version of the DrawScenery method which simply updates one specific coordinate of the scenery.
        /// Can be used to draw individual scenery game objects.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        internal void DrawScenery(int x, int y)
        {


            if (SceneryMap[x, y] != null)
            {
                Write(SceneryMap[x, y].Symbol,
                    x, y,
                    SceneryMap[x, y].FgColor, SceneryMap[x, y].BgColor);
            }
        }

        // Draw units -- NEW FOR CLASS: Now uses the Write helper method we made earlier.

        internal void DrawUnits(int x, int y)
        {
            if (UnitMap[x, y] != null)
            {
                Write(UnitMap[x, y].Symbol,
                    x, y,
                    UnitMap[x, y].FgColor, UnitMap[x, y].BgColor);
            }
        }

        internal void DrawUnits()
        {
            for (int x = 0; x < GameManager.COLS; x++)
            {
                for (int y = 0; y < GameManager.ROWS - 1; y++)
                {
                    if (UnitMap[x, y] != null)
                    {
                        Write(UnitMap[x, y].Symbol,
                            x, y,
                            UnitMap[x, y].FgColor, UnitMap[x, y].BgColor);
                    }
                }
            }
        }
    }
}
