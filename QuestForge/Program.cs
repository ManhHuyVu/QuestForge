using System;
using QuestForge;

public class Program
{
    static List<Dictionary<int, GameEntity>> RegisteredEntities = new List<Dictionary<int, GameEntity>>();

    static Stack<GameEvent> eventStack = new Stack<GameEvent>();

    static QuestForge.Player Hero = new QuestForge.Player("Hero", "The brave hero of the story.");

    static QuestForge.Enemy Goblin = new QuestForge.EnemyMaster().CreateEnemy(QuestForge.EnemyTypes.Easy, "Goblin", "A sneaky goblin lurking in the shadows.");

    static QuestForge.EventContext BattleForest = new QuestForge.EventContext();

    static QuestForge.ZoneManager zm = new QuestForge.ZoneManager();

    static QuestForge.ItemMaster im = new QuestForge.ItemMaster();
    public static void Init()
    {
        // Initialize game entities, such as players, enemies, and items
        BattleForest.Player = Hero;
        BattleForest.Enemy = Goblin;
        BattleForest.ItemMaster = im;
        CombatEvent Gob_Am = new CombatEvent("Goblin Ambush");
        DialougeEvent StartBattle = new DialougeEvent("Goblin", "You won't get past me, Hero!");
        lootEvent Gob_Loot = new lootEvent("Goblin Loot", "R");
        QuestForge.Zone Forest = new QuestForge.Zone("Forest", "A dense and mysterious forest filled with unknown dangers.", 'E');
        LoadEventStack(Forest.Events);
        zm.AddZone(Forest, null, null);
        zm.PushEvent(Forest, StartBattle);
        zm.PushEvent(Forest, Gob_Am);
        zm.PushEvent(Forest, Gob_Loot);
        Register(Hero);
        Register(Goblin);
        Console.WriteLine(Hero.ToString());
        Console.WriteLine(Goblin.ToString());
    }
    public static void Main(string[] args)
    {
        Console.WriteLine("Welcome to QuestForge!");
        Init();
        if (Hero.MovePlayer(zm, "Forest"))
        {
            Console.WriteLine($"{Hero.Name} enters the {Hero.CurrentZone.Name}.");
            BattleForest.Zone = Hero.CurrentZone;
            while (eventStack.Count > 0)
            {
                GameEvent currentEvent = eventStack.Pop();
                currentEvent.Execute(BattleForest);
            }
            Console.WriteLine(Hero.ToString());
        }
        else
        {
            Console.WriteLine("Failed to move to the zone.");
        }
        ; // Move player to the first zone (Forest)
    }

    public static int Register(GameEntity entity)
    {
        int index = RegisteredEntities.FindIndex(e => e.ContainsValue(entity));
        if (index != -1)
        {
            return index;
        }

        var newDict = new Dictionary<int, GameEntity>();
        newDict.Add(RegisteredEntities.Count, entity);
        RegisteredEntities.Add(newDict);
        return RegisteredEntities.Count - 1;
    }

    public static bool Unregister(int id)
    {
        if (id < 0 || id >= RegisteredEntities.Count)
        {
            return false;
        }
        RegisteredEntities.RemoveAt(id);
        return true;
    }

    public static void LoadEventStack(Stack<GameEvent> events)
    {
        foreach (var gameEvent in events)
        {
            eventStack.Push(gameEvent);
        }
    }
}