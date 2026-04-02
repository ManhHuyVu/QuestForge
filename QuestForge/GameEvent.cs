using System;
using System.Runtime.InteropServices;

namespace QuestForge
{  
    public class EventContext// Context class to hold relevant information for events
    {
        public Player Player { get; set; }
        public Zone Zone { get; set; }
        public Enemy Enemy { get; set; }
        public Item Item { get; set; }
    }
    public abstract class GameEvent
    {
        public string Name { get; set; }

        public GameEvent(string name)
        {
            Name = name;
        }

        public abstract void Execute(EventContext context);
    }

    public class CombatEvent : GameEvent
    {
        public CombatEvent(string Combat) : base(Combat){}
        
        public override void Execute(EventContext context)
        {
            // Implement combat logic here, using context.Player, context.Enemy, etc.
            Console.WriteLine($"{context.Player.Name} engages in combat with {context.Enemy.Name}!");
        }
    }

    public class DialougeEvent : GameEvent
    {
        public string Dialogue { get; set; }

        public DialougeEvent(string name, string dialogue) : base(name)
        {
            Dialogue = dialogue;
        }

        public override void Execute(EventContext context)
        {
            if (context.Player != null)
            {
                Console.WriteLine($"{context.Player.Name} says: '{Dialogue}'");
            }
            else
            {
                Console.WriteLine($"{context.Enemy.Name} says: '{Dialogue}'");
            }
            
        }
    }

    public class lootEvent : GameEvent
    {

        public lootEvent(string name) : base(name){}

        public override void Execute(EventContext context)
        {
            context.Player.AddItemToInventory(context.Item.MakeLoot(), 1);
            Console.WriteLine($"{context.Player.Name} loots {context.Item.Name}!");
        }
    }
}