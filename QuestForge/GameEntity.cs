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

        private string descriptionAndCatchphrase;
        public string DescriptionAndCatchphrase
        {
            get;
            set {if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Description and catchphrase cannot be empty.");
                }
                descriptionAndCatchphrase = value;
            }
        }

        public GameEntity(string name, char type, string descirptionAndCatchphrased)
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