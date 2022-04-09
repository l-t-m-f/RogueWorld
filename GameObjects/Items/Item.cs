namespace RogueWorld.GameObjects.Items {
    internal abstract class Item : GameObject
    {
        public string Name { get; set; }
        public int Durability { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }

        public Item(int x, int y) {
            PositionX = x;
            PositionY = y;
        }

        internal void PickItem() {

        }

    }
}
