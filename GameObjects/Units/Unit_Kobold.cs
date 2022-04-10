using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueWorld.GameObjects.Units
{
    internal class Unit_Kobold : Unit
    {

        public Unit_Kobold(int x, int y,
            int minHealth, int maxHealth,
            int minStrength, int maxStrength,
            int minSpeed, int maxSpeed,
            int minIntelligence, int maxIntelligence) : base(x, y,
            minHealth, maxHealth,
            minStrength, maxStrength,
            minSpeed, maxSpeed,
            minIntelligence, maxIntelligence)
        {
            Faction = 1;
            Symbol = 'k';
            Name = "Kobold";
            FgColor = ConsoleColor.Red;
        }

    }
}
