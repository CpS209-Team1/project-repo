using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Model
{
    abstract class Enemy
    {
        public IEnemyObserver observer;
        public int Health { get; set; }
        public int Id { get; set; }
        public Location EnemyLoc;
        public string Image { get; set; }

        static int nextId;

       static Random rand = new Random();

        int choose = rand.Next(1, 6);

        public Enemy(IEnemyObserver observer, int x, int y)
        {
            this.observer = observer;
            Health = 10;
            EnemyLoc.X = x;
            EnemyLoc.Y = y;
            Image = "skeleton.png";
            Id = ++nextId;
        }

        public abstract string GetKind();

        public void UpdatePosition()
        {
           
            if (EnemyMove.Instance.Timer < 100)
            {
                if (choose == 1 && EnemyLoc.X + .5 < World.Instance.borderRight)
                {
                    EnemyMove.Instance.MoveRight(this);
             
                }
                else if (choose == 2 && EnemyLoc.Y + .5 < World.Instance.borderBottom)
                {
                    EnemyMove.Instance.MoveDown(this);
                    
                }
                else if (choose == 3 && EnemyLoc.X - 1 > 0)
                {
                    EnemyMove.Instance.MoveLeft(this);
                   
                }
                else if (choose == 4 && EnemyLoc.Y - 1 > 0)
                {
         
                    EnemyMove.Instance.MoveUp(this);
                }
                else if (choose == 5 && EnemyLoc.Y - 1 > 0)
                {

                    EnemyMove.Instance.Stand();
                }
                else
                {
                    EnemyMove.Instance.Timer = 0;
                    choose = rand.Next(1, 6);
                }
            }
            else
            {
                EnemyMove.Instance.Timer = 0;
                choose = rand.Next(1, 6);
            }

            observer.NotifyMoved(this);
        }

        public abstract void RemoveEnemyHealth(int amount);

        public abstract void AddEnemyHealth(int amount);

        public abstract void KillEnemy();

    }


    class Skeleton : Enemy
    {
        public Skeleton(IEnemyObserver observer, int x, int y) : base(observer, x, y)
        {

        }

        /// <summary>
        /// Returns entity kind
        /// </summary>
        /// <returns></returns>
        public override string GetKind()
        {
            return "Skeleton";
        }

        /// <summary>
        /// Removes number from enemy's health
        /// </summary>
        public override void RemoveEnemyHealth(int amount)
        {
            Health -= amount;
        }

        /// <summary>
        /// Adds number to enemy's health
        /// </summary>
        public override void AddEnemyHealth(int amount)
        {
            Health += amount;
        }

        public override void KillEnemy()
        {
            if(Health <= 0)
            {
                World.Instance.Entities.Remove(this);
            }
        }

    }

    class EnemyControl : ContentControl, IEnemyObserver
    {
        public void NotifyMoved(Enemy enemy)
        {
            Canvas.SetTop(this, enemy.EnemyLoc.Y);
            Canvas.SetLeft(this, enemy.EnemyLoc.X);
        }
    }

    interface IEnemyObserver
    {
        void NotifyMoved(Enemy enemy);
    }
}
