using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

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
               

                var enemy = new Skeleton(enemyControl, x, y);
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
