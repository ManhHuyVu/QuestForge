using System;

namespace QuestForge
{
    public class Player
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }

        private List<(string, int)> inventory = new List<(string, int)>();

        public Player(string name)
        {
            Name = name;
            Level = 1;
            Experience = 0;
        }
    }
}