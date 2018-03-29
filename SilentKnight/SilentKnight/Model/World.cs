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
