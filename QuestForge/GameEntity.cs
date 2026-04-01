using System;

namespace QuestForge
{
    public abstract class GameEntity
    {
        public virtual string Name {get; set;}

        private char typee;
        public char Type
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

        public string DescriptionAndCatchphrase
        {
            get;
            set {if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Description and catchphrase cannot be empty.");
                }
                DescriptionAndCatchphrase = value;
            }
        }

        public GameEntity(string name, char type, string descirptionAndCatchphrase)
        {
            Name = name;
            Type = type;
            DescriptionAndCatchphrase = descirptionAndCatchphrase;
        }

        public override string ToString()
        {
            return $"{Name} - {DescriptionAndCatchphrase}";
        }
    }
}