using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Rogue.GameObjects;
using Rogue.GameObjects.Units;

namespace Rogue.Managers
{
    public class UnitManager
    {

        public Unit_Rogue Rogue;

        public UnitManager()
        {
            Rogue = new Unit_Rogue(90, 30);
        }

        public void MoveUnitBy(Unit unit, int x, int y)
        {
            GameManager.Instance.ClearUnitMap(unit);
            unit.PositionX += x;
            unit.PositionY += y;
            GameManager.Instance.UpdateUnitMap(unit);
        }

        public void MoveUnitTo(Unit unit, int x, int y)
        {
            GameManager.Instance.ClearUnitMap(unit);
            unit.PositionX = x;
            unit.PositionY = y;
            GameManager.Instance.UpdateUnitMap(unit);
        }

    }
}
