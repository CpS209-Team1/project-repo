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

        public Enemy(IEnemyObserver observer, int x, int y)
        {
            this.observer = observer;
            Health = 10;
            EnemyLoc.X = 0;
            EnemyLoc.Y = 0;
            Image = "skeleton.png";
            Id = ++nextId;
        }

        public abstract string GetKind();

        public void UpdatePosition()
        {
            EnemyLoc.X += 1;
            EnemyLoc.Y += 1;
            observer.NotifyMoved(this);
        }

        public abstract void RemoveEnemyHealth(int amount);

        public abstract void AddEnemyHealth(int amount);

    }


    class Skeleton : Enemy
    {
        public Skeleton(IEnemyObserver observer, int x, int y): base(observer, x, y)
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds number to enemy's health
        /// </summary>
        public override void AddEnemyHealth(int amount)
        {
            throw new NotImplementedException();
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
