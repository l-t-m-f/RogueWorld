using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RogueWorld.Managers;

namespace RogueWorld.GameObjects.Units
{

    public struct Stats
    {

        public int Health;
        public int Strength;
        public int Toughness;
        public int Speed;
        public int Intelligence;
    }

    public abstract class Unit : GameObject
    {

        public Stats Stats;

        public int Faction;

        public Directions Direction;

        public Unit(int x, int y)
        {
            PositionX = x;
            PositionY = y;

            SetBaseStats(10, 4);
        }

        /// <summary>
        /// Sets the base stats of the unit. 
        /// Called as part of the Unit constructor.
        /// </summary>
        public void SetBaseStats(int minHealth, int minStat,
            int healthBonus = 0, int strengthBonus = 0, 
            int toughnessBonus = 0, int speedBonus = 0, 
            int intelligenceBonus = 0) {
            Random random = new Random();

            Stats.Health = minHealth + random.Next(10) + healthBonus;
            Stats.Strength = minStat + random.Next(10) + strengthBonus;
            Stats.Toughness = minStat + random.Next(10) + toughnessBonus;
            Stats.Speed = minStat + random.Next(10) + speedBonus;
            Stats.Intelligence = minStat + random.Next(10) + intelligenceBonus;
        }
    }
}
