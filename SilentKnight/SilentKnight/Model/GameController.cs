﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
/// <summary>
/// This file contains the `GameController`, and `ISerializable`
/// </summary>

namespace Model
{

    /// <summary>
    /// Implements `Serialize` and `Deserialize`
    /// </summary>
    interface ISerializable
    {
        List<string> Serialize();
        void Deserialize(StreamReader rd);
    }

    /// <summary>
    /// This class controlls methods that are part of the main game model
    /// </summary>
    public class GameController
    {
        public List<string> Users = new List<string>(); // This contains a list of players

        /// <summary>
        /// This method adds time to the game clock
        /// </summary>
        public void AddTime()
        {
            World.Instance.Time += 1;
        }

        /// <summary>
        /// This method calculates the game's score
        /// </summary>
        public void CalculateScore()
        {
            Player.Instance.PlayerScore += (1000 / (World.Instance.Time / Player.Instance.Health)) + 100;
        }

        /// <summary>
        /// Validates whether a user exists in a specified text file.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="filename"></param>
        public bool ValidateUser(string name,string filename)
        {
            if (!File.Exists(filename))
            {
                using (StreamWriter w = File.AppendText(filename));
            }
            string[] file = File.ReadAllText(filename).Split(new string[]{Environment.NewLine},StringSplitOptions.None);
            List<string> contents = new List<string>(file);
            foreach (string line in contents)
            {
                if (line.Contains("@"))
                {
                    string user = line.Substring(1, line.IndexOf(":") - 1);
                    Console.WriteLine(user);
                    Users.Add(user);
                }
            }
            return Users.Contains(name);
        }


        /// <summary>
        /// This method creates a new user
        /// </summary>
        /// <param name="name">Player's name</param>
        /// <param name="filename">File's name</param>
        /// <param name="ctrl">reference to GameController</param>
        public void CreateNewUser(string name, string filename,GameController ctrl)
        {
            if (!ctrl.ValidateUser(name,filename))
            {
                Player.Instance.PlayerName = name;
            }
        }

        /// <summary>
        /// Displays player and all entities in World.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="filename"></param>
        public void Print()
        {
            Console.WriteLine(String.Format("Player: {0} at ({1},{2})",
                                                                    Player.Instance.PlayerName,
                                                                    Player.Instance.PlayerLoc.X,
                                                                    Player.Instance.PlayerLoc.Y));
            Console.WriteLine(String.Format("World Entities:"));
            foreach (Enemy ent in World.Instance.Entities)
            {
                Console.WriteLine(String.Format("\t-> {0} at ({1},{2})",
                                                                    ent.Image,
                                                                    ent.EnemyLoc.X,
                                                                    ent.EnemyLoc.Y));
            }
        }

        /// <summary>
        /// Removes the current player's data from a given text file.
        /// </summary>
        /// <param name="filename"></param>
        public void RemovePlayerData(string filename)
        {
            int startInd = 0;
            int endInd = 0;
            if (!File.Exists(filename))
            {
                using (StreamWriter w = File.AppendText(filename)) ;
            }
            string[] file = File.ReadAllText(filename).Split(new string[]{Environment.NewLine},StringSplitOptions.None);
 
            List<string> contents = new List<string>(file);
            contents.RemoveAll(String.IsNullOrWhiteSpace);
            List<string> outfile = contents;

            bool found = false;
            int ind = 0;
            foreach(string line in contents)
            {
                string curline = line.Trim();

                if (curline == String.Format("@{0}:",Player.Instance.PlayerName))
                {
                    startInd = ind;
                    found = true;
                }
                else if (((curline.Contains("@") || (ind == contents.Count - 1)) && found))
                {
                    if (curline.Contains("@")) { endInd = ind - 1; }
                    else { endInd = ind; }
                    break;
                }
                ++ind;
            }
            if (found)
            {
                if (endInd == 0) { endInd = startInd; }
                for (int i = startInd; i <= endInd; ++i)
                {
                    outfile.RemoveAt(startInd);
                }
                outfile.RemoveAll(String.IsNullOrWhiteSpace);
                File.WriteAllLines(filename, outfile.ToArray(), Encoding.UTF8);
            }
            if (!found) Console.WriteLine("***USER NOT FOUND");
        }

        /// <summary>
        /// Updates the player's location
        /// 
        /// Takes the canvas x and y pos and sets it to the player pos
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void ComputePlayerMove(double x, double y, string KeyPress)
        {
            Player.Instance.PlayerLoc.X = x + 100;
            Player.Instance.PlayerLoc.Y = y + 100;
            switch (KeyPress) //Keypress is used to determine which direction the player was looking in when he/she attacked
            {
                //Sets the player's direction to the appropriate direction
                case "S":
                    Player.Instance.PlayerDirection = Direction.Down;
                    break;
                case "A":
                    Player.Instance.PlayerDirection = Direction.Left;
                    break;
                case "W":
                    Player.Instance.PlayerDirection = Direction.Up;
                    break;
                case "D":
                    Player.Instance.PlayerDirection = Direction.Right;
                    break;
            }
        }

