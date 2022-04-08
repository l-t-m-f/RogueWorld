using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RogueWorld.Utilities;
using RogueWorld.GameObjects;
using RogueWorld.GameObjects.Units;

namespace RogueWorld.Managers
{
    internal class UnitManager
    {

        public Unit_Rogue Rogue;
        public Unit_Kobold Kobold;

        public UnitManager()
        {
            Rogue = new Unit_Rogue(40, 20);
            Kobold = new Unit_Kobold(50, 30);
        }

        // Draw units -- NEW FOR CLASS: Now uses the Write helper method we made earlier.

        internal void DrawUnits(int x, int y)
        {
            if (GameManager.Instance.UnitMap[x, y] != null)
            {
                Util.Write(GameManager.Instance.UnitMap[x, y].Symbol,
                    x, y,
                    GameManager.Instance.UnitMap[x, y].FgColor, GameManager.Instance.UnitMap[x, y].BgColor);
            }
        }

        internal void DrawUnits()
        {
            for (int x = 0; x < GameManager.COLS; x++)
            {
                for (int y = 0; y < GameManager.ROWS - 1; y++)
                {
                    if (GameManager.Instance.UnitMap[x, y] != null)
                    {
                        Util.Write(GameManager.Instance.UnitMap[x, y].Symbol,
                            x, y,
                            GameManager.Instance.UnitMap[x, y].FgColor, GameManager.Instance.UnitMap[x, y].BgColor);
                    }
                }
            }
        }

        internal void ActAtOffset(Unit unit, int x, int y) {

            int newX = unit.PositionX + x;
            int newY = unit.PositionY + y;

            // Make sur the new position is not "out-of-bounds":
            if (newX < GameManager.COLS-1 && newX > 1 && 
                newY < GameManager.ROWS-1 && newY > 1) {

                if (TryAttack(unit, newX, newY)) {
                    if(unit.Name == "Player") {
                        GameManager.Instance.GUIManager.PrintLog("Player attacked to " + newX + ", " + newY + "(" +
                            GameManager.Instance.UnitMap[newX, newY].Stats.CurrentHealth + "/" + 
                            GameManager.Instance.UnitMap[newX, newY].Stats.MaxHealth + ")",
                            ConsoleColor.Red, ConsoleColor.Black);
                    }

                    UpdateUnitLivingState(GameManager.Instance.UnitMap[newX, newY]);

                } else if (TryMove(unit, newX, newY)) {
                    if(unit.Name == "Player") {
                        GameManager.Instance.GUIManager.PrintLog("Player moved to " + newX + ", " + newY,
                            ConsoleColor.White, ConsoleColor.Black);
                    }
                } else {

                }
            }
        }

        internal bool TryAttack(Unit attacker, int x, int y) {

        var damage = 1;

        // If new position already contains a unit of the opposing faction.
            if (CheckCellForUnit(x, y) == true
            && GameManager.Instance.UnitMap[x, y].Faction != attacker.Faction) {

                DealDamageToUnit(GameManager.Instance.UnitMap[x, y], damage);
                return true;

            }

            return false;
        }

        internal bool TryMove(Unit mover, int x, int y) {

            if(CheckIfClearIsCell(x, y) == true) {
                // If the new position doesn't contain a unit, check if it contains a solid scenery.
                // If its clear, allow the movement.

                GameManager.Instance.EraseObject(GameManager.Instance.UnitMap, mover);
                GameManager.Instance.DrawStaticObject(mover.PositionX, mover.PositionY);

                mover.PositionX = x;
                mover.PositionY = y;

                GameManager.Instance.AddObject(GameManager.Instance.UnitMap, mover);
                GameManager.Instance.UnitManager.DrawUnits(mover.PositionX, mover.PositionY);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Checks if a targeted cell contains a unit. Returns true / false.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        internal bool CheckCellForUnit(int x, int y) {

            if(GameManager.Instance.UnitMap[x, y] == null) {

                return false;

            }

            return true;
        }

        internal void DealDamageToUnit(Unit unit, int damage) {

            unit.Stats.CurrentHealth--;
                              
        }

        internal void UpdateUnitLivingState(Unit unit) {

            var T = GameManager.Instance.TurnManager;

            if(unit.Stats.CurrentHealth == 0)
            {
                GameManager.Instance.EraseObject(GameManager.Instance.UnitMap,
                    unit);
                T.TurnUnits.Remove(unit);
            }      
        }

        internal void KillUnit(Unit unit)
        {
            var T = GameManager.Instance.TurnManager;
            T.TurnUnits.Remove(unit);
        }

        internal bool CheckIfClearIsCell(int x, int y)
        {

            if(GameManager.Instance.SceneryMap[x, y].Solidity == false)
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
                GameManager.Instance.EraseObject(GameManager.Instance.UnitMap, unit);
                GameManager.Instance.DrawStaticObject(unit.PositionX, unit.PositionY);
                unit.PositionX = x;
                unit.PositionY = y;
                GameManager.Instance.AddObject(GameManager.Instance.UnitMap, unit);
                GameManager.Instance.UnitManager.DrawUnits(unit.PositionX, unit.PositionY);
                if(unit.Name == "Player") {
                    GameManager.Instance.GUIManager.PrintLog("Player moved to " + unit.PositionX + ", " + unit.PositionY,
                        ConsoleColor.White, ConsoleColor.Black);
                }
            }
        }
    }
}
