using System.Net.ServerSentEvents;
using System.Reflection;
using QuestForge;
namespace QuestForge.Tests;

public class Player
{
    public QuestForge.Player otherTesting = new QuestForge.Player("Hero", "The brave hero of the story.");

    [Fact]
    public void PlayerInitWithBadName_ThrowExemption()
    {
        Assert.Throws<InvalidNameException>(() =>
        {
            QuestForge.Player Hero = new QuestForge.Player(null, "The brave hero of the story.");
        });
        Assert.Throws<InvalidNameException>(() =>
        {
            QuestForge.Player Hero = new QuestForge.Player("   ", "The brave hero of the story.");
        });
    }

    [Fact]
    public void PlayerInitWithGoodName_ReturnsValidPlayer()
    {
        // Given
        string validName = "Hero";
        string validDescription = "The brave hero of the story.";

        // When
        QuestForge.Player Hero = new QuestForge.Player(validName, validDescription);
    
        // Then
        Assert.Equal(validName, Hero.Name);
        Assert.Equal(validDescription, Hero.Description);
        Assert.Equal(100.0, Hero.Health);
    }

    [Fact]
    public void PlayerAddItemInInventory()
    {
        // Given
        Item itemObj = new ItemMaster().MakeLoot();
    
        // When
        otherTesting.AddItemToInventory(itemObj, 1);
    
        // Then
        Assert.Equal(1, otherTesting.inventory.Count);

        // When add the same item again
        otherTesting.AddItemToInventory(itemObj, 1);
    
        // Then
        Assert.Equal(1, otherTesting.inventory.Count);
        Assert.Equal(2, otherTesting.inventory[itemObj]);
    }

    [Fact]
    public void PlayerRemoveItemInInventory()
    {
        // Throw away more than own
        // Given
        Item itemObj = new ItemMaster().MakeLoot();
        otherTesting.AddItemToInventory(itemObj, 1);

        // When
        otherTesting.RemoveItemFromInventory(itemObj, 2);
    
        // Then
        Assert.Equal(0, otherTesting.inventory.Count);

        // Remove part of the item
        // Given
        Item itemObj = new ItemMaster().MakeLoot();
        otherTesting.AddItemToInventory(itemObj, 3);

        // When
        otherTesting.RemoveItemFromInventory(itemObj, 2);
    
        // Then
        Assert.Equal(1, otherTesting.inventory.Count);

        // Remove item that is not in inventory
        // Given
        Item itemObj = new ItemMaster().MakeLoot();
        otherTesting.AddItemToInventory(itemObj, 1);

        // When
        Assert.Throws<InvalidObjectException>(() =>
        {
            otherTesting.RemoveItemFromInventory(new Item("Test Item", "A test item.", 10, 5), 2);
        });
    }

    [Fact]
    public void PlayerRevive_ReturnOriginalStat()
    {  
        //Given
        otherTesting.TakeDamage(90);
        otherTesting.AddItemToInventory(new ItemMaster.MakeLoot(), 1);
        Assert.Equal(otherTesting.inventory.Count, 1);

        //When
        otherTesting.Revive();

        //Then
        Assert.Equal(100, otherTesting.Health);
        Assert.Equal(otherTesting.inventory.Count, 0);
    }

    [Fact]
    public void PlayerTakeDamage_ReturnsTrueWhenDefeated()
    {
        // Given
        otherTesting.TakeDamage(110); // More damage than health

        // When
    
        // Then
        Assert.True(otherTesting.TakeDamage(110));
        otherTesting.Revive();
    }

    [Fact]
    public void PlayerFindItemByName_ReturnsCorrectItems()
    {
        // Given
        Item itemObj = new ItemMaster().MakeLoot();
        Item itemObj2 = new Item("Test Item", "A test item.", 10, 5);
        otherTesting.AddItemToInventory(itemObj, 2);
        otherTesting.AddItemToInventory(itemObj2, 1);

        // When
        var result = otherTesting.FindItemByName(itemObj.Name);

        // Then
        Assert.Equal(1, result[itemObj]);
    }

    [Fact]
    public void PlayerMovePlayer_ReturnsTrueWhenMoveIsValid()
    {
        // Given
        QuestForge.Zone Forest = new QuestForge.Zone("Forest", "A dense and mysterious forest filled with unknown dangers.", 'E');
        QuestForge.ZoneManager zm = new QuestForge.ZoneManager();
        zm.AddZone(Forest, null, null);

        // When
        otherTesting.CurrentZone = null; // Ensure player starts with no current zone
        bool moveResult = otherTesting.MovePlayer(zm, "Forest");
    
        // Then
        Assert.True(moveResult);
        Assert.Equal(Forest, otherTesting.CurrentZone);
    }

    public void PlayerMovePlayer_ReturnsFalseWhenMoveIsInvalid()
    {
        // Given
        QuestForge.ZoneManager zm = new QuestForge.ZoneManager();

        // When
        otherTesting.CurrentZone = null; // Ensure player starts with no current zone
        bool moveResult = otherTesting.MovePlayer(zm, "NonExistentZone");
    
        // Then
        Assert.False(moveResult);
        Assert.Null(otherTesting.CurrentZone);
    }
}

public class Enemy
{
    public QuestForge.Enemy otherTesting = new QuestForge.EnemyMaster().CreateEnemy(EnemyTypes.Easy, "Goblin", "A small and mischievous creature.");

    [Fact]
    public void ()
    {
        // Given
    
        // When
    
        // Then
    }
}
