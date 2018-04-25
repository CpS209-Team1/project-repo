using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Threading;

/// <summary>
/// This file contains an abstract class `Enemy` along with its children: `Skeleton`, `Troll`, and `Spider`
/// This file also includes an interface `IEnemyObserver`
/// </summary>

namespace Model
{
    /// <summary>
    /// This class contains methods that control the enemy
    /// </summary>
    public abstract class Enemy : ISerializable
    {
        public IEnemyObserver Observer; // Instance of the observer
        public int Health { get; set; } // Enemy's health
        public Location EnemyLoc; // Contains the enemy's x and y coords
        public string Image { get; set; } // Contains the enemy's type (Skeleton, troll, etc.)
        public int CoolDownTimer { get; set; } // Enemy attack cooldown countdown
        public int CoolDown { get; set; } // Enemy cooldown number (static for each enemy type)
        public int AttackDamage { get; set; } // Amount of attack damage an enemy can deal
        static Random rand = new Random(); // Used to create random numbers
        public double EnemySpeed { get; set; } // Enemy's walking speed
        int choose = rand.Next(1, 6); // Used for determining enemy's direction
        public int Height { get; set; } // Used for dynamic measurements
        public int Center { get; set; } // Used for dynamic measurements
        public Direction EnemyDirection { get; set; } // Contains the enemy's current direction
        public bool IsMoving { get; set; } // Determines if player is moving or not
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="observer">Enemy observer</param>
        /// <param name="x">Enemy's x coord</param>
        /// <param name="y">Enemy's y coord</param>
        /// <param name="image">Enemy's type</param>
        /// <param name="height">Enemy's image type</param>
        public Enemy(IEnemyObserver observer, double x, double y, string image, int height)
        {
            CoolDownTimer = 100;
            Observer = observer;
            EnemyLoc.X = x;
            EnemyLoc.Y = y;
            Image = image;
        }

        public abstract string GetKind();

