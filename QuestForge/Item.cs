using System;

namespace QuestForge
{
    public class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double AttackBonus { get; set; } // Attack bonus provided by the item

        public Item(string name, string description, double attackBonus)
        {
            Name = name;
            Description = description;
            AttackBonus = attackBonus;
        }
    }
}