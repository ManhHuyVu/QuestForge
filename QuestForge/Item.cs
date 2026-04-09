using System;

namespace QuestForge
{
    public class ItemMaster // Control all item generation and management in the game, including loot drops, crafting, and inventory management
    {
        public Rarities currentRarity { get; set; } // default rarity for loot drops, can be changed based on game events or player actions
        
        public enum Rarities { R, SR, SSR }; // R for Rare, SR for Super Rare, SSR for Super Super Rare

        public Dictionary<Item, string> ItemList {get; private set;} = new Dictionary<Item, string>() // dictionary of items and their rarities
        {
            {new ItemWeapon("Sword of Destiny", "A legendary sword with immense power.", 'W', 15.0, 10.0, 20), "R"},
            {new ItemArmor("Shield of Valor", "A sturdy shield that can withstand powerful attacks.", 'A', 20.0, 10.0, 15), "R"},
            {new ItemPotion("Health Potion", "A potion that restores health.", 'P', 0.5, 5.0, 50), "R"},
            {new ItemQuestObject("Golden Key", "A key that opens the door to the treasure room.", 'Q', 0.1, 100.0, "The Lost Treasure"), "SR"},
            {new ItemWeapon("Axe of Fury", "A powerful axe that can deal massive damage.", 'W', 20.0, 15.0, 30), "SSR"}
        };

        public void AddNewItem(Item item, string rarity) // Add a new item to the ItemList dictionary, with its corresponding rarity
        {
            if (!Enum.TryParse(rarity, out Rarities parsedRarity))
            {
                throw new ArgumentException("Invalid rarity. Valid rarities are: R, SR, SSR.");
            }
            if (ItemList.ContainsKey(item))
            {
                throw new ArgumentException("Item already exists in the ItemList.");
            }
            ItemList.Add(item, rarity);
        }

        public Item MakeLoot() // Generate loot based on the current rarity, using a random selection from the ItemList dictionary
        {
            Dictionary<Item, string> RaritiesDrawn = new Dictionary<Item, string>();
            foreach(var pair in ItemList)
            {
                if (pair.Value == currentRarity.ToString())
                {
                    RaritiesDrawn.Add(pair.Key, pair.Value);
                }
                else if (pair.Value == currentRarity.ToString())
                {
                    RaritiesDrawn.Add(pair.Key, pair.Value);
                }
                else if (pair.Value == currentRarity.ToString())
                {
                    RaritiesDrawn.Add(pair.Key, pair.Value);
                }
            }
            int index = new Random().Next(0, RaritiesDrawn.Count - 1);
            return RaritiesDrawn.ElementAt(index).Key
            ;
        }

    }

    public class Item: GameEntity // Base item class, with common properties and methods for all items
    {
        private char ItemType; // 'W' for weapon, 'A' for armor, 'P' for potions, 'Q' for quest objects
        public double Weight {get; private set; }
        public double Value {get; private set; } 

        public Item(string name, string description, char itemType, double weight, double value) : base(name, 'I', description)
        {
            ItemType = itemType;
            Weight = weight;
            Value = value;
        }

        public override string ToString()
        {
            return $@"{base.ToString()} 
            - Type: {ItemType}, Weight: {Weight}, Value: {Value}, Rarity: ";
        }
    }

    public class ItemWeapon : Item // Weapon item class, with additional properties and methods specific to weapons
    {
        public int AttackPower { get; private set; }

        public ItemWeapon(string name, string description, char itemType, double weight, double value, int attackPower) : base(name, description, itemType, weight, value)
        {
            AttackPower = attackPower;
        }

        public override string ToString()
        {
            return $@"{base.ToString()} 
            - Attack Power: {AttackPower}";
        }
    }

    public class ItemArmor : Item // Armor item class, with additional properties and methods specific to armor
    {
        public int DefensePower { get; private set; }

        public ItemArmor(string name, string description, char itemType, double weight, double value, int defensePower) : base(name, description, itemType, weight, value)
        {
            DefensePower = defensePower;
        }

        public override string ToString()
        {
            return $@"{base.ToString()} 
            - Defense Power: {DefensePower}";
        }
    }

    public class ItemPotion : Item // Potion item class, with additional properties and methods specific to potions
    {
        private int HealAmount;

        public ItemPotion(string name, string description, char itemType, double weight, double value, int healAmount) : base(name, description, itemType, weight, value)
        {
            HealAmount = healAmount;
        }

        public override string ToString()
        {
            return $@"{base.ToString()} 
            - Heal Amount: {HealAmount}";
        }
    }

    public class ItemQuestObject : Item // Quest object item class, with additional properties and methods specific to quest objects
    {
        private string QuestName;

        public ItemQuestObject(string name, string description, char itemType, double weight, double value, string questName) : base(name, description, itemType, weight, value)
        {
            QuestName = questName;
        }

        public override string ToString()
        {
            return $@"{base.ToString()} 
            - Quest Name: {QuestName}";
        }
    }
}