        /// <summary>
        /// This method updates the enemy's position
        /// </summary>
        public void UpdatePosition()
        {
            double dist = Math.Sqrt(Math.Pow(Player.Instance.PlayerLoc.X - (EnemyLoc.X + 100), 2) + Math.Pow(Player.Instance.PlayerLoc.Y - (EnemyLoc.Y + 100), 2));
            //Checks if the enemy is within 150 px from the player, if so then it will call the Track method
            if (dist < Height)
            {
                EnemyMove.Instance.Track(this, dist);
            }
            //If the enemy isn't near the player then the enemy will randomly go in a given position by calling one of the methods below
            else if (EnemyMove.Instance.Timer < 100)
            {
                if (choose == 1 && EnemyLoc.X + EnemySpeed < World.Instance.borderRight - Center)
                {
                    EnemyMove.Instance.MoveRight(this, EnemySpeed);
                }
                else if (choose == 2 && EnemyLoc.Y + EnemySpeed < World.Instance.borderBottom - Center)
                {
                    EnemyMove.Instance.MoveDown(this, EnemySpeed);

                }
                else if (choose == 3 && EnemyLoc.X - EnemySpeed > 0)
                {
                    EnemyMove.Instance.MoveLeft(this, EnemySpeed);

                }
                else if (choose == 4 && EnemyLoc.Y - EnemySpeed > 0)
                {

                    EnemyMove.Instance.MoveUp(this, EnemySpeed);
                }
                else if (choose == 5 && EnemyLoc.Y - EnemySpeed > 0)
                {

                    EnemyMove.Instance.Stand(this);
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
            Observer.NotifyMoved(this);
        }

        /// <summary>
        /// Used for enemy serialization
        /// </summary>
        /// <returns></returns>
        public List<string> Serialize()
        {
            List<string> world = new List<string>();
            world.Add(String.Format("\t\t\tImage: {0}",Image));
            world.Add(String.Format("\t\t\t\tHealth: {0}",Health));
            world.Add(String.Format("\t\t\t\tLocation: {0},{1}",EnemyLoc.X,EnemyLoc.Y));
            return world;
        }

		/// <summary>
		/// Notifies enemy's control to stop animation timer.
		/// </summary>
		public void StopEnemyControl()
		{
			Observer.NotifyPause(this);
		}

		/// <summary>
		/// Notifies enemy's control to start animation timer.
		/// </summary>
		public void StartEnemyControl()
		{
			Observer.NotifyPlay(this);
		}

		public void Deserialize(StreamReader filename)
        {
            //Empty on purpose
        }

        /// <summary>
        /// This method removes `amount` from enemy's health
        /// </summary>
        /// <param name="amount">Amount of damage player dealt</param>
        public abstract void RemoveEnemyHealth(int amount);

        /// <summary>
        /// This method adds `amount` to enemy's health
        /// </summary>
        /// <param name="amount">Amount to be added to health</param>
        public abstract void AddEnemyHealth(int amount);

        /// <summary>
        /// Removes enemy from World list
        /// </summary>
        public abstract void KillEnemy();

    }

    /// <summary>
    /// This class defines the Skeleton enemy
    /// </summary>
    class Skeleton : Enemy
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="observer">Enemy observer</param>
        /// <param name="x">Skeleton's x coord</param>
        /// <param name="y">Skeleton's y coord</param>
        /// <param name="image">Skeleton's type</param>
        /// <param name="height">Skeleton's image type</param>
        public Skeleton(IEnemyObserver observer, double x, double y, string image, int height ) : base(observer, x, y, image, height)
        {
            switch(World.Instance.Difficulty)
            {
                case 1:
                    AttackDamage = 2;
                    break;
                case 2:
                    AttackDamage = 4;
                    break;
                case 3:
                    AttackDamage = 6;
                    break;
            }
            switch (World.Instance.Difficulty)
            {
                case 1:
                    Health = 5 + World.Instance.LevelCount * World.Instance.Difficulty;
                    break;
                case 2:
                    Health = 10 + World.Instance.LevelCount * World.Instance.Difficulty; ;
                    break;
                case 3:
                    Health = 20 + World.Instance.LevelCount * World.Instance.Difficulty; ;
                    break;
            }
            Height = height;
            EnemySpeed = .5;
            CoolDown = 100;
            Center = Height / 2;
        }

        /// <summary>
        /// Returns entity kind
        /// </summary>
        /// <returns></returns>
        public override string GetKind()
        {
            return "skeleton";
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


    /// <summary>
    /// This class defines the Troll enemy
    /// </summary>
    class Troll : Enemy
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="observer">Enemy observer</param>
        /// <param name="x">Troll's x coord</param>
        /// <param name="y">Troll's y coord</param>
        /// <param name="image">Troll's type</param>
        /// <param name="height">Troll's image type</param>
        public Troll(IEnemyObserver observer, double x, double y, string image, int height) : base(observer, x, y, image, height)
        {
            switch (World.Instance.Difficulty)
            {
                case 1:
                    AttackDamage = 1;
                    break;
                case 2:
                    AttackDamage = 3;
                    break;
                case 3:
                    AttackDamage = 5;
                    break;
            }
            switch (World.Instance.Difficulty)
            {
                case 1:
                    Health = 2 + World.Instance.LevelCount * World.Instance.Difficulty;
                    break;
                case 2:
                    Health = 5 + World.Instance.LevelCount * World.Instance.Difficulty; ;
                    break;
                case 3:
                    Health = 8 + World.Instance.LevelCount * World.Instance.Difficulty; ;
                    break;
            }
            Height = height;
            EnemySpeed = .8;
            CoolDown = 50;
            Center = Height / 2;
        }

        /// <summary>
        /// Returns entity kind
        /// </summary>
        /// <returns></returns>
        public override string GetKind()
        {
            return "troll";
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




    /// <summary>
    /// This class defines the Spider boss
    /// </summary>
    class Spider : Enemy
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="observer">Enemy observer</param>
        /// <param name="x">Spider's x coord</param>
        /// <param name="y">Spider's y coord</param>
        /// <param name="image">Spider's type</param>
        /// <param name="height">Spider's image type</param>
        public Spider(IEnemyObserver observer, double x, double y, string image, int height) : base(observer, x, y, image, height)
        {
            switch (World.Instance.Difficulty)
            {
                case 1:
                    AttackDamage = 5;
                    break;
                case 2:
                    AttackDamage = 6;
                    break;
                case 3:
                    AttackDamage = 7;
                    break;
            }
            switch (World.Instance.Difficulty)
            {
                case 1:
                    Health = 20 + World.Instance.LevelCount * World.Instance.Difficulty;
                    break;
                case 2:
                    Health = 30 + World.Instance.LevelCount * World.Instance.Difficulty; ;
                    break;
                case 3:
                    Health = 40 + World.Instance.LevelCount * World.Instance.Difficulty; ;
                    break;
            }
            Height = height;
            EnemySpeed = .5;
            CoolDown = 50;
            Center = Height / 2;
        }

        /// <summary>
        /// Returns entity kind
        /// </summary>
        /// <returns></returns>
        public override string GetKind()
        {
            return "spider";
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

    /// <summary>
    /// This interface implements Observer pattern
    /// </summary>
    public interface IEnemyObserver
    {
        void NotifyMoved(Enemy enemy);
        void NotifySpawn(Enemy enemy);
        void NotifyAttack(Enemy enemy);
		void NotifyPause(Enemy enemy);
		void NotifyPlay(Enemy enemy);
	}
}
