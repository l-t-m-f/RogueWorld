namespace RogueWorld.GameObjects.Items {

    internal class Item_FireSword : Item {

        public Item_FireSword(int x, int y) : base(x, y) {
            Symbol = 't';
            ItemName = "Fire Sword";
            Durability = 5;
            Defense = 0;
            FgColor = ConsoleColor.Magenta;
        }

    }
}