        /// <summary>
        /// Calculates the player's attack to see if attack was successfull
        /// </summary>
        public void ComputePlayerMeleeAttack()
        {
            Player.Instance.PlayerState.HandleInput("melee");
           
        }

        public void ComputePlayerRangedAttack()
        {
            Player.Instance.PlayerState.HandleInput("ranged");
            if (Player.Instance.PlayerCoolDown == 0)
            {
                Player.Instance.PlayerCoolDown = 20;
            }
        }

        /// <summary>
        /// Calculates the enemy's attack to see if attack was successful
        /// </summary>
        public void ComputeEnemyAttack()
        {
            int hit = 0;
            foreach (Enemy i in World.Instance.Entities)
            {
                if (Math.Sqrt(Math.Pow(Player.Instance.PlayerLoc.X - (i.EnemyLoc.X + i.Center), 2) + Math.Pow(Player.Instance.PlayerLoc.Y - (i.EnemyLoc.Y + i.Center), 2)) < i.Center/1.5 && i.CoolDownTimer == 0 && World.Instance.CheatMode == false)
                {
                    i.Observer.NotifyAttack(i);
                    Player.Instance.RemovePlayerHealth(i.AttackDamage);
                    Console.WriteLine(i.Health);
                    //Console.WriteLine(Player.Instance.Health);
                    i.CoolDownTimer = i.CoolDown ;
                    hit += 1;
                }
                else
                {
                    if (i.CoolDownTimer != 0)
                    {
                        i.CoolDownTimer -= 1; //This method causes enemy's to have a 100 tick cooldown before being able to injure the player again
                    }
                }
            }
        }

        /// <summary>
        /// Computes the arrow's location to see if it made contact with an enemy
        /// </summary>
        public void ComputeArrowAttack()
        {
            foreach (Arrow i in World.Instance.EntitiesArrow)
            {
                foreach (Enemy j in World.Instance.Entities)
                {
                    if (Math.Sqrt(Math.Pow(i.ArrowLocation.X - (j.EnemyLoc.X+j.Center), 2) + Math.Pow(i.ArrowLocation.Y - (j.EnemyLoc.Y+j.Center), 2)) <= (20))
                    {
                        j.RemoveEnemyHealth(2);
                        EnemyMove.Instance.Hit(j);
                        World.Instance.DeadArrow.Add(i);
                    }

                    if (j.Health <= 0)
                    {
                        World.Instance.DeadEnemy.Add(j);// Once an enemy is removed from the Entities list, it is added to DeadEnemy for removal from the canvas

                        Player.Instance.PlayerScore += 1;

                    }

                }
            }
            foreach (Enemy i in World.Instance.DeadEnemy)
            {
                i.KillEnemy();
            }
        }

        public void KeepEnemyInBounds()
        {
            foreach (Enemy i in World.Instance.Entities)
            {
                EnemyMove.Instance.Teleport(i);
            }
        }

        /// <summary>
        /// Activates all of the model's serialization methods
        /// </summary>
        /// <param name="filename">output file</param>
        public void Save(string filename)
        {
            if (!File.Exists(filename))
            {
                using (StreamWriter w = File.AppendText(filename)) ;
            }
            RemovePlayerData(filename);
            string[] file = File.ReadAllText(filename).Split(new string[]{Environment.NewLine},StringSplitOptions.None);
            List<string> contents = new List<string>(file);
            contents.RemoveAll(String.IsNullOrWhiteSpace);
            List <string> plr = Player.Instance.Serialize();
            List <string> world = World.Instance.Serialize();
            foreach(string attr in plr) { contents.Add(attr); }
            foreach(string elem in world) { contents.Add(elem); }
            contents.RemoveAll(String.IsNullOrWhiteSpace);
            File.WriteAllLines(filename, contents.ToArray(), Encoding.UTF8);
        }

        /// <summary>
        /// Starts the loading process
        /// </summary>
        /// <param name="filename">input file</param>
        public void Load(string filename)
        {
            string line;
            using (StreamReader rd = new StreamReader(filename))
            {
                while ((line = rd.ReadLine()) != null)
                {
                    if (line == String.Format("@{0}:",Player.Instance.PlayerName))
                    {
                        while(((line = rd.ReadLine()) != null) && (!line.Contains("@")))
                        {
                            if (line.Contains("- Player:"))
                            {
                                Player.Instance.Deserialize(rd);
                            }
                            else if (line.Contains("- World:"))
                            {
                                World.Instance.Deserialize(rd);
                            }
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Contains enemy and player locations
    /// </summary>
    public struct Location
    {
        public double X { get; set; }
        public double Y { get; set; }
    }
}
