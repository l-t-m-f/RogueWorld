using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueWorld.GameObjects.Scenery
{
    public class Scenery_Boulder : Scenery
    {

        public Scenery_Boulder(int x, int y) : base(x, y) {
            Symbol = 'O';
            Name = "Ground";
            Durability = 15;
            FgColor = ConsoleColor.DarkBlue;
            Solidity = true;
        }

    }
}
