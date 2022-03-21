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

        internal void MoveUnitBy(Unit unit, int x, int y)
        {
            if(CheckIfClearIsCell(unit.PositionX + x, unit.PositionY + y) == true)
            {
                GameManager.Instance.DrawEngine.EraseObject(GameManager.Instance.DrawEngine.UnitMap, unit);
                GameManager.Instance.DrawEngine.DrawScenery(unit.PositionX, unit.PositionY);
                unit.PositionX += x;
                unit.PositionY += y;
                GameManager.Instance.DrawEngine.AddObject(GameManager.Instance.DrawEngine.UnitMap, unit);
                GameManager.Instance.DrawEngine.DrawUnits(unit.PositionX, unit.PositionY);
                GameManager.Instance.LogsEngine.PrintLog("Player moved to " + unit.PositionX + ", " + unit.PositionY);
            }
        }

        internal bool CheckIfClearIsCell(int x, int y)
        {

            if(GameManager.Instance.DrawEngine.UnitMap[x, y] == null &&
               GameManager.Instance.DrawEngine.SceneryMap[x, y].Solidity == false)
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
                GameManager.Instance.LogsEngine.PrintLog("Player moved to " + unit.PositionX + ", " + unit.PositionY);
            }
        }
    }
}
