using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    class World
    {
        public List<Enemy> Entities { get; set; }
        private World() { }

        /// <summary>
        /// Adds enemy to `Entities` list
        /// </summary>
        /// <param name="enemy"></param>
        public void AddEntity(Enemy enemy)
        {

        }

        /// <summary>
        /// Removes enemy from `Entities` list
        /// </summary>
        /// <param name="id"></param>
        public void RemoveEntity(int id)
        {

        }

        private static World instance = new World();
        public static World Instance
        {
            get
            {
                return Instance;
            }
        }
    }
}
