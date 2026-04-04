using System;

namespace QuestForge
{
    public class ItemMaster
    {
        public string currentRarity { get; set; } = "R"; // default rarity for loot drops, can be changed based on game events or player actions
        
        private enum Rarities { R, SR, SSR }; // R for Rare, SR for Super Rare, SSR for Super Super Rare

        private Dictionary<Item, string> ItemList = new Dictionary<Item, string>() // dictionary of items and their rarities
        {
            {new ItemWeapon("Sword of Destiny", "A legendary sword with immense power.", 'W', 15.0, 10.0, 20), "R"},
            {new ItemArmor("Shield of Valor", "A sturdy shield that can withstand powerful attacks.", 'A', 20.0, 10.0, 15), "R"},
            {new ItemPotion("Health Potion", "A potion that restores health.", 'P', 0.5, 5.0, 50), "R"},
            {new ItemQuestObject("Golden Key", "A key that opens the door to the treasure room.", 'Q', 0.1, 100.0, "The Lost Treasure"), "SR"}
        };

        public Item MakeLoot()
        {
            Random rand = new Random();
            int index = currentRarity switch
            {
                "R" => 1,
                "SR" => 2,
                "SSR" => 3,
                _ => 0
            };
            if(index == 0)
            {
                index = rand.Next(1, 3);
            }
            Dictionary<Item, string> RaritiesDrawn = new Dictionary<Item, string>();
            foreach(var pair in ItemList)
            {
                if (pair.Value == Rarities.R.ToString() && index == 1)
                {
                    RaritiesDrawn.Add(pair.Key, pair.Value);
                }
                else if (pair.Value == Rarities.SR.ToString() && index == 2)
                {
                    RaritiesDrawn.Add(pair.Key, pair.Value);
                }
                else if (pair.Value == Rarities.SSR.ToString() && index == 3)
                {
                    RaritiesDrawn.Add(pair.Key, pair.Value);
                }
            }
            index = rand.Next(0, RaritiesDrawn.Count - 1);
            return RaritiesDrawn.ElementAt(index).Key
            ;
        }

    }

    public class Item: GameEntity
    {
        private char ItemType; // 'W' for weapon, 'A' for armor, 'P' for potions, 'Q' for quest objects
        private double Weight;
        private double Value;

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

    public class ItemWeapon : Item
    {
        private int AttackPower;

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

    public class ItemArmor : Item
    {
        private int DefensePower;

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

    public class ItemPotion : Item
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

    public class ItemQuestObject : Item
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