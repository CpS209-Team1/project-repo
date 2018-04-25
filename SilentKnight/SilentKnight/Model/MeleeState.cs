using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// This file contains part of a state machine
/// </summary>
namespace Model
{
    /// <summary>
    /// This class computes player melee attacks
    /// </summary>
    class MeleeState : IState
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public MeleeState() { }

        /// <summary>
        /// Calculates the player's attack to see whether or not it was successful
        /// </summary>
        public void Update()
        {
            Random rand = new Random();
            foreach (Enemy i in World.Instance.Entities)
            {
                int randNum = rand.Next(0, 10);
                double enemyDistance = Math.Sqrt(Math.Pow((Player.Instance.PlayerLoc.X) - (i.EnemyLoc.X + i.Center), 2) + Math.Pow(Player.Instance.PlayerLoc.Y - (i.EnemyLoc.Y + i.Center), 2));
                if (enemyDistance < i.Center + 50 && World.Instance.CheatMode == false)
                {
                    if (Player.Instance.PlayerDirection == Direction.Left && i.EnemyLoc.X + i.Center < Player.Instance.PlayerLoc.X)
                    {
                        i.RemoveEnemyHealth(2);
                        EnemyMove.Instance.Hit(i);
                    }
                    else if (Player.Instance.PlayerDirection == Direction.Right && i.EnemyLoc.X +i.Center > Player.Instance.PlayerLoc.X)
                    {
                        i.RemoveEnemyHealth(2);
                        EnemyMove.Instance.Hit(i);
                    }
                    else if (Player.Instance.PlayerDirection == Direction.Up && i.EnemyLoc.Y + i.Center < Player.Instance.PlayerLoc.Y)
                    {

                        i.RemoveEnemyHealth(2);
                        EnemyMove.Instance.Hit(i);
                    }
                    else if (Player.Instance.PlayerDirection == Direction.Down && i.EnemyLoc.Y + i.Center > Player.Instance.PlayerLoc.Y)
                    {
                        i.RemoveEnemyHealth(2);
                        EnemyMove.Instance.Hit(i);
                    }

                }
                else if (enemyDistance < 100 && World.Instance.CheatMode == true)
                {
                    i.RemoveEnemyHealth(i.Health);
                    EnemyMove.Instance.Hit(i);
                }
                if (i.Health <= 0)
                {
                    World.Instance.DeadEnemy.Add(i);// Once an enemy is removed from the Entities list, it is added to DeadEnemy for removal from the canvas
                    Player.Instance.PlayerScore += 1;
                    if (randNum == 7)
                    {
                        randNum = rand.Next(1, 5);
                        if (randNum + Player.Instance.Health <= 20)
                        {
                            Player.Instance.Health += randNum;
                        }
                    }
                }
            }

            foreach (Enemy i in World.Instance.DeadEnemy)
            {
                i.KillEnemy();
            }
        }

        /// <summary>
        /// Handles new input from the user and decides what to do with it
        /// </summary>
        /// <param name="data"></param>
        public void HandleInput(string data)
        {
            if(data == "melee" && Player.Instance.PlayerCoolDown == 0)
            {
                Update();
            }
            else if (data == "ranged" && Player.Instance.PlayerCoolDown == 0)
            {
                Console.WriteLine(Player.Instance.PlayerCoolDown);
                Player.Instance.PlayerState.Change("ranged");
            }
        }
        
        /// <summary>
        /// This has no function just needs to be in here since it is inherited from IState
        /// </summary>
        public void Change()
        {

        }

        /// <summary>
        /// This has no function just needs to be in here since it is inherited from IState
        /// </summary>
        public void Exit()
        {

        }

    }
}
