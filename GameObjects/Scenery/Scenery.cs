using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Rogue.Managers;

namespace Rogue.GameObjects.Scenery
{
    public abstract class Scenery : GameObject
    {
        private int _durability;
        public int Durability
        {
            get { return _durability; }
            set { _durability = value; }
        }

        public Scenery(int x, int y)
        {
            PositionX = x;
            PositionY = y;
        }
    }
}
