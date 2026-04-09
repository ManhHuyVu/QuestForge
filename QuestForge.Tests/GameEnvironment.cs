using System.Net.ServerSentEvents;
using System.Reflection;
using QuestForge;
using Xunit;

namespace QuestForge.Tests;

public class ZoneTests
{
    public ZoneManager zoneMaster = new ZoneManager();

    [Fact]
    public void ZoneManager_AddZoneInEmptyLinkedList()
    {
        // Given
        Zone Town = new QuestForge.Zone("Town", "A peaceful town with friendly inhabitants.", 'S');
    
        // When
        zoneMaster.AddZone(Town, null, null);
    
        // Then
        Assert.Equal(Town, zoneMaster.zones.First.Value);
    }

    [Fact]
    public void ZoneManager_AddZoneInTheBeginOfLinkedList()
    {
        // Given
        Zone Town = new QuestForge.Zone("Town", "A peaceful town with friendly inhabitants.", 'S');

        Zone Forest = new QuestForge.Zone("Forest", "A dense forest filled with dangerous creatures.", 'E');
    
        // When
        zoneMaster.AddZone(Forest, null, null);
        zoneMaster.AddZone(Town, null, Forest);
    
        // Then
        Assert.Equal(Town, zoneMaster.zones.First.Value);
    }

    [Fact]
    public void ZoneManager_AddZoneInTheEndOfLinkedList()
    {
        // Given
        Zone Town = new QuestForge.Zone("Town", "A peaceful town with friendly inhabitants.", 'S');

        Zone Forest = new QuestForge.Zone("Forest", "A dense forest filled with dangerous creatures.", 'E');
    
        // When
        zoneMaster.AddZone(Town, null, null);
        zoneMaster.AddZone(Forest, Town, null);
    
        // Then
        Assert.Equal(Forest, zoneMaster.zones.Last.Value);
    }

    [Fact]
    public void ZoneManager_AddZoneInTheMiddleOfLinkedList()
    {
        // Given
        Zone Town = new QuestForge.Zone("Town", "A peaceful town with friendly inhabitants.", 'S');

        Zone Forest = new QuestForge.Zone("Forest", "A dense forest filled with dangerous creatures.", 'E');

        Zone Castle = new QuestForge.Zone("Castle", "An ancient castle rumored to be haunted.", 'M');
    
        // When
        zoneMaster.AddZone(Town, null, null);
        zoneMaster.AddZone(Castle, Town, null);
        zoneMaster.AddZone(Forest, Town, Castle);
    
        // Then
        Assert.Equal(Forest, zoneMaster.zones.First.Next.Value);
    }

    [Fact]
    public void ZoneManager_PushEventIntoAvailableZone_ReturnTrue()
    {
        // Given
        Zone Town = new QuestForge.Zone("Town", "A peaceful town with friendly inhabitants.", 'S');
        zoneMaster.AddZone(Town, null, null);

        // When
        Assert.True(zoneMaster.PushEvent(Town, new QuestForge.DialougeEvent("A stranger arrives in town.", "A mysterious figure has entered the town square.")));

        // Then
        Assert.Single(zoneMaster.zones.First.Value.Events);
    }

    [Fact]
    public void ZoneManager_PushEventIntoUnavailableZone_ReturnFalse()
    {
        // Given
        Zone Town = new QuestForge.Zone("Town", "A peaceful town with friendly inhabitants.", 'S');

        // When
        var result = zoneMaster.PushEvent(Town, new QuestForge.DialougeEvent("A stranger arrives in town.", "A mysterious figure has entered the town square."));

        // Then
        Assert.False(result);
    }

    [Fact]
    public void ZoneManager_PopNextAvaliableEvent_ReturnsCorrectEvent()
    {
        // Given
        Zone Town = new QuestForge.Zone("Town", "A peaceful town with friendly inhabitants.", 'S');
        zoneMaster.AddZone(Town, null, null);
        zoneMaster.PushEvent(Town, new QuestForge.DialougeEvent("A stranger arrives in town.", "A mysterious figure has entered the town square."));

        // When
        QuestForge.GameEvent? nextEvent = zoneMaster.PopNextEvent(Town);
        // Then
        Assert.IsAssignableFrom<QuestForge.GameEvent>(nextEvent);
    }

