using System;

namespace QuestForge
{
    public class Item: GameEntity
    {
        private char ItemType; // 'W' for weapon, 'A' for armor, 'P' for potions, 'Q' for quest objects
        private double Weight;
        private double Value;
        private enum Rarities; // R for Rare, SR for Super Rare, SSR for Super Super Rare

        public Item(string name, string description, char itemType, double weight, double value): base(name, 'I', description)
        {
            ItemType = itemType;
            Weight = weight;
            Value = value;
        }

        public void MakeLoot()
        {
            
        }

        public override string ToString()
        {
            return $@"{base.ToString()} 
            - Type: {ItemType}, Weight: {Weight}, Value: {Value}, Rarity: ";
        }
    }
}