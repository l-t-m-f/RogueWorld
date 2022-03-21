using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueWorld.GameObjects.Units
{
    public class Unit_Rogue : Unit
    {

        public Unit_Rogue(int x, int y) : base(x, y)
        {
            Symbol = '@';
            Name = "Rogue";
            Health = 10;
        }
    }
}
