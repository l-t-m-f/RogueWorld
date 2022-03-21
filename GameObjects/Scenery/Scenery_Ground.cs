using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueWorld.GameObjects.Scenery
{
    public class Scenery_Ground : Scenery
    {

        public Scenery_Ground(int x, int y) : base(x, y)
        {
            Symbol = '.';
            Name = "Ground";
            Durability = -2;
        }

    }
}
