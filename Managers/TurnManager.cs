using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RogueWorld.GameObjects;
using RogueWorld.GameObjects.Units;

namespace RogueWorld.Managers
{

    internal class TurnManager
    {

        DateTime lastPressedTime = DateTime.MinValue;

        public static ConsoleKey LastKey;

        // List of our game units
        public List<GameObject> TurnUnits;

        public TurnManager()
        {
            TurnUnits = new List<GameObject>();
        }

        internal int ListTurnUnit(GameObject gameObject)
        {

            TurnUnits.Add(gameObject);

            return 0;
        }

        public void TakeUserInput()
        {

            var U = GameManager.Instance.UnitManager;
            var GUI = GameManager.Instance.GUIManager;

            LastKey = Console.ReadKey(true).Key;

            if (DateTime.Now > lastPressedTime.AddSeconds(.01))
            {
                if (Program.GameState == GameState.Menu)
                {

                    if (LastKey == ConsoleKey.UpArrow)
                    {
                        GUI.DecrementButtonSelection();
                    }
                    else if (LastKey == ConsoleKey.DownArrow)
                    {
                        GUI.IncrementButtonSelection();
                    }
                    else if (LastKey == ConsoleKey.Spacebar)
                    {
                        GUI.ActivateButtonSelection();
                    }

                }
                else if (Program.GameState == GameState.Continue)
                {
                    if (LastKey == ConsoleKey.UpArrow)
                    {
                        U.Rogue.Direction = Directions.Up;
                    }
                    else if (LastKey == ConsoleKey.LeftArrow)
                    {
                        U.Rogue.Direction = Directions.Left;
                    }
                    else if (LastKey == ConsoleKey.DownArrow)
                    {
                        U.Rogue.Direction = Directions.Down;
                    }
                    else if (LastKey == ConsoleKey.RightArrow)
                    {
                        U.Rogue.Direction = Directions.Right;
                    }
                }

                lastPressedTime = DateTime.Now;
            }
        }

        internal void PlayPlayerTurn()
        {
            var U = GameManager.Instance.UnitManager;

            switch (U.Rogue.Direction) {
                case Directions.Up:
                    U.MoveUnitBy(U.Rogue, 0, -1);
                    break;
                case Directions.Left:
                    U.MoveUnitBy(U.Rogue, -1, 0);
                    break;
                case Directions.Down:
                    U.MoveUnitBy(U.Rogue, 0, 1);
                    break;
                case Directions.Right:
                    U.MoveUnitBy(U.Rogue, 1, 0);
                    break;
                default:
                    break;
            }
        }

        internal bool FightResolution()
        {

            var update = false;

            foreach (Unit unit in TurnUnits)
            {

                if (unit.Stats.Health <= 0)
                {
                    GameManager.Instance.EraseObject(GameManager.Instance.UnitMap,
                    unit);
                    //GameManager.Instance.UnitEngine.KillUnit(unit);
                    update = true;
                }
            }

            return update;
        }

        internal void PlayAITurn()
        {

            foreach (Unit unit in TurnUnits)
            {
                    var x = 0;
                    var y = 0;

                    Random random = new Random();
                    if(random.Next(2) == 0) {
                        random = new Random();
                        x = random.Next(-1, 2);
                    } else {
                        random = new Random();
                        y = random.Next(-1, 2);
                    }
                    
                    GameManager.Instance.UnitManager.MoveUnitBy(unit, x, y);

            }
        }
    }
}
