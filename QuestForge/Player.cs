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
        public double Health { get; private set; } // Health point
        public double Attack {get; private set;} // Attack point
        public double Defense {get; private set;} // Defense point
        public int Score { get; private set; }
        public Zone CurrentZone { get; private set; }

        private Dictionary<Item,int> inventory; // List of dictionary to store item object and quantity

        public Player(string namee, string descriptionAndCatchphrase) : base(namee, 'P', descriptionAndCatchphrase)
        {
            Health = 100.0; // Starting health point
            Attack = 20.0; // Starting attack point
            Defense = 10.0; // Starting defense point
            Score = 0;
            inventory = new Dictionary<Item,int>();
        }

        public void AddItemToInventory(Item itemObj, int quantity)
        {
            bool checkItem = false;
            foreach (var pair in inventory)
            {
                if (pair.Key == itemObj)
                {
                    inventory[pair.Key] += quantity;
                    checkItem = true;
                    break;
                }
            }
            if (!checkItem)
            {
                inventory.Add(itemObj, quantity);
            }
        }

        public void RemoveItemFromInventory(Item itemObj, int quantity)
        {
            foreach (var pair in inventory)
            {
                if (pair.Key == itemObj)
                {
                    inventory[pair.Key] -= quantity;
                    if (inventory[pair.Key] == 0)
                    {
                        inventory.Remove(pair.Key);
                    }
                    return;
                }
            }
            throw new ArgumentException("Item not found in inventory.");
        }

        public Dictionary<Item, int> FindItemByName(string itemName)
        {
            var result = new Dictionary<Item, int>();
            foreach (var pair in inventory)
            {
                if (pair.Key.Name == itemName)
                {
                    result.Add(pair.Key, pair.Value);
                }
            }
            return result;
        }
        
        public bool MovePlayer(ZoneManager zm, string toZone)
        {
            // Check if target zone exists
            Zone target = zm.GetZoneByName(toZone);
            if (target == null)
                return false;

            // Validate travel rules
            if (!zm.CanTravel(CurrentZone, target))
                return false;

            // Move player
            CurrentZone = target;
            return true;
        }

        public override string ToString()
        {   
            string inventoryString = "Inventory:\n";
            foreach (var pair in inventory)
            {
                inventoryString += $"Item: {pair.Key.Name}, Quantity: {pair.Value}\n";
            }
            return $@"{base.ToString()} 
            - Character: Player
            - Attack: {Attack}, Defense: {Defense}, Score: {Score}
            - {inventoryString}";
        }
    }
}