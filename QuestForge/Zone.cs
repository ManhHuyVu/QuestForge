using System;

namespace QuestForge
{
    public class Zone
    {
        public string Name {get; private set;}
        public string Description{get; private set;}
        public char Difficulty {get; private set;} // S for Safe, E for Easy, H for Hard, B for Boss\
        public Stack<GameEvent> Events {get; private set;} = new();

        public Zone(string name, string description, char difficulty)
        {
            Name = name;
            Description = description;
            Difficulty = difficulty;
        }   

        public override string ToString()
        {
            return $@"{Name} 
            - {Description} 
            - Difficulty: {Difficulty}";
        }       
    }
}