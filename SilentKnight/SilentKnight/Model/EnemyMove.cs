using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    class EnemyMove
    {
        public double Timer { get; set; }
        private EnemyMove()
        {
            Timer = 0;
        }

        public void MoveLeft(Enemy enemy)
        {
            enemy.EnemyLoc.X -= .25;
            Timer += 1;
        }

        public void MoveRight(Enemy enemy)
        {
            enemy.EnemyLoc.X += .25;
            Timer += 1;
        }

        public void MoveUp(Enemy enemy)
        {
            enemy.EnemyLoc.Y -= .25;
            Timer += 1;
        }

        public void MoveDown(Enemy enemy)
        {
            enemy.EnemyLoc.Y += .25;
            Timer += 1;
        }

        public void Track(Enemy enemy)
        {
            if(Player.Instance.PlayerLoc.X < enemy.EnemyLoc.X)
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
                    if (enemy.EnemyLoc.X + 50 < World.Instance.borderBottom)
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

        public void Stand()
        {
            Timer += .1;
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
