using System;

namespace QuestForge
{
    public enum CombatActionType // Enum to represent different types of combat actions
    {
        Attack,
        Defend,
        Flee,
        RoundOver
    }   

    public class CombatAction // Class to represent a combat action taken by either the player or the enemy
    {
        public GameEntity Source { get; set; }

        public CombatActionType ActionType { get; set; }

        public double Value { get; set; } // Damage for Attack, Defense for Defend, SuccessRate for Flee

        public CombatAction(CombatActionType actionType, double value, GameEntity source)
        {
            Source = source;
            ActionType = actionType;
            Value = value;
        }
    }

    public class CombatManager
    {
        public Queue<CombatAction> CombatQueue { get; private set; }

        public CombatManager()
        {
            CombatQueue = new Queue<CombatAction>();
        }

        public void ChooseAction(Player source)
        {
            Console.WriteLine("Choose an action:");
            Console.WriteLine("1. Attack");
            Console.WriteLine("2. Defend");
            Console.WriteLine("3. Flee");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    CombatQueue.Enqueue(new CombatAction(CombatActionType.Attack, source.Attack, source));
                    break;
                case "2":
                    CombatQueue.Enqueue(new CombatAction(CombatActionType.Defend, 30, source));
                    break;
                case "3":
                    Console.WriteLine("Enter flee success rate (0-1):");
                    double successRate = double.Parse(Console.ReadLine());
                    CombatQueue.Enqueue(new CombatAction(CombatActionType.Flee, successRate, source));
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    ChooseAction(source);
                    break;
            }
            Console.WriteLine();
        }

        public void EnemyRandomAction(Enemy enemy)
        {
            Random rand = new Random();
            int action = rand.Next(1, 4); // Randomly choose between 1, 2, or 3
            switch (action)
            {
                case 1:
                    CombatQueue.Enqueue(new CombatAction(CombatActionType.Attack, enemy.Attack, enemy));
                    Console.WriteLine("Enemy chooses to attack!");
                    break;
                case 2:
                    CombatQueue.Enqueue(new CombatAction(CombatActionType.Defend, 10, enemy));
                    Console.WriteLine("Enemy chooses to defend!");
                    break;
                case 3:
                    double successRate = rand.NextDouble(); // Random success rate for fleeing
                    CombatQueue.Enqueue(new CombatAction(CombatActionType.Flee,successRate, enemy));
                    Console.WriteLine("Enemy chooses to flee!");
                    break;
            }
            Console.WriteLine();
        }
        
        public void QueueCombatAction(Player player, Enemy enemy)
        {
            for(int i = 0; i < 2; i++)
            {
                ChooseAction(player);
                EnemyRandomAction(enemy); 
            }
        }

        public string PlayCombatRound(Player player, Enemy enemy)
        {
            bool GameEnd = false;
            string Result = "";
            while (!GameEnd)
            {
                QueueCombatAction(player, enemy); // Queue the action for the round
                while (CombatQueue.Count > 0 && Result == "")
                {
                    CombatAction action = CombatQueue.Dequeue();
                    if (action.ActionType == CombatActionType.Attack)
                    {
                        if (action.Source == player)
                        {
                            GameEnd = enemy.TakeDamage(action.Value);
                            Console.WriteLine($"Player attacks for {action.Value} damage!");
                            Console.WriteLine($"Enemy health is now {enemy.Health}");
                        }
                        else
                        {
                            GameEnd = player.TakeDamage(action.Value);
                            Console.WriteLine($"Enemy attacks for {action.Value} damage!");
                            Console.WriteLine($"Player health is now {player.Health}");
                        }
                        if (GameEnd)
                        {
                            Result = action.Source == player ? "Player" : "Enemy";
                            Console.WriteLine(Result);
                        }
                    }
                    else if (action.ActionType == CombatActionType.Defend)
                    {
                        if (action.Source == player)
                        {
                            player.Defense += action.Value;
                            Console.WriteLine($"Player defends with {action.Value} defense!");
                        }
                        else
                        {
                            enemy.Defense += action.Value;
                            Console.WriteLine($"Enemy defends with {action.Value} defense!");
                        }
                    }
                    else if (action.ActionType == CombatActionType.Flee)
                    {
                        if (action.Source == player)
                        {
                            if (action.Value > 0.5) // Arbitrary success threshold
                            {
                                Console.WriteLine("Player successfully flees!");
                                GameEnd = true;
                                Result = "Draw";
                            }
                            else
                            {
                                Console.WriteLine("Player failed to flee!");
                            }
                        }
                        else
                        {
                            if (action.Value > 0.5) // Arbitrary success threshold
                            {
                                Console.WriteLine("Enemy successfully flees!");
                                GameEnd = true;
                                Result = "Draw";
                            }
                            else
                            {
                                Console.WriteLine("Enemy failed to flee!");
                            }
                        }
                    }
                }
            } 
            return Result;
        }
    }
}