using System;

namespace QuestForge
{
    public abstract class GameEntity
    {
        public string Name { get; set; }

        private char type;
        public char Type
        {
            get { return type; }
            set
            {
                if (value != 'P' && value != 'E' && value != 'I')
                {
                    throw new ArgumentException("Type must be 'P' for Player, 'E' for Enemy, or 'I' for items.");
                }
                type = value;
            }
        }

        public GameEntity(string name, char type, string descirptionAndCatchphrase)
        {
            Name = name;
        }
    }
}