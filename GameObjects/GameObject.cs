using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Rogue.GameObjects.Units;

namespace Rogue.GameObjects
{
    public abstract class GameObject
    {
        public string Name;
        public char Symbol;
        public int PositionX;
        public int PositionY;
    }
}
