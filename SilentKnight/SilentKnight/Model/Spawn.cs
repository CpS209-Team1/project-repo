using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    class Spawn
    {
        private Spawn() { }
        private static Spawn instance = new Spawn();

        /// <summary>
        /// Takes enemyCount and calls command's `DoCreate` to create `enemyCount` ammount of enemies
        /// </summary>
        /// <param name="enemyCount"></param>
        public void DoSpawn(int enemyCount)
        {

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
