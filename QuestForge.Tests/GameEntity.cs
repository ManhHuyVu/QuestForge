using System.Net.ServerSentEvents;
using System.Reflection;
using QuestForge;
using Xunit;

namespace QuestForge.Tests;

public class Player
{
    public QuestForge.Player otherTesting = new QuestForge.Player("Hero", "The brave hero of the story.");

    [Fact]
    public void PlayerToString()
    {
        // Given
    
        // When
        string playerString = otherTesting.ToString();
    
        // Then
        Assert.Contains("Hero", playerString);
        Assert.Contains("The brave hero of the story.", playerString);
    }

    [Fact]
    public void Player_InitWithBadName_ThrowException()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            QuestForge.Player Hero = new QuestForge.Player(null, "The brave hero of the story.");
        });
        Assert.Throws<ArgumentException>(() =>
        {
            QuestForge.Player Hero = new QuestForge.Player("   ", "The brave hero of the story.");
        });
    }

    [Fact]
    public void Player_InitWithGoodName_ReturnsValidPlayer()
    {
        // Given
        string validName = "Hero";
        string validDescription = "The brave hero of the story.";

        // When
        QuestForge.Player Hero = new QuestForge.Player(validName, validDescription);
    
        // Then
        Assert.Equal(validName, Hero.Name);
        Assert.Equal(validDescription, Hero.DescriptionAndCatchphrase);
        Assert.Equal(100.0, Hero.Health);
    }

    [Fact]
    public void Player_AddItemToInventory()
    {
        // Given
        QuestForge.Item itemObj = new QuestForge.ItemMaster().MakeLoot();
    
        // When
        otherTesting.AddItemToInventory(itemObj, 1);
    
        // Then
        Assert.Single(otherTesting.inventory);

        // When add the same item again
        otherTesting.AddItemToInventory(itemObj, 1);
    
        // Then
        Assert.Single(otherTesting.inventory);
        Assert.Equal(2, otherTesting.inventory[itemObj]);
    }

    [Fact]
    public void Player_RemoveItemFromInventory()
    {
        // Throw away more than own
        // Given
        QuestForge.Item itemObj = new QuestForge.ItemMaster().MakeLoot();
        otherTesting.AddItemToInventory(itemObj, 1);

        // When
        otherTesting.RemoveItemFromInventory(itemObj, 2);
    
        // Then
        Assert.Empty(otherTesting.inventory);

        // Remove part of the item
        // Given
        QuestForge.Item itemObj1 = new QuestForge.ItemMaster().MakeLoot();
        otherTesting.AddItemToInventory(itemObj1, 3);

        // When
        otherTesting.RemoveItemFromInventory(itemObj1, 2);
    
        // Then
        Assert.Single(otherTesting.inventory);

        // Remove item that is not in inventory
        // Given
        QuestForge.Item itemObj2 = new QuestForge.Item("Test Item", "A test item.", 'A', 10, 5);
        otherTesting.AddItemToInventory(itemObj2, 1);

        // When
        Assert.Throws<ArgumentException>(() =>
        {
            otherTesting.RemoveItemFromInventory(new QuestForge.Item("Not test Object", "Not it", 'A', 10, 5), 2);
        });
    }

    [Fact]
    public void Player_Revive_ReturnOriginalStat()
    {  
        //Given
        otherTesting.TakeDamage(90);
        otherTesting.AddItemToInventory(new QuestForge.ItemMaster().MakeLoot(), 1);
        Assert.Single(otherTesting.inventory);

        //When
        otherTesting.Revive();

        //Then
        Assert.Equal(100, otherTesting.Health);
        Assert.Empty(otherTesting.inventory);
    }

    [Fact]
    public void Player_TakeDamage_ReturnsTrueWhenDefeated()
    {
        // Given
        int damage = 110; // More damage than health

        // When
    
        // Then
        Assert.True(otherTesting.TakeDamage(damage)); // True because player is defeated
        otherTesting.Revive();
    }

    [Fact]
    public void Player_TakeDamage_ReturnsFalseWhenNotDefeated()
    {
        // Given
        int damage = 50; // Less damage than health
    
        // When
    
        // Then
        Assert.False(otherTesting.TakeDamage(damage)); // False because player is not defeated

    }

    [Fact]
    public void Player_FindItemByName_ReturnsCorrectItems()
    {
        // Given
        QuestForge.Item itemObj = new QuestForge.ItemMaster().MakeLoot();
        QuestForge.Item itemObj2 = new QuestForge.Item("Test Item", "A test item.", 'A', 10, 5);
        otherTesting.AddItemToInventory(itemObj, 2);
        otherTesting.AddItemToInventory(itemObj2, 1);

        // When
        Dictionary<QuestForge.Item, int> result = otherTesting.FindItemByName(itemObj.Name);

        // Then
        Assert.Single(result);
    }

    [Fact]
    public void Player_MovePlayer_ReturnsFalseWhenMoveIsInvalid()
    {
        // Given
        QuestForge.ZoneManager zm = new QuestForge.ZoneManager();

        // When
        bool moveResult = otherTesting.MovePlayer(zm, "NonExistentZone");
    
        // Then
        Assert.False(moveResult);
        Assert.Null(otherTesting.CurrentZone);
    }

    [Fact]
    public void Player_MovePlayer_ReturnsTrueWhenMoveIsValid()
    {
        // Given
        QuestForge.Zone Forest = new QuestForge.Zone("Forest", "A dense and mysterious forest filled with unknown dangers.", 'E');
        QuestForge.ZoneManager zm = new QuestForge.ZoneManager();
        zm.AddZone(Forest, null, null);

        // When
        bool moveResult = otherTesting.MovePlayer(zm, "Forest");
    
        // Then
        Assert.True(moveResult);
        Assert.Equal(Forest, otherTesting.CurrentZone);
    }
}

