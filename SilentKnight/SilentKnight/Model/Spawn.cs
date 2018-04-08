using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// using System.Windows.Controls;
// using System.Windows.Media.Imaging;

namespace Model
{
    class Spawn
    {
        public IEnemyObserver observer;
        private Spawn() { }
        private static Spawn instance = new Spawn();

        /// <summary>
        /// Takes enemyCount and calls command's `DoCreate` to create `enemyCount` ammount of enemies
        /// </summary>
        /// <param name="enemyCount"></param>
        public void DoSpawn(int enemyCount)
        {
           Random rand = new Random();
           for (int i = 1; i <= enemyCount; i++)
           {
               int x = rand.Next(0, (int)World.Instance.borderRight);
               int y = rand.Next(0, (int)World.Instance.borderBottom);

               var enemy = new Skeleton(observer, x, y,"skeleton.png");
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
