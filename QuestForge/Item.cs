using System;

namespace QuestForge
{
    public class Item: GameEntity
    {
        private char ItemType; // 'W' for weapon, 'A' for armor, 'P' for potions, 'Q' for quest objects
        private double Weight;
        private double Value;
        private enum Rarities { R, SR, SSR }; // R for Rare, SR for Super Rare, SSR for Super Super Rare

        private Dictionary<Item, string> ItemList = new Dictionary<Item, string>()
        {
            {new Item("Sword of Destiny", "A legendary sword with immense power.", 'W', 15.0, 10.0), "R"},
            {new Item("Shield of Valor", "A sturdy shield that can withstand powerful attacks.", 'A', 20.0, 10.0), "R"},
            {new Item("Health Potion", "A potion that restores health.", 'P', 0.5, 5.0), "R"}
        };

        public Item(string name, string description, char itemType, double weight, double value): base(name, 'I', description)
        {
            ItemType = itemType;
            Weight = weight;
            Value = value;    
        }

        public Item MakeLoot()
        {
            Random rand = new Random();
            int index = rand.Next(0,2);
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

        public override string ToString()
        {
            return $@"{base.ToString()} 
            - 
            - Type: {ItemType}, Weight: {Weight}, Value: {Value}, Rarity: ";
        }
    }
}