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
