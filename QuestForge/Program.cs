using System;
using System.Diagnostics.Tracing;
using QuestForge;

public class Program
{
    static GameMaster gm = new GameMaster();

    static QuestForge.Player Hero = new QuestForge.Player("Hero", "The brave hero of the story.");

    static QuestForge.Enemy Goblin = new QuestForge.EnemyMaster().CreateEnemy(QuestForge.EnemyTypes.Easy, "Goblin", "A sneaky goblin lurking in the shadows.");

    static QuestForge.EventContext BattleForest = new QuestForge.EventContext();

    static QuestForge.ZoneManager zm = new QuestForge.ZoneManager();

    static QuestForge.CombatManager cm = new QuestForge.CombatManager();

    static QuestForge.ItemMaster im = new QuestForge.ItemMaster();
    public static void Init()
    {
        // Initialize game entities, such as players, enemies, and items
        BattleForest.Player = Hero;
        BattleForest.Enemy = Goblin;
        BattleForest.ItemMaster = im;
        BattleForest.CombatManager = cm;
        CombatEvent Gob_Am = new CombatEvent("Goblin Ambush");
        DialougeEvent StartBattle = new DialougeEvent("Goblin", "You won't get past me, Hero!");
        LootEvent Gob_Loot = new LootEvent("Goblin Loot", "R");
        QuestForge.Zone Forest = new QuestForge.Zone("Forest", "A dense and mysterious forest filled with unknown dangers.", 'E');
        zm.AddZone(Forest, null, null);
        zm.PushEvent(Forest, StartBattle);
        zm.PushEvent(Forest, Gob_Am);
        zm.PushEvent(Forest, Gob_Loot);
        gm.Register(Hero);
        gm.Register(Goblin);
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
            Stack<GameEvent> eventStack = new Stack<GameEvent>(Hero.CurrentZone.Events);
            while (eventStack.Count > 0)
            {
                GameEvent currentEvent = eventStack.Pop();
                if(BattleForest.CombatResult != "Player" && BattleForest.CombatResult != null)
                {
                    if(currentEvent is LootEvent)
                    {
                        continue; // Skip loot events if the player didn't win the combat
                    }
                }
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
}

public class GameMaster
{
    static List<Dictionary<int, GameEntity>> RegisteredEntities = new List<Dictionary<int, GameEntity>>();
    public int Register(GameEntity entity)
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

    public bool Unregister(int id)
    {
        if (id < 0 || id >= RegisteredEntities.Count)
        {
            return false;
        }
        RegisteredEntities.RemoveAt(id);
        return true;
    }


}