    [Fact]
    public void ZoneManager_PopNextUnavailableEvent_ReturnsNull()
    {
        // Given
        Zone Town = new QuestForge.Zone("Town", "A peaceful town with friendly inhabitants.", 'S');

        // When
        QuestForge.GameEvent? nextEvent = zoneMaster.PopNextEvent(Town);

        // Then
        Assert.Null(nextEvent);

        //Given
        zoneMaster.AddZone(Town, null, null);

        // When
        nextEvent = zoneMaster.PopNextEvent(Town);

        // Then
        Assert.Null(nextEvent);
    }

    [Fact]
    public void ZoneManager_GetAvailiableZoneByName_ReturnZone()
    {
        // Given
        Zone Town = new QuestForge.Zone("Town", "A peaceful town with friendly inhabitants.", 'S');
        zoneMaster.AddZone(Town, null, null);

        // When
        Zone? foundZone = zoneMaster.GetZoneByName("Town");    
        // Then
        Assert.Equal(Town, foundZone);
    }

    [Fact]
    public void ZoneManager_GetUnavailableZoneByName_ReturnsNull()
    {
        // Given
        Zone Town = new QuestForge.Zone("Town", "A peaceful town with friendly inhabitants.", 'S');
        zoneMaster.AddZone(Town, null, null);

        // When
        Zone? foundZone = zoneMaster.GetZoneByName("Forest");

        // Then
        Assert.Null(foundZone);
    }

    public void ZoneManager_DisplayZones_ReturnsCorrectString()
    {
        // Given
        Zone Town = new QuestForge.Zone("Town", "A peaceful town with friendly inhabitants.", 'S');
        Zone Forest = new QuestForge.Zone("Forest", "A dense forest filled with dangerous creatures.", 'E');
        zoneMaster.AddZone(Town, null, null);
        zoneMaster.AddZone(Forest, Town, null);

        // When
        string display = zoneMaster.DisplayZones();

        // Then
        Assert.Contains("Town (S)", display);
        Assert.Contains("Forest (E)", display);
    }

    [Fact]
    public void ZoneToString_ReturnsCorrectString()
    {
        // Given
        Zone Town = new QuestForge.Zone("Town", "A peaceful town with friendly inhabitants.", 'S');

        // When
        string zoneString = Town.ToString();

        // Then
        Assert.Contains("Town", zoneString);
        Assert.Contains("A peaceful town with friendly inhabitants.", zoneString);
        Assert.Contains("S", zoneString);
    }
}

public class GameEventTests
{
    public EventContext context = new EventContext();

    [Fact]
    public void DialougeEvent_Execute_PrintsCorrectDialogue()
    {
        // Given
        QuestForge.Player player = new QuestForge.Player("Hero", "A brave adventurer.");
        context.Player = player;
        DialougeEvent dialogueEvent = new DialougeEvent("Greeting", "Welcome to the town!");

        // When
        dialogueEvent.Execute(context);
    }

    [Fact]
    public void CombatEvent_Execute_PrintsCombatResult()
    {
        // Given
        QuestForge.Player player = new QuestForge.Player("Hero", "A brave adventurer.");
        QuestForge.Enemy enemy = new QuestForge.Enemy("Goblin", "A sneaky goblin.", 30, 5);
        CombatManager combatManager = new CombatManager();
        context.Player = player;
        context.Enemy = enemy;
        context.CombatManager = combatManager;
        CombatEvent combatEvent = new CombatEvent("Combat");

        // When
        combatEvent.Execute(context);

        // Then
        Assert.NotNull(context.CombatResult);
    }

    [Fact]
    public void LootEvent_Execute_PutItemInInventory()
    {
        // Given
        QuestForge.Player player = new QuestForge.Player("Hero", "A brave adventurer.");
        context.Player = player;
        QuestForge.LootEvent lootEvent = new QuestForge.LootEvent("Loot", "SR");

        // When
        lootEvent.Execute(context);
        Dictionary<QuestForge.Item, int> itemInIventory = context.Player.FindItemByName(context.ItemReturned.Name);

        // Then
        Assert.Single(itemInIventory);
    }
}