using System;

namespace QuestForge
{
    public class CombatAction
    {
        public GameEntity Source { get; set; }

        public CombatAction(GameEntity source)
        {
            Source = source;
        }
    }

    public class Attack: CombatAction
    {
        public double Damage { get; set; }
        public Attack(double damage, GameEntity source): base(source)
        {
            Damage = damage;
        }
    }

    public class Defend: CombatAction
    {
        public double Defense { get; set; }
        public Defend(double defense, GameEntity source): base(source)
        {
            Defense = defense;
        }
    }

    public class Flee: CombatAction
    {
        public double SuccessRate { get; set; }
        public Flee(double successRate, GameEntity source): base(source)
        {
            SuccessRate = successRate;
        }
    }

    public class CombatManager
    {
        public Queue<CombatAction> CombatQueue { get; private set; }

        
    }
}