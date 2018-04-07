using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    class GameController
    {
        /// <summary>
        /// Updates the player's location
        /// 
        /// Takes the canvas x and y pos and sets it to the player pos
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void ComputePlayerMove(double x, double y)
        {
            Player.Instance.PlayerLoc.X = x;
            Player.Instance.PlayerLoc.Y = y;
        }

        /// <summary>
        /// Calls Enemy's `DoMove` method
        /// </summary>
        public void MoveEnemies()
        {

        }

        /// <summary>
        /// Calculates the player's attack to see if attack was successfull
        /// </summary>
        public void ComputePlayerAttack()
        {
            foreach (Enemy i in World.Instance.Entities)
            {
                if (Math.Sqrt(Math.Pow(Player.Instance.PlayerLoc.X - i.EnemyLoc.X, 2) + Math.Pow(Player.Instance.PlayerLoc.Y - i.EnemyLoc.Y, 2)) < 50)
                {
                    i.RemoveEnemyHealth(2);

                }
                if (i.Health <= 0)
                {
                    World.Instance.DeadEnemy.Add(i);
                }
            }
            
            foreach (Enemy i in World.Instance.DeadEnemy)
            {
                i.KillEnemy();
            }

        }

        /// <summary>
        /// Calculates the enemy's attack to see if attack was successful
        /// </summary>
        public void ComputeEnemyAttack()
        {

        }

        public void SpawnEnemies()
        {

        }
    }

    /// <summary>
    /// Contains enemy and player locations
    /// </summary>
    struct Location
    {
        public double X { get; set; }
        public double Y { get; set; }
    }
}
