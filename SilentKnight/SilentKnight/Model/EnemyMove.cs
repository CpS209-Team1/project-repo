using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// This class is used to control the enemy's movement AI
    /// </summary>
    class EnemyMove
    {
        public double Timer { get; set; }//Used as a "delay" (No need to save)
        private EnemyMove()
        {
            Timer = 0;
        }

        /// <summary>
        ///This method takes `enemy` and removes .25 from its X value
        /// </summary>
        /// <param name="enemy"></param>
        public void MoveLeft(Enemy enemy)
        {
            enemy.EnemyLoc.X -= .25;
            Timer += 1;
        }

        /// <summary>
        /// This method takes `enemy` and adds .25 to its X value
        /// </summary>
        /// <param name="enemy"></param>
        public void MoveRight(Enemy enemy)
        {
            enemy.EnemyLoc.X += .25;
            Timer += 1;
        }

        /// <summary>
        /// This method takes `enemy` and removes .25 from its Y value
        /// </summary>
        /// <param name="enemy"></param>
        public void MoveUp(Enemy enemy)
        {
            enemy.EnemyLoc.Y -= .25;
            Timer += 1;
        }

        /// <summary>
        /// This method takes `enemy` and adds .25 to its Y value
        /// </summary>
        /// <param name="enemy"></param>
        public void MoveDown(Enemy enemy)
        {
            enemy.EnemyLoc.Y += .25;
            Timer += 1;
        }

        /// <summary>
        /// This method is used to tell the enemy where the player is when tracking
        /// </summary>
        /// <param name="enemy"></param>
        public void Track(Enemy enemy)
        {
            if (Player.Instance.PlayerLoc.X < enemy.EnemyLoc.X)
            {
                enemy.EnemyLoc.X -= .5;
            }
            else if (Player.Instance.PlayerLoc.X > enemy.EnemyLoc.X)
            {
                enemy.EnemyLoc.X += .5;
            }
            if (Player.Instance.PlayerLoc.Y < enemy.EnemyLoc.Y)
            {
                enemy.EnemyLoc.Y -= .5;
            }
            else if (Player.Instance.PlayerLoc.Y > enemy.EnemyLoc.Y)
            {
                enemy.EnemyLoc.Y += .5;
            }
        }

        /// <summary>
        /// This method is used to knock back enemy's when they are hit.
        /// The method uses switch to test for Direction. The method then knocks the enemy in the appropriate direction according to Direction
        /// </summary>
        /// <param name="enemy"></param>
        public void Hit(Enemy enemy)
        {
            switch (Player.Instance.PlayerDirection)
            {
                case Direction.Down:
                    if (enemy.EnemyLoc.Y + 50 < World.Instance.borderBottom)
                    {
                        enemy.EnemyLoc.Y += 50;
                    }
                    else
                    {
                        enemy.EnemyLoc.Y += World.Instance.borderBottom - enemy.EnemyLoc.Y;
                    }
                    break;
                case Direction.Up:
                    if (enemy.EnemyLoc.Y - 50 > 0)
                    {
                        enemy.EnemyLoc.Y -= 50;
                    }
                    else
                    {
                        enemy.EnemyLoc.Y -= enemy.EnemyLoc.Y;
                    }
                    break;
                case Direction.Left:
                    if (enemy.EnemyLoc.X - 50 > 0)
                    {
                        enemy.EnemyLoc.X -= 50;
                    }
                    else
                    {
                        enemy.EnemyLoc.X -= enemy.EnemyLoc.X;
                    }
                    break;
                case Direction.Right:
                    if (enemy.EnemyLoc.X + 50 < World.Instance.borderRight)
                    {
                        enemy.EnemyLoc.X += 50;
                    }
                    else
                    {
                        enemy.EnemyLoc.X += World.Instance.borderRight - enemy.EnemyLoc.X;
                    }

                    break;
            }
        }

        /// <summary>
        /// This method causes enemy's to pause
        /// </summary>
        public void Stand()
        {
            Timer += .1;
        }

        public void Teleport(Enemy enemy)
        {
            if (enemy.EnemyLoc.X > World.Instance.borderRight)
            {
                enemy.EnemyLoc.X = World.Instance.borderRight - 150;
            }
            if (enemy.EnemyLoc.Y > World.Instance.borderBottom)
            {
                enemy.EnemyLoc.Y = World.Instance.borderBottom - 150;
            }
        }

        private static EnemyMove instance = new EnemyMove();
        public static EnemyMove Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
