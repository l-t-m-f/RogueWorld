using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RogueWorld.Managers;

namespace RogueWorld.GameObjects.Units
{
    public abstract class Unit : GameObject
    {
        private int _health;
        public int Health { get { return _health; }
            set { _health = value; } }
        public int Faction;

        public Directions Direction;

        public Unit(int x, int y)
        {
            PositionX = x;
            PositionY = y;
        }
    }
}
