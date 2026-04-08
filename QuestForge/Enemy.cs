using System;
using System.ComponentModel;

namespace QuestForge
{
    public enum EnemyTypes { Easy, Hard, Boss }
    public class EnemyMaster
    {
        public Enemy CreateEnemy(EnemyTypes type, string name, string description) // Make enemy based on type, with name and description provided by user
        {
            switch (type)
            {
                case EnemyTypes.Easy:
                    return new Enemy(name, description, 50.0, 10.0);
                case EnemyTypes.Hard:
                    return new Enemy(name, description, 100.0, 20.0);
                case EnemyTypes.Boss:
                    return new Enemy(name, description, 200.0, 30.0);
                default:
                    throw new ArgumentException("Invalid enemy type");
            }
        }
    }

    public class Enemy : GameEntity
    {   
        public double Health { get; private set; } // Health point
        public double Attack {get; private set;} // Attack point
        public double Defense {get; set;} // Defense point
        
        public Enemy(string name, string description, double health, double attack) : base(name, 'E', description)
        {
            Health = health;
            Attack = attack;
            Defense = 0;
        }

        public bool TakeDamage(double damage)
        {
            Health -= Math.Max(0, damage - Defense); // Calculate damage after defense
            if (Health <= 0)
            {
                return true; // Enemy is defeated, == GameEnd
            }
            return false
            ;
        }

        public override string ToString()
        {
            return $@"{base.ToString()}
            - Character: Enemy
            - Health: {Health}, Attack: {Attack}, Defense: {Defense}";
        }
    }
}