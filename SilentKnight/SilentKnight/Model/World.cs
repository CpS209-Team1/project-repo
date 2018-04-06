using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    class World
    {
        public List<Enemy> Entities { get; set; }//This list is for storing all game entities
        public List<Enemy> DeadEnemy { get; set; }//This list is for storing enemies that need to be removed from the canvas
        public double borderRight { get; set; }//This is for border collision
        public double borderBottom { get; set; }//This is for border collision
        public int Difficulty { get; set; }
        public bool CheatMode { get; set; }
        public int LevelCount { get; set; }

        private World()
        {
            Entities = new List<Enemy>();
            DeadEnemy = new List<Enemy>();
            borderBottom = 0;
            Difficulty = 1;
            CheatMode = false;
            LevelCount = 1;
        }
        private static World instance = new World();
        /// <summary>
        /// Adds enemy to `Entities` list
        /// </summary>
        /// <param name="enemy"></param>
        public void AddEntity(Enemy enemy)
        {

        }

        // public Enemy GetEntityByID(int ID)
        //{
        //Enemy enemy;
        //enemy = new Skeleton();
        // return enemy;
        //}
        /// <summary>
        /// Removes enemy from `Entities` list
        /// </summary>
        /// <param name="id"></param>
        public void RemoveEntity(int id)
        {

        }

        public static World Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
