using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Model
{
    class World
    {
        public List<Enemy> Entities { get; set; } //This list is for storing all game entities
        public List<Enemy> DeadEnemy { get; set; } //This list is for storing enemies that need to be removed from the canvas
        public double borderRight { get; set; } //This is for border collision
        public double borderBottom { get; set; } //This is for border collision
        public int Difficulty { get; set; } //Ranges from 1 to 3
        public bool CheatMode { get; set; }
        public int LevelCount { get; set; }
        public int Time { get; set; }
        public bool GameCompleted { get; set; }
        public List<Enemy> EntitiesLoad { get; set; }
        public bool Load { get; set; }
        public List<string> EnemyTypes {get;set;}
        private World()
        {
            Entities = new List<Enemy>();
            DeadEnemy = new List<Enemy>();
            borderBottom = 0;
            Difficulty = 1; 
            CheatMode = false;
            LevelCount = 1;
            Time = 0;
            GameCompleted = false;
            EntitiesLoad = new List<Enemy>();
            Load = false;
            EnemyTypes = new List<string> { "skeleton", "troll" };
        }

        public void ResetWorld()
        {
            Entities = new List<Enemy>();
            DeadEnemy = new List<Enemy>();
            Difficulty = 1;
            CheatMode = false;
            LevelCount = 1;
            Time = 0;
            GameCompleted = false;
        }

        private static World instance = new World();

        /// <summary>
        /// Adds enemy to `Entities` list
        /// </summary>
        /// <param name="enemy"></param>
        public void AddEntity(Enemy enemy)
        {
            Entities.Add(enemy);
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

        public List<string> Serialize()
        {
            List<string> world = new List<string>();
            int entNum = World.Instance.Entities.Count();
            world.Add("\t- World:");
            world.Add("\t\tAttributes: ");
            world.Add(String.Format("\t\t\tBorder: {0},{1}",World.Instance.borderRight,World.Instance.borderBottom));
            world.Add(String.Format("\t\t\tDifficulty: {0}",World.Instance.Difficulty));
            world.Add(String.Format("\t\t\tCheatMode: {0}",World.Instance.CheatMode));
            world.Add(String.Format("\t\t\tLevelCount: {0}",World.Instance.LevelCount));
            world.Add(String.Format("\t\t\tTime: {0}",World.Instance.Time));
            world.Add(String.Format("\t\tEntities: {0}",entNum));
            foreach (Enemy ent in World.Instance.Entities)
            {
                List<string> attrs = ent.Serialize();
                foreach(string attr in attrs) { world.Add(attr); }
            }
            return world;
        }

        public void Deserialize(StreamReader rd)
        {
            Instance.Load = true;
            rd.ReadLine();
            string[] border = rd.ReadLine().Trim().Split(' ')[1].Split(',');
            World.Instance.borderRight = Convert.ToInt32(border[0]);
            World.Instance.borderBottom = Convert.ToInt32(border[1]);
            World.Instance.Difficulty = Convert.ToInt32(rd.ReadLine().Trim().Split(' ')[1]);
            World.Instance.CheatMode = Boolean.Parse(rd.ReadLine().Trim().Split(' ')[1]);
            World.Instance.LevelCount = Convert.ToInt32(rd.ReadLine().Trim().Split(' ')[1]);
            World.Instance.Time = Convert.ToInt32(rd.ReadLine().Trim().Split(' ')[1]);
            int numEnts = Convert.ToInt32(rd.ReadLine().Trim().Split(' ')[1]);
            for (int i = 0; i < numEnts; ++i)
            {
                string image = rd.ReadLine().Trim().Split(' ')[1];
                int health = Convert.ToInt32(rd.ReadLine().Trim().Split(' ')[1]);
                string[] loc = rd.ReadLine().Trim().Split(' ')[1].Split(',');
                double x = Convert.ToDouble(loc[0]);
                double y = Convert.ToDouble(loc[1]);
                Skeleton ent = new Skeleton(Spawn.Instance.observer,x,y,image);
                World.Instance.Entities.Add(ent);
            }
            
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
