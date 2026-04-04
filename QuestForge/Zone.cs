using System;

namespace QuestForge
{
    public class Zone
    {
        public string Name {get; private set;}
        public string Description{get; private set;}
        public char Difficulty {get; private set;} // S for Safe, E for Easy, H for Hard, B for Boss\
        public List<GameEvent> Events {get; private set;} = new();

        public Zone(string name, string description, char difficulty, List<GameEvent> events)
        {
            Name = name;
            Description = description;
            Difficulty = difficulty;
            Events = events;
            Events.Reverse();
        }   

        public override string ToString()
        {
            return $@"{Name} 
            - {Description} 
            - Difficulty: {Difficulty}";
        }       
    }
}