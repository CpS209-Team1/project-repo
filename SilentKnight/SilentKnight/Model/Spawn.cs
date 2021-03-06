﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// This file contains logic for loading 
/// </summary>
namespace Model
{
    /// <summary>
    /// This class is used to create instances of enemies when loading
    /// </summary>
    class Spawn
    {
        public IEnemyObserver observer { get; set; } // Observer reference

        /// <summary>
        /// Constructor
        /// </summary>
        private Spawn() { }
        private static Spawn instance = new Spawn();

        /// <summary>
        /// Takes enemyCount and calls command's `DoCreate` to create `enemyCount` ammount of enemies
        /// </summary>
        /// <param name="enemyCount"></param>
        public void DoSpawn(int enemyCount)
        {
            Enemy enemy;
            Random rand = new Random();

            foreach (Enemy j in World.Instance.Entities)
            {
                int x = rand.Next(0, (int)World.Instance.borderRight);
                int y = rand.Next(0, (int)World.Instance.borderBottom);
                switch (j.GetKind())
                {
                    case "skeleton":
                        enemy = new Skeleton(observer, x, y, "/Assets/skeleton/skeleton_topdown_basic18.png", 75);
                        break;
                    case "troll":
                        enemy = new Troll(observer, x, y, "/Assets/troll/troll_topdown_basic18", 75);
                        break;
                    default:
                        enemy = new Skeleton(observer, x, y, "/Assets/skeleton/skeleton_topdown_basic18.png", 75);
                        break;
                }
                World.Instance.Entities.Add(enemy);
            }
        }

        public static Spawn Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
