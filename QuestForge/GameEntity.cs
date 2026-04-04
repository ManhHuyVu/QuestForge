using System;

namespace QuestForge
{
    public abstract class GameEntity // use for Player, Enemy, and Item
    {
        public virtual string Name {get; set;}
        private char typee;
        public char Type // 'P' for Player, 'E' for Enemy, 'I' for Item
        {
            get { return typee; }
            set
            {
                if (value != 'P' && value != 'E' && value != 'I')
                {
                    throw new ArgumentException("Type must be 'P' for Player, 'E' for Enemy, or 'I' for items.");
                }
                typee = value;
            }
        }

        private string descriptionAndCatchphrase; 
        public string DescriptionAndCatchphrase // Catchphrase for player and enemy, description for item
        {
            get;
            set {if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Description and catchphrase cannot be empty.");
                }
                descriptionAndCatchphrase = value;
            }
        }

        public GameEntity(string name, char type, string descirptionAndCatchphrased) // Constructor for GameEntity, which will be called by Player, Enemy, and Item constructors
        {
            Name = name;
            Type = type;
            descriptionAndCatchphrase = descirptionAndCatchphrased;
        }

        public override string ToString()
        {
            return $"{Name} - {descriptionAndCatchphrase}";
        }
    }
}