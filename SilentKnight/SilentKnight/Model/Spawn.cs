﻿using System;
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
                var enemyControl = new EnemyControl();
                enemyControl.Content = new Image()
                {
                    Source = new BitmapImage(new Uri("/Assets/skeleton.png", UriKind.Relative))

                };
                enemyControl.Width = 50;
                enemyControl.Height = 50;
                Canvas.SetTop(enemyControl, rand.Next(0,300));
                Canvas.SetLeft(enemyControl, rand.Next(0, 300));
                World.Instance.CanvasEntities.Add(enemyControl);

                var enemy = new Skeleton(enemyControl, 0, 0);
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
