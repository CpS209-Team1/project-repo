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

        }

        /// <summary>
        /// Calculates the enemy's attack to see if attack was successfull
        /// </summary>
        public void ComputeEnemyAttack()
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
