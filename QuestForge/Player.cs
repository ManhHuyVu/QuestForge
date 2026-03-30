using System;

namespace QuestForge
{
    public class Player
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public double Attack {get; set;} // Attack point
        public int Score { get; set; }

        private List<Dictionary<Item,int>> inventory; // List of dictionary to store item name and quantity
        // TODO: Does this one need a description of the item as well? Maybe a separate Item class? (string for now)

        public Player(string name)
        {
            Name = name;
            Level = 1;
            Experience = 0;
            Attack = 10.0; // Starting attack point
            Score = 0;
            inventory = new List<Dictionary<string,int>>();
        }

        public void PickupItem(string itemName, int quantity)
        {
            bool checkItem = false;
            foreach (var item in inventory)
            {
                if (item.ContainsKey(itemName))
                {
                    checkItem = true;
                    item[itemName] += quantity;
                }
            }
            if (!checkItem)
            {
                inventory.Add(new Dictionary<string, int> { { itemName, quantity } });
            }
        }
    }
}