using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
            PlayerScore = 0;
        }

        public void Login(string user,string filename, GameController ctrl)
        {
            if (ctrl.ValidateUser(user,filename))
            {
                Player.Instance.PlayerName = user;
            }
            else
            {
                Console.WriteLine("**NO USER FOUND");
            }
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

        public List<string> Serialize()
        {
            List<string> player = new List<string>();
            player.Add(String.Format("@{0}:",Player.Instance.PlayerName));
            player.Add("\t- Player:");
            player.Add(String.Format("\t\tHealth: {0}",Player.Instance.Health));  
            player.Add(String.Format("\t\tLocation: {0},{1}",Player.Instance.PlayerLoc.X,Player.Instance.PlayerLoc.Y));
            player.Add(String.Format("\t\tPlayerScore: {0}",Player.Instance.PlayerScore));
            player.Add(String.Format("\t\tHealthLevel: {0}",Player.Instance.HealthLevel));   
            return player;                                                                                                                      
        }

        public void Deserialize(StreamReader rd)
        {
            Player.Instance.Health = Convert.ToInt32(rd.ReadLine().Trim().Split(' ')[1]);
            string[] Loc = rd.ReadLine().Trim().Split(' ')[1].Split(',');
            Player.Instance.PlayerLoc.X = Convert.ToDouble(Loc[0]);
            Player.Instance.PlayerLoc.Y = Convert.ToDouble(Loc[1]);
            Player.Instance.PlayerScore = Convert.ToInt32(rd.ReadLine().Trim().Split(' ')[1]);
            Player.Instance.HealthLevel = Convert.ToInt32(rd.ReadLine().Trim().Split(' ')[1]);
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
