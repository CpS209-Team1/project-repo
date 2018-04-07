﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model

{
    enum Direction { Up, Down, Left, Right}; //Used to determine the player's "viewing" direction
    class Player : ISerializable
    {
        public int Health { get; set; }
        public Location PlayerLoc;
        public Direction PlayerDirection { get; set; }
        public int PlayerScore { get; set; }
        public string PlayerName { get; set; }
        public int HealthLevel { get; set; }
        private Player()
        {
            PlayerDirection = Direction.Down;
            Health = 20;
            PlayerLoc.X = 234;
            PlayerLoc.Y = 159;
            HealthLevel = 1;
        }

        /// <summary>
        /// Removes `ammount` from `Health`
        /// </summary>
        /// <param name="ammount"></param>
        public void RemovePlayerHealth(int amount)
        {
            Health -= amount;
        }


        /// <summary>
        /// Adds `ammount` to `Health`
        /// </summary>
        /// <param name="ammount"></param>
        public void AddPlayerHealth(int amount)
        {

        }

        public void Serialize(string filename)
        {
            
        }

        public void Deserialize(string filename)
        {

        }

        private static Player instance = new Player();

        public static Player Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
