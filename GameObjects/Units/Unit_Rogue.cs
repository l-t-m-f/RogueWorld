using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueWorld.GameObjects.Units
{
    internal class Unit_Rogue : Unit
    {

        public Unit_Rogue(int x, int y,
            int minHealth, int maxHealth,
            int minStrength, int maxStrength,
            int minSpeed, int maxSpeed,
            int minIntelligence, int maxIntelligence) : base(x, y,
            minHealth, maxHealth,
            minStrength, maxStrength,
            minSpeed, maxSpeed,
            minIntelligence, maxIntelligence)
        {
            Faction = 0;
            Symbol = '@';
            Name = "Player";
        }

        internal void UpdateColorBasedOnHealth()
        {
            

            if (Attributes.CurrentHealth == Attributes.MaxHealth)
            {
                FgColor = ConsoleColor.White;
            } 
            else if (Attributes.CurrentHealth < 0.25f * Attributes.MaxHealth)
            {
                FgColor = ConsoleColor.Red;

            } 
            else if (Attributes.CurrentHealth < 0.5f * Attributes.MaxHealth)
            {
                FgColor = ConsoleColor.Yellow;

            } else {
                FgColor = ConsoleColor.Green;
            }
        }

        internal override bool TryMove(int col, int row) {

            if (GameManager.Instance.CheckIfClearIsCell(col, row) == true) {

                // If the new position doesn't contain a unit, check if it contains a solid scenery.
                // If its clear, allow the movement.

                GameManager.Instance.EraseObject(GameManager.Instance.UnitMap, this);
                GameManager.Instance.Draw(PositionX, PositionY);

                PositionX = col;
                PositionY = row;

                GameManager.Instance.AddObject(GameManager.Instance.UnitMap, this);
                GameManager.Instance.Draw(PositionX, PositionY);

                if (Name == "Player") {

                    // Prints out the action in the log.
                    GameManager.Instance.GUIManager.PrintLog("Player moved to " + col + ", " + row,
                        ConsoleColor.White, ConsoleColor.Black);
                }

                return true;
            }

            return false;
        }

        internal override bool TryAttack(int col, int row) {

            var damage = 1;

            var target = GameManager.Instance.UnitMap[col, row];

            // If new position already contains a unit of the opposing faction.
            if (GameManager.Instance.CheckCellForUnit(col, row) == true
            && target.Faction != this.Faction) {

                target.DealDamageToUnit(damage);

                if (Name == "Player") {

                    // Prints out the action in the log.
                    GameManager.Instance.GUIManager.PrintLog("Player attacked to " + col + ", " + row + "(" +
                        target.Attributes.CurrentHealth + "/" +
                        target.Attributes.MaxHealth + ")",
                        ConsoleColor.Red, ConsoleColor.Black);
                }

                return true;

            }

            return false;
        }
    }
}
