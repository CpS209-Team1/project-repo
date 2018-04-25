using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

/// <summary>
/// This file contains player logic and an enum `Direction`
/// </summary>
namespace Model
{
    public enum Direction { Up, Down, Left, Right}; //Used to determine the player's "viewing" direction
    /// <summary>
    /// This class contains player attributes and is a Singleton
    /// </summary>
    class Player : ISerializable
    {
        public int Health { get; set; } // Player's health
        public Location PlayerLoc; // Player's x and y location
        public Direction PlayerDirection { get; set; } // Player's direction
        public int PlayerScore { get; set; } // Current score
        public string PlayerName { get; set; } // Contains player's username
        public int PlayerCoolDown { get; set; } // Contains player's cool down
        public bool PlayerIsDead { get; set; } // Bool which tells if the player is dead yet
        public StateMachine PlayerState { get; set; } // Contains reference to the player's state machine

        /// <summary>
        /// Constructor
        /// </summary>
        private Player()
        {
            PlayerName = "";
            PlayerDirection = Direction.Down;
            Health = 20;
            PlayerLoc.X = World.Instance.borderRight / 2;
            PlayerLoc.Y = World.Instance.borderBottom / 2;
            PlayerScore = 0;
            PlayerCoolDown = 0;
            PlayerIsDead = false;
            PlayerState = new StateMachine();
        }

        /// <summary>
        /// Resets player
        /// </summary>
        public void ResetPlayer()
        {
            PlayerName = "";
            PlayerDirection = Direction.Down;
            Health = 20;
            PlayerLoc.X = 234;
            PlayerLoc.Y = 159;
            PlayerScore = 0;
            PlayerCoolDown = 0;
            PlayerIsDead = false;
        }

        /// <summary>
        /// Sets `PlayerName` when the user logs in
        /// </summary>
        /// <param name="user"></param>
        public void Login(string user)
        {
            Player.Instance.PlayerName = user;
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

        /// <summary>
        /// Serializes the player's variables
        /// </summary>
        /// <returns>Returns list of serialized variables</returns>
        public List<string> Serialize()
        {
            List<string> player = new List<string>();
            player.Add(String.Format("@{0}:",Player.Instance.PlayerName));
            player.Add("\t- Player:");
            player.Add(String.Format("\t\tHealth: {0}",Player.Instance.Health));  
            player.Add(String.Format("\t\tLocation: {0},{1}",Player.Instance.PlayerLoc.X,Player.Instance.PlayerLoc.Y));
            player.Add(String.Format("\t\tPlayerScore: {0}",Player.Instance.PlayerScore));
            return player;                                                                                                                      
        }

        /// <summary>
        /// Deserializes the player's variables
        /// </summary>
        /// <param name="rd">StreamReader</param>
        public void Deserialize(StreamReader rd)
        {
            Player.Instance.Health = Convert.ToInt32(rd.ReadLine().Trim().Split(' ')[1]);
            string[] Loc = rd.ReadLine().Trim().Split(' ')[1].Split(',');
            Player.Instance.PlayerLoc.X = Convert.ToDouble(Loc[0]) - 100;
            Player.Instance.PlayerLoc.Y = Convert.ToDouble(Loc[1]) - 100;
            Player.Instance.PlayerScore = Convert.ToInt32(rd.ReadLine().Trim().Split(' ')[1]);
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
