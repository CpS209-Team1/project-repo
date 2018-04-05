using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Model
{
        struct Location
    {
        public double X { get; set; }
        public double Y { get; set; }
    }

    class Player
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public Location PlayerLoc;
        private Player()
        {
            Health = 20;
        }

        public void Login(string user)
        {
            Name = user;
            Health = 10;
            PlayerLoc.X = 0;
            PlayerLoc.Y = 0;
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

        public void Deserialize(StreamReader rd)
        {
            string[] Loc = rd.ReadLine().Trim().Split(' ')[1].Split(',');
            PlayerLoc.X = Convert.ToDouble(Loc[0]);
            PlayerLoc.Y = Convert.ToDouble(Loc[1]);
            Health = Convert.ToInt32(rd.ReadLine().Trim().Split(' ')[1]);
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