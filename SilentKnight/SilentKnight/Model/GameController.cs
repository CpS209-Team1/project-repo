using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Model
{
    interface ISerializable
    {
        List<string> Serialize();
        void Deserialize(StreamReader rd);
    }

    class GameController
    {
        int currentTime = 0;
        public int AddTime()
        {
            World.Instance.Time += 1;
            if(currentTime == 59)
            {
                currentTime = 0;
            }
            else
            {
                currentTime += 1;
            }
            return (currentTime);
        }

        public void CalculateScore()
        {
            Player.Instance.PlayerScore += (1000 / World.Instance.Time) + 100;
        }

        public List<string> Users = new List<string>();

        /// <summary>
        /// Validates whether a user exists in a specified text file.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="filename"></param>
        public bool ValidateUser(string name,string filename)
        {
            string[] file = File.ReadAllText(filename).Split('\n');
            List<string> contents = new List<string>(file);
            foreach (string line in contents)
            {
                if (line.Contains("@"))
                {
                    Users.Add(line.Substring(1,line.IndexOf(":") - 1));
                }
            }
            return Users.Contains(name);
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
            string[] file = File.ReadAllText(filename).Split('\n');
            List<string> contents = new List<string>(file);
            contents.RemoveAll(String.IsNullOrWhiteSpace);
            List<string> outfile = contents;

            int size = contents.Count();
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
                for (int i = startInd; i <= endInd; ++i)
                {
                    outfile.RemoveAt(startInd);
                }
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
            Player.Instance.PlayerLoc.X = x;
            Player.Instance.PlayerLoc.Y = y;
            switch (KeyPress)//Keypress is used to determine which direction the player was looking in when he/she attacked
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
        public void ComputePlayerAttack()
        {
            foreach (Enemy i in World.Instance.Entities)
            {
                if (Math.Sqrt(Math.Pow(Player.Instance.PlayerLoc.X - i.EnemyLoc.X, 2) + Math.Pow(Player.Instance.PlayerLoc.Y - i.EnemyLoc.Y, 2)) < 50)
                {
                    i.RemoveEnemyHealth(2);
                    EnemyMove.Instance.Hit(i);
                }
                if (i.Health <= 0)
                {
                    World.Instance.DeadEnemy.Add(i);// Once an enemy is removed from the Entities list, it is added to DeadEnemy for removal from the canvas
                    Player.Instance.PlayerScore += 1;
                }
            }

            foreach (Enemy i in World.Instance.DeadEnemy)
            {
                i.KillEnemy();
            }
        }

        /// <summary>
        /// Calculates the enemy's attack to see if attack was successful
        /// </summary>
        public int ComputeEnemyAttack()
        {
            int hit = 0;
            foreach (Enemy i in World.Instance.Entities)
            {
                if (Math.Sqrt(Math.Pow(Player.Instance.PlayerLoc.X - i.EnemyLoc.X, 2) + Math.Pow(Player.Instance.PlayerLoc.Y - i.EnemyLoc.Y, 2)) < 25 && i.CoolDown == 0 && World.Instance.CheatMode == false)
                {
                    Player.Instance.RemovePlayerHealth(2);
                    //Console.WriteLine(Player.Instance.Health);
                    i.CoolDown = 100;
                    hit += 1;
                }
                else
                {
                    if (i.CoolDown != 0)
                    {
                        i.CoolDown -= 1; //This method causes enemy's to have a 100 tick cooldown before being able to injure the player again
                    }
                }
            }
            return (hit);
        }

        public void KeepEnemyInBounds()
        {
            foreach (Enemy i in World.Instance.Entities)
            {
                EnemyMove.Instance.Teleport(i);
            }
        }

        public void Save(string filename)
        {
            RemovePlayerData(filename);
            string[] file = File.ReadAllText(filename).Split('\n');
            List<string> contents = new List<string>(file);
            contents.RemoveAll(String.IsNullOrWhiteSpace);
            List <string> plr = Player.Instance.Serialize();
            List <string> world = World.Instance.Serialize();
            foreach(string attr in plr) { contents.Add(attr); }
            foreach(string elem in world) { contents.Add(elem); }
            File.WriteAllLines(filename, contents.ToArray(), Encoding.UTF8);
        }

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
    struct Location
    {
        public double X { get; set; }
        public double Y { get; set; }
    }
}