public class Enemy
{
    public QuestForge.EnemyMaster otherTesting = new QuestForge.EnemyMaster();

    [Fact]
    public void EnemyMaster_CreateEasyEnemy_ReturnsCorrectEnemy()
    {
        // Given
        QuestForge.EnemyTypes enemyType = QuestForge.EnemyTypes.Easy;
        string name = "Goblin";
        string description = "A small and mischievous creature.";

        // When
        QuestForge.Enemy enemy = otherTesting.CreateEnemy(enemyType, name, description);

        // Then
        Assert.Equal(50, enemy.Health);
        Assert.Equal(10, enemy.Attack);
    }

    [Fact]
    public void EnemyMaster_CreateHardEnemy_ReturnsCorrectEnemy()
    {
        // Given
        QuestForge.EnemyTypes enemyType = QuestForge.EnemyTypes.Hard;
        string name = "Orc";
        string description = "A large and brutish creature.";

        // When
        QuestForge.Enemy enemy = otherTesting.CreateEnemy(enemyType, name, description);

        // Then
        Assert.Equal(100, enemy.Health);
        Assert.Equal(20, enemy.Attack);
    }

    [Fact]
    public void EnemyMaster_CreateBossEnemy_ReturnsCorrectEnemy()
    {
        // Given
        QuestForge.EnemyTypes enemyType = QuestForge.EnemyTypes.Boss;
        string name = "Dragon";
        string description = "A powerful and fearsome creature.";

        // When
        QuestForge.Enemy enemy = otherTesting.CreateEnemy(enemyType, name, description);

        // Then
        Assert.Equal(200, enemy.Health);
        Assert.Equal(30, enemy.Attack);
    }

    [Fact]
    public void EnemyToString()
    {
        // Given
        QuestForge.Enemy enemy = otherTesting.CreateEnemy(QuestForge.EnemyTypes.Easy, "Goblin", "A small and mischievous creature.");

        // When
        string enemyString = enemy.ToString();
        // Then
        Assert.Contains("Goblin", enemyString);
        Assert.Contains("A small and mischievous creature.", enemyString);
    }
    
    [Fact]
    public void Enemy_TakeDamage_ReturnsTrueWhenDefeated()
    {
        // Given
        QuestForge.Enemy enemy = otherTesting.CreateEnemy(QuestForge.EnemyTypes.Easy, "Goblin", "A small and mischievous creature.");
        double damage = 60; // More damage than health

        // When
    
        // Then
        Assert.True(enemy.TakeDamage(damage));
    }

