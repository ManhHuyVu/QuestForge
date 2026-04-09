using System;

namespace QuestForge
{
    public class Player: GameEntity
    {
        public override string Name
        {
            get { return base.Name; }
            set{
                if (string.IsNullOrWhiteSpace(value)) // Validate that player name is not empty or whitespace
                {
                    throw new ArgumentException("Player name cannot be empty.");
                }
                base.Name = value;
            }
        }
        public double Health { get; private set; } // Health point
        public double Attack {get; private set;} // Attack point
        public double Defense {get; set;} // Defense point
        public int Score { get; private set; }
        public Zone CurrentZone { get; private set; }

        private Dictionary<Item,int> inventory; // List of dictionary to store item object and quantity

        public Player(string namee, string descriptionAndCatchphrase) : base(namee, 'P', descriptionAndCatchphrase)
        {
            Health = 100.0; // Starting health point
            Attack = 20.0; // Starting attack point
            Defense = 0; // Starting defense point
            Score = 0;
            inventory = new Dictionary<Item,int>();
        }

        public void Revive()
        {
            Health = 100.0; // Reset health to full
            Attack = 20.0; // Reset attack to base value
            Defense = 0; // Reset defense to base value
            inventory.Clear(); // Clear inventory
            Console.WriteLine($"{Name} has been revived with full health and a clean inventory.");
        }
        
        // Use in GameEvent.cs in loot event to add item to player's inventory when player wins loot event
        public void AddItemToInventory(Item itemObj, int quantity)
        {
            bool checkItem = false;
            foreach (var pair in inventory)
            {
                if (pair.Key == itemObj)
                {
                    inventory[pair.Key] += quantity;
                    checkItem = true;
                    if (itemObj is ItemWeapon weapon)
                    {
                        Attack += weapon.AttackPower; // Increase player's attack by weapon's attack bonus
                    }
                    else if (itemObj is ItemArmor armor)
                    {
                        Defense += armor.DefensePower; // Increase player's defense by armor's defense bonus    
                    }
                    break;
                }
            }
            if (!checkItem)
            {
                inventory.Add(itemObj, quantity);
                if (itemObj is ItemWeapon weapon)
                {
                    Attack += weapon.AttackPower; // Increase player's attack by weapon's attack bonus
                }
                else if (itemObj is ItemArmor armor)
                {
                    Defense += armor.DefensePower; // Increase player's defense by armor's defense bonus    
                }
            }
        }

        // No use in current phase
        public void RemoveItemFromInventory(Item itemObj, int quantity)
        {
            foreach (var pair in inventory)
            {
                if (pair.Key == itemObj)
                {
                    inventory[pair.Key] -= quantity;
                    if (inventory[pair.Key] <= 0)
                    {
                        inventory.Remove(pair.Key);
                    }
                    return;
                }
            }
            throw new ArgumentException("Item not found in inventory.");
        }
        
        // Use in CombatManager.cs to process damage taken during combat
        public bool TakeDamage(double damage)
        {
            Health -= damage;
            if (Health < 0)
            {
                Health = 0;
                return true; // Player is defeated, == GameEnd
            }
            return false
            ;
        }

        public Dictionary<Item, int> FindItemByName(string itemName) // Return a dictionary of items with the same name and their quantities
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
        
        public bool MovePlayer(ZoneManager zm, string toZone) // Move player to another zone if the move is valid according to the ZoneManager's rules
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
            - Character: Player, Health: {Health}
            - Attack: {Attack}, Defense: {Defense}, Score: {Score}
            - {inventoryString}";
        }
    }
}