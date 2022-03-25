using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RogueWorld.GameObjects;
using RogueWorld.GameObjects.Units;

namespace RogueWorld.Managers
{
    internal class UnitEngine
    {

        public Unit_Rogue Rogue;
        public Unit_Kobold Kobold;

        public UnitEngine()
        {
            Rogue = new Unit_Rogue(40, 20);
            Kobold = new Unit_Kobold(50, 30);
        }

        /// <summary>
        /// Adds a  object to a specified objectMap (SceneryMap/UnitMap).
        /// </summary>
        /// <param name="objectMap">Map to update.</param>
        /// <param name="gameObject">New GameObject to be placed. 
        /// The object should already have been initialized elsewhere with coordinates.</param>
        /// <returns></returns>
        internal void MoveUnitBy(Unit unit, int x, int y) {

            int newX = unit.PositionX + x;
            int newY = unit.PositionY + y;

            // Make sur the new position is not "out-of-bounds":
            if (newX < GameManager.COLS-1 && newX > 1 && 
                newY < GameManager.ROWS-1 && newY > 1) {

                // If new position already contains a unit.
                if (CheckCellForUnit(newX, newY) == true) {

                    // Verify if the unit is of a different "faction".
                    // If so, it is an enemy. Therefore, attack it.
                    if (GameManager.Instance.DrawEngine.UnitMap[newX, newY].Faction != unit.Faction) {

                        GameManager.Instance.LogsEngine.PrintLog("Player attacked to "
                            + newX + ", " + newY + "(" +
                            GameManager.Instance.DrawEngine.UnitMap[newX, newY].Health + "/ 3)",
                            ConsoleColor.Red, ConsoleColor.Black);
                        DealDamageToUnit(GameManager.Instance.DrawEngine.UnitMap[newX, newY]);
                    }
                // If the new position doesn't contain a unit, check if it contains a solid scenery.
                // If its clear, allow the movement.
                } else if(CheckIfClearIsCell(newX, newY) == true) {

                    GameManager.Instance.DrawEngine.EraseObject(GameManager.Instance.DrawEngine.UnitMap, unit);
                    GameManager.Instance.DrawEngine.DrawScenery(unit.PositionX, unit.PositionY);

                    unit.PositionX = newX;
                    unit.PositionY = newY;

                    GameManager.Instance.DrawEngine.AddObject(GameManager.Instance.DrawEngine.UnitMap, unit);
                    GameManager.Instance.DrawEngine.DrawUnits(unit.PositionX, unit.PositionY);

                    if(unit.Name == "Player") {
                        GameManager.Instance.LogsEngine.PrintLog("Player moved to " + newX + ", " + newY,
                            ConsoleColor.White, ConsoleColor.Black);
                    }
                }
            }
        }

        /// <summary>
        /// Checks if a targeted cell contains a unit. Returns true / false.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        internal bool CheckCellForUnit(int x, int y) {

            if(GameManager.Instance.DrawEngine.UnitMap[x, y] == null) {

                return false;

            }

            return true;
        }

        internal void DealDamageToUnit(Unit unit) {
            unit.Health--;
            if(unit.Health == 0)
            {
                GameManager.Instance.DrawEngine.EraseObject(GameManager.Instance.DrawEngine.UnitMap,
                    unit);
                GameManager.Instance.GameObjects.Remove(unit);
            }
        }

        internal void KillUnit(Unit unit)
        {
            GameManager.Instance.GameObjects.Remove(unit);
        }

        internal bool CheckIfClearIsCell(int x, int y)
        {

            if(GameManager.Instance.DrawEngine.SceneryMap[x, y].Solidity == false)
            {
                return true;
            } else
            {
                return false;
            }
        }

        internal void MoveUnitTo(Unit unit, int x, int y)
        {
            if (CheckIfClearIsCell(x, y) == true)
            {
                GameManager.Instance.DrawEngine.EraseObject(GameManager.Instance.DrawEngine.UnitMap, unit);
                GameManager.Instance.DrawEngine.DrawScenery(unit.PositionX, unit.PositionY);
                unit.PositionX = x;
                unit.PositionY = y;
                GameManager.Instance.DrawEngine.AddObject(GameManager.Instance.DrawEngine.UnitMap, unit);
                GameManager.Instance.DrawEngine.DrawUnits(unit.PositionX, unit.PositionY);
                if(unit.Name == "Player") {
                    GameManager.Instance.LogsEngine.PrintLog("Player moved to " + unit.PositionX + ", " + unit.PositionY,
                        ConsoleColor.White, ConsoleColor.Black);
                }
            }
        }
    }
}