    [Fact]
    public void Enemy_TakeDamage_ReturnsFalseWhenNotDefeated()
    {
        // Given
        QuestForge.Enemy enemy = otherTesting.CreateEnemy(QuestForge.EnemyTypes.Easy, "Goblin", "A small and mischievous creature.");
        double damage = 30; // Less damage than health

        // When
    
        // Then
        Assert.False(enemy.TakeDamage(damage));
    }
}

public class Item
{
    public QuestForge.ItemMaster otherTesting = new QuestForge.ItemMaster();

    [Fact]
    public void ItemMaster_AddNewItemToItemList()
    {
        // Given
        QuestForge.Item item = new QuestForge.Item("Test Item", "A test item.", 'W', 1.0, 10.0);
        string rarity = "R";

        // When
        otherTesting.AddNewItem(item, rarity);

        // Then
        Assert.True(otherTesting.ItemList.ContainsKey(item));
        Assert.Equal(rarity, otherTesting.ItemList[item]);
    }

    [Fact]
    public void ItemMaster_MakeCorrectLoot_ReturnsValidItem()
    {
        // Given
        QuestForge.Item itemR = otherTesting.MakeLoot();
        otherTesting.currentRarity = "SR"; // Set rarity to Super Rare for testing
        QuestForge.Item itemSR = otherTesting.MakeLoot();
        otherTesting.currentRarity = "SSR"; // Set rarity to Super Super Rare for testing
        QuestForge.Item itemSSR = otherTesting.MakeLoot();
        // When
    
        // Then
        Assert.NotNull(itemR);
        Assert.False(string.IsNullOrWhiteSpace(itemR.Name));
        Assert.False(string.IsNullOrWhiteSpace(itemR.DescriptionAndCatchphrase));
        Assert.True(itemR.Value > 0);
        Assert.True(itemR.Weight > 0);
        Assert.Equal("R", otherTesting.ItemList[itemR]);
        Assert.Equal("SR", otherTesting.ItemList[itemSR]);
        Assert.Equal("SSR", otherTesting.ItemList[itemSSR]);
    }

    [Fact]
    public void ItemWeapon_IncreaseDamage_WhenEquipped()
    {
        // Given
        QuestForge.Player player = new QuestForge.Player("Hero", "The brave hero of the story.");
        QuestForge.ItemWeapon weapon = new QuestForge.ItemWeapon("Sword of Destiny", "A legendary sword with immense power.", 'W', 15.0, 10.0, 20);

        // When
        player.AddItemToInventory(weapon, 1);

        // Then
        Assert.Equal(40.0, player.Attack); // Player's attack should increase by weapon's attack bonus
    }

    [Fact]
    public void ItemArmor_IncreaseDefense_WhenEquipped()
    {
        // Given
        QuestForge.Player player = new QuestForge.Player("Hero", "The brave hero of the story.");
        QuestForge.ItemArmor armor = new QuestForge.ItemArmor("Plate Armor", "A set of heavy plate armor.", 'A', 20.0, 15.0, 10);

        // When
        player.AddItemToInventory(armor, 1);

        // Then
        Assert.Equal(10, player.Defense); // Player's defense should increase by armor's defense bonus
    }

    [Fact]
    public void ItemToString()
    {
        // Given
        QuestForge.Item item = new QuestForge.Item("Sample Item", "Do nothing", 'W', 1.0, 10.0);
        QuestForge.ItemWeapon weapon = new QuestForge.ItemWeapon("Sword of Destiny", "A legendary sword with immense power.", 'W', 15.0, 10.0, 20);
        QuestForge.ItemArmor armor = new QuestForge.ItemArmor("Plate Armor", "A set of heavy plate armor.", 'A', 20.0, 15.0, 10);
        QuestForge.ItemPotion potion = new QuestForge.ItemPotion("Health Potion", "A potion that restores health.", 'P', 0.5, 5.0, 50);

        // When
        string itemString = item.ToString();
        string weaponString = weapon.ToString();
        string armorString = armor.ToString();
        string potionString = potion.ToString();

        // Then
        Assert.Contains("Sample Item", itemString);
        Assert.Contains("Sword of Destiny", weaponString);
        Assert.Contains("Plate Armor", armorString);
        Assert.Contains("Health Potion", potionString);
    }
}