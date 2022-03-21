using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Rogue.Managers;

namespace Rogue.GameObjects.Units
{
    public abstract class Unit : GameObject
    {
        private int _health;
        public int Health { get { return _health; }
            set { _health = value; } }

        public Unit(int x, int y)
        {
            PositionX = x;
            PositionY = y;
        }
    }
}
