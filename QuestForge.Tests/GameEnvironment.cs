using System.Net.ServerSentEvents;
using System.Reflection;
using QuestForge;
using Xunit;

namespace QuestForge.Tests;

public class GameEnvironmentTests
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

}