using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueWorld.GameObjects.Units
{
    internal class Unit_Rogue : Unit
    {

        public Unit_Rogue(int x, int y) : base(x, y)
        {
            Faction = 0;
            Symbol = '@';
            Name = "Player";

            SetBaseStats(10, 4);
        }

        public override void SetBaseStats(int minHealth, int minStat,
            int healthBonus = 0, int strengthBonus = 0,
            int toughnessBonus = 0, int speedBonus = 0,
            int intelligenceBonus = 0) {
            Random random = new Random();

            Stats.MaxHealth = minHealth + random.Next(10) + healthBonus;
            Stats.CurrentHealth = Stats.MaxHealth;
            Stats.Strength = minStat + random.Next(10) + strengthBonus;
            Stats.Toughness = minStat + random.Next(10) + toughnessBonus;
            Stats.Speed = minStat + random.Next(10) + speedBonus;
            Stats.Intelligence = minStat + random.Next(10) + intelligenceBonus;
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
                        target.Stats.CurrentHealth + "/" +
                        target.Stats.MaxHealth + ")",
                        ConsoleColor.Red, ConsoleColor.Black);
                }

                return true;

            }

            return false;
        }
    }
}
