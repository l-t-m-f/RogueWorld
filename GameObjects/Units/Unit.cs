namespace RogueWorld.GameObjects.Units {

    internal class Equipment {

        public int ArmorTotal { get; set; }

        public EquipmentSlot HeadSlot;
        public EquipmentSlot ArmsSlot;
        public EquipmentSlot BodySlot;
        public EquipmentSlot LegsSlot;
        public EquipmentSlot FeetSlot;
    }

    internal class EquipmentSlot {

        public string SlotName { get; set; }
        
        public Item? ItemInSlot { get; set; } 

    }

    internal class Stats
    {

        public int MaxHealth;
        public int CurrentHealth;

        public int Strength;
        public int Toughness;
        public int Speed;
        public int Intelligence;
    }

    internal abstract class Unit : GameObject
    {

        public Stats Stats;

        public int Faction;

        public Directions Direction;

        public Unit(int x, int y)
        {
            PositionX = x;
            PositionY = y;

            Stats = new Stats();
        }

        public abstract void SetBaseStats(int minHealth, int minStat,
            int healthBonus = 0, int strengthBonus = 0,
            int toughnessBonus = 0, int speedBonus = 0,
            int intelligenceBonus = 0);

        internal void DealDamageToUnit(int damage) {
            Stats.CurrentHealth -= damage;
        }

        internal void KillUnit() {

            if (Stats.CurrentHealth == 0) {
                GameManager.Instance.EraseObject(GameManager.Instance.UnitMap, this);
                GameManager.Instance.TurnUnits.Remove(this);
            }
        }

        internal void ActAtOffset(int x, int y) {

            int newX = PositionX + x;
            int newY = PositionY + y;

            // Make sur the new position is not "out-of-bounds":
            if (newX < GameManager.COLS - 1 && newX > 1 &&
                newY < GameManager.ROWS - 1 && newY > 1) {

                if (this.TryAttack(newX, newY)) {

                    var target = GameManager.Instance.UnitMap[newX, newY];
                    target.KillUnit();

                } else if (TryMove(newX, newY)) {
                } else {

                }
            }
        }

        internal virtual bool TryMove(int col, int row) {

            if (GameManager.Instance.CheckIfClearIsCell(col, row) == true) {

                // If the new position doesn't contain a unit, check if it contains a solid scenery.
                // If its clear, allow the movement.

                GameManager.Instance.EraseObject(GameManager.Instance.UnitMap, this);
                GameManager.Instance.Draw(PositionX, PositionY);

                PositionX = col;
                PositionY = row;

                GameManager.Instance.AddObject(GameManager.Instance.UnitMap, this);
                GameManager.Instance.Draw(PositionX, PositionY);

                return true;
            }

            return false;
        }

        internal virtual bool TryAttack(int col, int row) {

            var damage = 1;
            
            var target = GameManager.Instance.UnitMap[col, row];

            // If new position already contains a unit of the opposing faction.
            if (GameManager.Instance.CheckCellForUnit(col, row) == true
            && target.Faction != this.Faction) {

                target.DealDamageToUnit(damage);

                return true;

            }

            return false;
        }

        internal void CalculateLOS(Unit target) {

            var targetX = target.PositionX;
            var targetY = target.PositionY;

            var absDistX = Math.Abs(PositionX - targetX);
            var absDistY = Math.Abs(PositionY - targetY);

            Util.Write("     ", 0, 1, ConsoleColor.DarkGray);
            Util.Write(absDistX + " " + absDistY, 0, 1, ConsoleColor.DarkGray);

        }

    }
}
