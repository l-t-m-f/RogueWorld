using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RogueWorld.GameObjects.Units;

namespace RogueWorld.GameObjects
{
    public abstract class GameObject
    {
        public string Name;
        public char Symbol;
        public int PositionX;
        public int PositionY;
        public ConsoleColor FgColor = ConsoleColor.White;
        public ConsoleColor BgColor = ConsoleColor.Black;

    }
}
