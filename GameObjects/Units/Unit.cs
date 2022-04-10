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

    internal class States
    {
        public int Burning { get; set; }
        public int Frozen { get; set; }
        public int Poisoned { get; set; }
        public int Soaked { get; set; }

        public States()
        {
            Burning = 0;
            Frozen = 0;
            Poisoned = 0;
            Soaked = 0;
        }
    }

    internal class Attributes
    {

        public int MaxHealth { get; set; }
        public int CurrentHealth { get; set; }
        public int MaxStrength { get; set; }
        public int CurrentStrength { get; set; }
        public int MaxSpeed { get; set; }
        public int CurrentSpeed { get; set; }
        public int MaxIntelligence { get; set; }
        public int CurrentIntelligence { get; set; }

        //Removed toughness, we'll deal with it somewhere else (equipment).

        public Attributes(int minHealth, int maxHealth,
            int minStrength, int maxStrength,
            int minSpeed, int maxSpeed,
            int minIntelligence, int maxIntelligence)
        {
            Random random = new();

            MaxHealth = 4 + random.Next(minHealth, maxHealth + 1);
            MaxStrength = 1 + random.Next(minStrength, maxStrength + 1);
            MaxSpeed = 1 + random.Next(minSpeed, maxSpeed + 1);
            MaxIntelligence = 1 + random.Next(minIntelligence, maxIntelligence + 1);

            CurrentHealth = MaxHealth;
            CurrentStrength = MaxStrength;
            CurrentSpeed = MaxSpeed;
            CurrentIntelligence = MaxIntelligence;
        }

    }

    internal abstract class Unit : GameObject
    {

        public Attributes Attributes;

        public States States;

        public int Faction;

        public Directions Direction;

        public Unit(int x, int y,
            int minHealth, int maxHealth,
            int minStrength, int maxStrength,
            int minSpeed, int maxSpeed,
            int minIntelligence, int maxIntelligence)
        {
            PositionX = x;
            PositionY = y;

            Attributes = new(minHealth, maxHealth,
                minStrength, maxStrength,
                minSpeed, maxSpeed,
                minIntelligence, maxIntelligence);
            States = new();
        }

        internal void DealDamageToUnit(int damage) {
            Attributes.CurrentHealth -= damage;
        }

        internal void KillUnit() {

            if (Attributes.CurrentHealth == 0) {
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

        internal int CalculateLOS(Unit target) {

            var targetX = target.PositionX;
            var targetY = target.PositionY;

            var absDistX = Math.Abs(PositionX - targetX);
            var absDistY = Math.Abs(PositionY - targetY);

            return absDistX + absDistY;

        }

    }
}
