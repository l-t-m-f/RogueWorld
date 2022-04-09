using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueWorld.GameObjects.Units
{
    internal class Unit_Kobold : Unit
    {

        public Unit_Kobold(int x, int y) : base(x, y)
        {
            Faction = 1;
            Symbol = 'k';
            Name = "Kobold";
            FgColor = ConsoleColor.Red;

            SetBaseStats(3, 0);
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
    }
}
