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
        public double Attack {get; private set;} // Attack point
        public double Defense {get; private set;} // Defense point
        public int Score { get; private set; }

        private List<Dictionary<Item,int>> inventory; // List of dictionary to store item object and quantity

        public Player(string namee, string descriptionAndCatchphrase) : base(namee, 'P', descriptionAndCatchphrase)
        {
            Attack = 20.0; // Starting attack point
            Defense = 10.0; // Starting defense point
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
            - Attack: {Attack}, Defense: {Defense}, Score: {Score}";
        }
    }
}