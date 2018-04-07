using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Model
{
    class Data
    {
        public string PlayerName = null;
        public string Difficulty = null;
        public int Points = 0;
        public int HighScore = 0;
        public int Level = 0;
        public int CurrentLevel = 0;
        public List<string> WorldEntities = new List<string>{"null"};
        
        /// <summary>
        /// Adds new player entry to a given filename
        /// </summary>
        /// <param name="plr"></param> <param name="filename"></param>
        public void CreatePlayer(string plr,string filename)
        {
            string line;
            using (StreamWriter file = new StreamWriter(filename))
            {
                using (StreamReader rd = new StreamReader(filename))
                {
                    while ((line = rd.ReadLine()) != null)
                    {
                        file.WriteLine(String.Format("{0} : {1} : {2} : {3} : {4} : {5}",
                                                                                        PlayerName,
                                                                                        Difficulty,
                                                                                        Points.ToString(),
                                                                                        HighScore.ToString(),
                                                                                        Level.ToString(),
                                                                                        CurrentLevel.ToString()));
                        //WorldEntities.ForEach(file.WriteLine());
                    }
                }
            }
        }


        /// <summary>
        /// Loads player data from a given filename into
        /// the appropriate variables
        /// </summary>
        /// <param name="filename"></param>
        public void Load(string filename)
        {
            string line;
            using (StreamReader file = new StreamReader(filename))
            {
                while((line = file.ReadLine()) != null)  
                {
                    string curLine = "string"; // line.Split(":");
                    // mbrun138 : medium : 450 : 200 : 2 : 2 : 3,skeleton,skeleton,skeleton
                    if (true)//curLine[0] == PlayerName)
                    {
                        //Difficulty = curLine[1];
                        Points = Convert.ToInt32(curLine[2]);
                        HighScore = Convert.ToInt32(curLine[3]);
                        Level = Convert.ToInt32(curLine[4]);
                        CurrentLevel = Convert.ToInt32(curLine[5]);
                       // WorldEntities = curLine[6].Split(",");
                    }
                }
            }
        }

        /// <summary>
        /// Saves current player data into a text file
        /// </summary>
        /// <param name="filename"></param>
        public void Save(string filename)
        {
            string line;
            using (StreamWriter file = new StreamWriter(filename))
            {
                using (StreamReader rd = new StreamReader(filename))
                {
                    while ((line = rd.ReadLine()) != null)
                    {
                        string curLine = "string"; //"line.Split(" : ");
                        if (curLine == PlayerName)//curLine[0] == PlayerName)
                        {
                            file.WriteLine(String.Format("{0} : {1} : {2} : {3} : {4} : {5}",
                                                                                            PlayerName,
                                                                                            Difficulty,
                                                                                            Points.ToString(),
                                                                                            HighScore.ToString(),
                                                                                            Level.ToString(),
                                                                                            CurrentLevel.ToString()));
                            //WorldEntities.ForEach(file.Write());
                        }
                    }
                }
            }
        }
    }
}


