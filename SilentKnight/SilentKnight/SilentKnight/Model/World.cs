﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilentKnight;

namespace Model
{
    class World
    {
        public List<Enemy> Entities { get; set; }
        public List<Enemy> DeadEnemy { get; set; }
        List<EnemyControl> CanvasEntities = new List<EnemyControl>();
        public double borderRight { get; set; }
        public double borderBottom { get; set; }
        private World()
        {
            Entities = new List<Enemy>();
            DeadEnemy = new List<Enemy>();
            CanvasEntities = new List<EnemyControl>();
            borderBottom = 0;
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