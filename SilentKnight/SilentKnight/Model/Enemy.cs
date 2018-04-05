using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Model
{
    abstract class Enemy : ISerializable
    {
        public IEnemyObserver observer;
        public int Health { get; set; }
        public int Id { get; set; }
        public Location EnemyLoc;
        public string Image { get; set; }

        static int nextId;
        public int CoolDown { get; set; } //Enemy attack cooldown

        static Random rand = new Random();

        int choose = rand.Next(1, 6);

        public Enemy(IEnemyObserver observer, int x, int y)
        {
            CoolDown = 0;
            this.observer = observer;
            switch(World.Instance.Difficulty)
            {
                case 1:
                    Health = 5;
                    break;
                case 2:
                    Health = 10;
                    break;
                case 3:
                    Health = 20;
                    break;
                default:
                    Health = 10;
                    break;
            }
            EnemyLoc.X = x;
            EnemyLoc.Y = y;
            Image = "skeleton.png";
            Id = ++nextId;
        }

        public abstract string GetKind();

        /// <summary>
        /// This method updates the enemy's position
        /// </summary>
        public void UpdatePosition()
        {
            //Checks if the enemy is within 150 px from the player, if so then it will call the Track method
            if (Math.Sqrt(Math.Pow(Player.Instance.PlayerLoc.X - EnemyLoc.X, 2) + Math.Pow(Player.Instance.PlayerLoc.Y - EnemyLoc.Y, 2)) < 150)
            {
                EnemyMove.Instance.Track(this);
            }
            //If the enemy isn't near the player then the enemy will randomly go in a given position by calling one of the methods below
            else if (EnemyMove.Instance.Timer < 100)
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
            // if the enemy runs into a wall then it will choose a new direction
            else
            {
                EnemyMove.Instance.Timer = 0;
                choose = rand.Next(1, 6);
            }

            observer.NotifyMoved(this);
        }

        public void Serialize(string filename)
        {

        }

        public void Deserialize(string filename)
        {

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

        //Removes enemy from Entities list
        public override void KillEnemy()
        {
            if (Health <= 0)
            {
                World.Instance.Entities.Remove(this);
            }
        }
    }


    // Oberver pattern
    interface IEnemyObserver
    {
        void NotifyMoved(Enemy enemy);
    }
}
