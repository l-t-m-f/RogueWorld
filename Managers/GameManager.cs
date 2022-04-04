using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RogueWorld.Utilities;
using RogueWorld.GameObjects;
using RogueWorld.GameObjects.Scenery;
using RogueWorld.GameObjects.Units;
using RogueWorld.GameObjects.Items;
using System.Drawing;

namespace RogueWorld.Managers
{
    internal class GameManager
    {
        // Makes this class into a singleton
        private static readonly Lazy<GameManager> lazy =
            new Lazy<GameManager>(() => new GameManager());

        public static GameManager Instance { get { return lazy.Value; } }

        // Window size
        public const int COLS = 90;
        public const int ROWS = 35;

        // Our various managers, which will be instantiated during the initialization
        public UnitManager UnitManager;
        public TurnManager TurnManager;
        public GUIManager GUIManager;
        // public DrawEngine DrawEngine; This manager has been integrated into the others
        // public LogsEngine LogsEngine; Moved to GUIManager


        /// <summary>
        /// CONSTRUCTOR
        /// Initializes the game manager. Because the manager is meant to be a singleton,
        /// the constructor in this exceptional case if PRIVATE. Usually, a constructor is
        /// public.
        /// </summary>
        private GameManager() {

            UnitManager = new UnitManager();
            TurnManager = new TurnManager();
            GUIManager = new GUIManager();
            //DrawEngine = new DrawEngine();
            //LogsEngine = new LogsEngine();

            var U = UnitManager;
            var T = TurnManager;

            InitMap();

            AddObject(UnitMap, U.Rogue);
            AddObject(UnitMap, U.Kobold);
            T.ListTurnUnit(U.Kobold);
            AddObject(SceneryMap, new Scenery_Boulder(10, 20));
            AddObject(SceneryMap, new Scenery_Boulder(17, 21));
            AddObject(SceneryMap, new Scenery_Boulder(20, 15));
        }

        #region World state

        public Scenery[,] SceneryMap;
        public Unit[,] UnitMap;
        public Item[,] ItemMap;

        /// <summary>
        /// Initializes three 2-D arrays for holding Units, Scenery and Items respecively.
        /// Fills the SceneryMap with Scenery_Ground objects.
        /// </summary>
        /// <returns></returns>
        internal void InitMap()
        {

            SceneryMap = new Scenery[GameManager.COLS, GameManager.ROWS];
            UnitMap = new Unit[GameManager.COLS, GameManager.ROWS];
            ItemMap = new Item[GameManager.COLS, GameManager.ROWS];

            for (int x = 0; x < GameManager.COLS; x++)
            {
                for (int y = 0; y < GameManager.ROWS; y++)
                {
                    var newScenery = new Scenery_Ground(x, y);
                    AddObject(SceneryMap, newScenery);
                }
            }
        }

        /// <summary>
        /// Places a gameObject inside the object map at specific coordinates.
        /// Because gameObject is a parent of Unit, Scenery and Item, this will work with
        /// any of our GameObject arrays.
        /// </summary>
        /// <param name="objectMap">GameObject array to place the gameObject into.</param>
        /// <param name="gameObject">GameObject to place into the array.</param>
        /// <returns></returns>
        internal int AddObject(GameObject[,] objectMap, GameObject gameObject)
        {

            objectMap[gameObject.PositionX, gameObject.PositionY] = gameObject;

            return 0;
        }

        /// <summary>
        /// Erases a gameObject inside a specific array of the same type.
        /// Because gameObject is a parent of Unit, Scenery and Item, this will work with
        /// any of our GameObject arrays.
        /// </summary>
        /// <param name="objectMap">GameObject array to remove the gameObject from.</param>
        /// <param name="gameObject">GameObject to remove from the array.</param>
        /// <returns></returns>
        internal int EraseObject(GameObject[,] objectMap, GameObject gameObject)
        {

            objectMap[gameObject.PositionX, gameObject.PositionY] = null;
            return 0;
        }

        /// <summary>
        /// Boolean to prevent drawing the whole scenery when its unnecessary.
        /// Set to true when we use the regular DrawScenery method.
        /// Set to false when setting the SceneryMap property written a few lines above.
        /// </summary>
        private bool _staticSceneIsDrawn;

        /// <summary>
        /// Method to draw the whole scenery.
        /// </summary>
        internal void DrawStaticObjects()
        {
            if (!_staticSceneIsDrawn)
            {
                for (int x = 0; x < COLS; x++)
                {
                    for (int y = 0; y < ROWS - 1; y++)
                    {
                        if (SceneryMap[x, y] != null)
                        {
                            Util.Write(SceneryMap[x, y].Symbol,
                                x, y,
                                SceneryMap[x, y].FgColor, SceneryMap[x, y].BgColor);
                        }
                    }
                }

                _staticSceneIsDrawn = true;
            }
        }

        /// <summary>
        /// Overloaded version of the DrawScenery method which simply updates one specific coordinate of the scenery.
        /// Can be used to draw individual scenery game objects.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        internal void DrawStaticObject(int x, int y)
        {


            if (SceneryMap[x, y] != null)
            {
                Util.Write(SceneryMap[x, y].Symbol,
                    x, y,
                    SceneryMap[x, y].FgColor, SceneryMap[x, y].BgColor);
            }
        }

        #endregion

    }
}
