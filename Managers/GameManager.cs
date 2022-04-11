namespace RogueWorld.Managers {
    internal class GameManager
    {
        // Makes this class into a singleton
        private static readonly Lazy<GameManager> lazy =
            new Lazy<GameManager>(() => new GameManager());

        public static GameManager Instance { get { return lazy.Value; } }

        public const int WINDOW_W = 108;
        public const int WINDOW_H = 35;
        public const int COLS = 90;
        public const int ROWS = 35;
        
        public GUIManager GUIManager;

        public Unit_Rogue Rogue;
        public Unit_Kobold Kobold;
        public Item_FireSword FireSword;


        DateTime lastPressedTime = DateTime.MinValue;

        public static ConsoleKey LastKey;

        public List<GameObject> TurnUnits;

        private GameManager() {

            Rogue = new Unit_Rogue(40, 20, 
                5, 10, 
                2, 4, 
                2, 4, 
                2, 4);

            Kobold = new Unit_Kobold(50, 30,
                2, 4,
                1, 2,
                1, 3,
                0, 0);

            FireSword = new Item_FireSword(5, 5);

            TurnUnits = new List<GameObject>();
            
            GUIManager = new GUIManager(Rogue);

            InitMap();

            AddObject(UnitMap, Rogue);
            AddObject(UnitMap, Kobold);
            ListTurnUnit(Kobold);
            AddObject(SceneryMap, new Scenery_Boulder(10, 20));
            AddObject(SceneryMap, new Scenery_Boulder(17, 21));
            AddObject(SceneryMap, new Scenery_Boulder(20, 15));
            AddObject(ItemMap, FireSword);
        }

        #region World state

        public Scenery[,] SceneryMap;
        public Unit[,] UnitMap;
        public Item[,] ItemMap;

        private bool _staticSceneIsDrawn;

        internal void InitMap()
        {

            SceneryMap = new Scenery[COLS, ROWS];
            UnitMap = new Unit[COLS, ROWS];
            ItemMap = new Item[COLS, ROWS];

            for (int x = 0; x < COLS; x++)
            {
                for (int y = 0; y < ROWS; y++)
                {
                    var newScenery = new Scenery_Ground(x, y);
                    AddObject(SceneryMap, newScenery);
                }
            }
        }

        internal int AddObject(GameObject[,] objectMap, GameObject gameObject) {

            objectMap[gameObject.PositionX, gameObject.PositionY] = gameObject;
            return 0;
        }

        internal int EraseObject(GameObject[,] objectMap, GameObject gameObject) {

            objectMap[gameObject.PositionX, gameObject.PositionY] = null;
            return 0;
        }

        internal void DrawAllScenery()
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

        internal void Draw(int x, int y)
        {
            if (UnitMap[x, y] != null) {
                Util.Write(UnitMap[x, y].Symbol, x, y,
                    UnitMap[x, y].FgColor, UnitMap[x, y].BgColor);
            }
            else if (ItemMap[x, y] != null) {
                Util.Write(ItemMap[x, y].Symbol, x, y,
                    ItemMap[x, y].FgColor, ItemMap[x, y].BgColor);
            }
            else if (SceneryMap[x, y] != null) {
                Util.Write(SceneryMap[x, y].Symbol, x, y,
                    SceneryMap[x, y].FgColor, SceneryMap[x, y].BgColor);
            }
        }

        #endregion

        #region Unit Rendering

        internal void DrawAllUnits() {
            for (int x = 0; x < COLS; x++) {
                for (int y = 0; y < ROWS - 1; y++) {
                    if (UnitMap[x, y] != null) {
                        Util.Write(UnitMap[x, y].Symbol, x, y,
                            UnitMap[x, y].FgColor, UnitMap[x, y].BgColor);
                    }
                }
            }
        }

        internal void DrawAllItems() {
            for (int x = 0; x < COLS; x++) {
                for (int y = 0; y < ROWS - 1; y++) {
                    if (ItemMap[x, y] != null) {
                        Util.Write(ItemMap[x, y].Symbol, x, y,
                            ItemMap[x, y].FgColor, ItemMap[x, y].BgColor);
                    }
                }
            }
        }

        internal bool CheckCellForUnit(int x, int y) {

            if (UnitMap[x, y] == null) {

                return false;

            }

            return true;
        }

        internal bool CheckIfClearIsCell(int x, int y) {

            if (SceneryMap[x, y].Solidity == false) {
                return true;
            } else {
                return false;
            }
        }


        #endregion

        #region Turn management


        internal int ListTurnUnit(Unit unit) {

            TurnUnits.Add(unit);
            return 0;
        }

        public void TakeUserInput() {

            Rogue.Direction = Directions.None;

            Unit_Rogue player = Rogue;
            LastKey = Console.ReadKey(true).Key;

            if (DateTime.Now > lastPressedTime.AddSeconds(.01)) {
                if (Program.GameState == GameState.Menu) {

                    if (LastKey == ConsoleKey.UpArrow) {
                        GUIManager.DecrementButtonSelection();
                    } else if (LastKey == ConsoleKey.DownArrow) {
                        GUIManager.IncrementButtonSelection();
                    } else if (LastKey == ConsoleKey.Spacebar) {
                        GUIManager.ActivateButtonSelection();
                    }

                } else if (Program.GameState == GameState.Continue) {
                    if (LastKey == ConsoleKey.UpArrow) {
                        player.Direction = Directions.Up;
                    } else if (LastKey == ConsoleKey.LeftArrow) {
                        player.Direction = Directions.Left;
                    } else if (LastKey == ConsoleKey.DownArrow) {
                        player.Direction = Directions.Down;
                    } else if (LastKey == ConsoleKey.RightArrow) {
                        player.Direction = Directions.Right;
                    }
                }

                lastPressedTime = DateTime.Now;
            }
        }

        internal void PlayPlayerTurn() {

            switch (Rogue.Direction) {

                case Directions.Up:
                    Rogue.ActAtOffset(0, -1);
                    break;
                case Directions.Left:
                    Rogue.ActAtOffset(-1, 0);
                    break;
                case Directions.Down:
                    Rogue.ActAtOffset(0, 1);
                    break;
                case Directions.Right:
                    Rogue.ActAtOffset(1, 0);
                    break;
                default:
                    break;

            }

            Rogue.UpdateColorBasedOnHealth();
        }

        internal void PlayAITurn() {

            foreach (Unit unit in TurnUnits) {


                if(Rogue.PositionX == unit.PositionX &&
                    Rogue.PositionY == unit.PositionY - 1)
                {
                    unit.TryAttack(unit.PositionX, unit.PositionY - 1);
                } 
                else if (Rogue.PositionX == unit.PositionX - 1 &&
                    Rogue.PositionY == unit.PositionY)
                {
                    unit.TryAttack(unit.PositionX - 1, unit.PositionY);
                } 
                else if (Rogue.PositionX == unit.PositionX && 
                    Rogue.PositionY == unit.PositionY + 1)
                {
                    unit.TryAttack(unit.PositionX, unit.PositionY + 1);
                } 
                else if (Rogue.PositionX == unit.PositionX + 1 &&
                    Rogue.PositionY == unit.PositionY)
                {
                    unit.TryAttack(unit.PositionX + 1, unit.PositionY);
                }

                else if (unit.CalculateLOS(Rogue) < 8)
                {
                    if (unit.PositionY > Rogue.PositionY)
                    {
                        unit.ActAtOffset(0, -1);
                    }
                    else if (unit.PositionX > Rogue.PositionX)
                    {
                        unit.ActAtOffset(-1, 0);
                    } 
                    else if (unit.PositionY < Rogue.PositionY)
                    {
                        unit.ActAtOffset(0, 1);
                    } 
                    else if (unit.PositionX < Rogue.PositionX)
                    {
                        unit.ActAtOffset(1, 0);
                    }
                }
                else
                {

                    var x = 0;
                    var y = 0;

                    Random random = new Random();

                    if (random.Next(2) == 0)
                    {
                        random = new Random();
                        x = random.Next(-1, 2);
                    } else
                    {
                        random = new Random();
                        y = random.Next(-1, 2);
                    }

                    unit.ActAtOffset(x, y);
                }
            }
        }

        

        #endregion

    }
}
