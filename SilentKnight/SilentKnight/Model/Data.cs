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
        public string PlayerName { get; set; }
        public string Difficulty { get; set; }
        public int Points { get; set; }
        public int HighScore { get; set; }
        public int Level { get; set; }
        public int CurrentLevel { get; set; }
        public List<Enemy> WorldEntities { get; set; }

        /// <summary>
        /// Loads player data from a given filename into
        /// the appropriate variables
        /// </summary>
        /// <param name="filename"></param>
        public void Load(string filename)
        {
            using (StreamReader file = new StreamReader(filename));
            {
                while((line = file.ReadLine()) != null)  
                {  
                    string curLine = line.Split("\s:\s");
                    // mbrun138 : medium : 450 : 200 : 2 : 2 : 3,skeleton,skeleton,skeleton
                    if (curLine[0] == PlayerName)
                    {
                        Diff = curLine[1];
                        Points = Convert.ToInt32(curline[2]);
                        HighScore = Convert.ToInt32(curline[3]);
                        Level = Convert.ToInt32(curLine[4]);
                        CurrentLevel = Convert.ToInt32(curline[5]);
                        WorldEntities = curline[6].Split(",");
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
            using (StreamWriter file = new StreamWriter(filename))
            {
                while (line = file.ReadLine()) != null)
                {
                    string curLine = line.Split("\s:\s");
                    if (curline[0] == PlayerName)
                    {
                        file.WriteLine(String.Format("{0} : {1} : {2} : {3} : {4} : {5}",
                                                                                        PlayerName,
                                                                                        Difficulty,
                                                                                        Points.ToString(),
                                                                                        HighScore.ToString(),
                                                                                        Level.ToString(),
                                                                                        CurrentLevel.ToString()));
                        WorldEntities.ForEach(file.Write());
                    }
                }
            }
        }
    }
}
