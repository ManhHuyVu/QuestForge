using System;
using QuestForge;

public class Program
{
    static List<Dictionary<int, GameEntity>> RegisteredEntities = new List<Dictionary<int, GameEntity>>();

    public static void Init()
    {
        // Initialize game entities, such as players, enemies, and items
        QuestForge.Player player1 = new QuestForge.Player("Hero", "The brave hero of the story.");
        QuestForge.Item sword = new QuestForge.Item("Sword of Destiny", "A legendary sword with immense power.", 'W', 15.0, 10.0);
        QuestForge.Item shield = new QuestForge.Item("Shield of Valor", "A sturdy shield that can withstand powerful attacks.", 'A', 20.0, 10.0);
        QuestForge.Item potion = new QuestForge.Item("Health Potion", "A potion that restores health.", 'P', 0.5, 5.0);
        Console.WriteLine(player1);
    }
    public static void Main(string[] args)
    {
        Console.WriteLine("Welcome to QuestForge!");
        
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

}