using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RogueWorld.GameObjects.Units;

namespace RogueWorld.Managers
{
    internal class TurnEngine
    {
        internal void PlayPlayerTurn()
        {
            switch (GameManager.Instance.UnitEngine.Rogue.Direction) {
                case Directions.Up:
                    GameManager.Instance.UnitEngine.MoveUnitBy(GameManager.Instance.UnitEngine.Rogue, 0, -1);
                    break;
                case Directions.Left:
                    GameManager.Instance.UnitEngine.MoveUnitBy(GameManager.Instance.UnitEngine.Rogue, -1, 0);
                    break;
                case Directions.Down:
                    GameManager.Instance.UnitEngine.MoveUnitBy(GameManager.Instance.UnitEngine.Rogue, 0, 1);
                    break;
                case Directions.Right:
                    GameManager.Instance.UnitEngine.MoveUnitBy(GameManager.Instance.UnitEngine.Rogue, 1, 0);
                    break;
                default:
                    break;
            }
        }

        internal void PlayAITurn()
        {

            foreach (Unit unit in GameManager.Instance.GameObjects)
            {

                if(unit.Health == 0) {
                    GameManager.Instance.DrawEngine.EraseObject(GameManager.Instance.DrawEngine.UnitMap,
                    unit);
                } else {
 
                    var x = 0;
                    var y = 0;

                    Random random = new Random();
                    if(random.Next(2) == 0) {
                        random = new Random();
                        x = random.Next(-1, 2);
                    } else {
                        random = new Random();
                        y = random.Next(-1, 2);
                    }
                    
                    GameManager.Instance.UnitEngine.MoveUnitBy(unit, x, y);

                }
            }
        }
    }
}
