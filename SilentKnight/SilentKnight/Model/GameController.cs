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
        public void ComputePlayerMove(double x, double y, string KeyPress)
        {
            Player.Instance.PlayerLoc.X = x;
            Player.Instance.PlayerLoc.Y = y;
            switch(KeyPress)//Keypress is used to determine which direction the player was looking in when he/she attacked
            {
                //Sets the player's direction to the appropriate direction
                case "S":
                    Player.Instance.PlayerDirection = Direction.Down;
                    break;
                case "A":
                    Player.Instance.PlayerDirection = Direction.Left;
                    break;
                case "W":
                    Player.Instance.PlayerDirection = Direction.Up;
                    break;
                case "D":
                    Player.Instance.PlayerDirection = Direction.Right;
                    break;
            }
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
                    EnemyMove.Instance.Hit(i);
                }
                if (i.Health <= 0)
                {
                    World.Instance.DeadEnemy.Add(i);// Once an enemy is removed from the Entities list, it is added to DeadEnemy for removal from the canvas
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
        public bool ComputeEnemyAttack()
        {
            foreach (Enemy i in World.Instance.Entities)
            {
                if (Math.Sqrt(Math.Pow(Player.Instance.PlayerLoc.X - i.EnemyLoc.X, 2) + Math.Pow(Player.Instance.PlayerLoc.Y - i.EnemyLoc.Y, 2)) < 25 && i.CoolDown == 0 && World.Instance.CheatMode == false)
                {
                    Player.Instance.RemovePlayerHealth(2);
                    Console.WriteLine(Player.Instance.Health);
                    i.CoolDown = 100;
                    return (true);
                }
                else
                {
                    if (i.CoolDown != 0)
                    {
                        i.CoolDown -= 1; //This method causes enemy's to have a 100 tick cooldown before being able to injure the player again
                    }
                    return (false);
                }
            }
            return (false);
        }

        public void Save()
        {

        }

        public void Load()
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
