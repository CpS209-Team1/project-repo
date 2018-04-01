using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    abstract class Enemy
    {
        public int Health { get; set; }
        public int Id { get; set; }
        public Location EnemyLoc;
        public string Image { get; set; }

        static int nextId;

        public abstract string GetKind();

        public abstract void UpdatePosition();

        public abstract void RemoveEnemyHealth(int amount);

        public abstract void AddEnemyHealth(int amount);

        public Enemy()
        {
            Health = 10;
            EnemyLoc.X = 0;
            EnemyLoc.Y = 0;
            Image = "skeleton.png";
            Id = ++nextId;
        }
    }


    class Skeleton : Enemy
    {
        public Skeleton()
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
        /// Updates entity position
        /// </summary>
        public override void UpdatePosition()
        {
            throw new NotImplementedException();
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
}
