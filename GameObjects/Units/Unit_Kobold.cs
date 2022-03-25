using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueWorld.GameObjects.Units
{
    public class Unit_Kobold : Unit
    {

        public Unit_Kobold(int x, int y) : base(x, y)
        {
            Faction = 1;
            Symbol = 'k';
            Name = "Kobold";
            Health = 3;
            FgColor = ConsoleColor.Red;
        }
    }
}
