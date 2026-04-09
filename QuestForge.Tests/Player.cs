namespace QuestForge.Tests;

public class Player
{
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

    public
}
