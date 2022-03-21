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
        private int _durability;
        public int Durability
        {
            get { return _durability; }
            set { _durability = value; }
        }

        private bool _solidity;
        public bool Solidity
        {
            get { return _solidity; }
            set { _solidity = value; }
        }

        public Scenery(int x, int y)
        {
            PositionX = x;
            PositionY = y;
        }
    }
}
