using System;

namespace QuestForge
{
    public class Player: GameEntity
    {
        public override string Name
        {
            get { return base.Name; }
            set{
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Player name cannot be empty.");
                }
                base.Name = value;
            }
        }
        public int Level { get; private set; }
        public int Experience { get; set; }
        public double Attack {get; private set;} // Attack point
        public int Score { get; private set; }

        private List<Dictionary<Item,int>> inventory; // List of dictionary to store item object and quantity

        public Player(string namee, string descriptionAndCatchphrase) : base(namee, 'P', descriptionAndCatchphrase)
        {
            Level = 1;
            Experience = 0;
            Attack = 10.0; // Starting attack point
            Score = 0;
            inventory = new List<Dictionary<Item,int>>();
        }

        public void AddItemToInventory(Item itemObj, int quantity)
        {
            bool checkItem = false;
            foreach (var item in inventory)
            {
                if (item.ContainsKey(itemObj))
                {
                    checkItem = true;
                    item[itemObj] += quantity;
                }
            }
            if (!checkItem)
            {
                inventory.Add(new Dictionary<Item, int> { { itemObj, quantity } });
            }
        }

        public override string ToString()
        {
            return $@"{base.ToString()} 
            - Level: {Level}, Experience: {Experience}, Attack: {Attack}, Score: {Score}";
        }
    }
}