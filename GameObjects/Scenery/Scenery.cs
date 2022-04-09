using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RogueWorld.Managers;

namespace RogueWorld.GameObjects.Scenery
{
    public abstract class Scenery : GameObject
    {
        public int Durability { get; set; }
        public bool Solidity { get; set; }

        public Scenery(int x, int y)
        {
            PositionX = x;
            PositionY = y;
        }
    }
}
