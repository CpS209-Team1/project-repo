using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Model
{
    public class HighScore
    {
        public List<Score> scoreList = new List<Score>();
        public int maxEntries = 10;

        public void LoadScores(string fileName)
        {
            // Help source: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/
            // file -system/how-to-write-to-a-text-file
            string inputLine;

            using (StreamReader inputFile = new StreamReader(fileName))
            {
                int ctr = 0;
                inputLine = inputFile.ReadLine();
                while (inputLine != null && inputLine != "")
                {
                    string[] inputArray = inputLine.Split(' ');
                    Score playerScore = new Score(inputArray[0], Convert.ToInt32(inputArray[1]));
                    scoreList.Add(playerScore);
                    inputLine = inputFile.ReadLine();
                    ctr++;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        public void WriteScores(string fileName)
        {

            // Help source: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/
            // file -system/how-to-read-a-text-file-one-line-at-a-time        
            using (StreamWriter outputFile = new StreamWriter(fileName))
            {
                for(int i = 0; i < scoreList.Count; i++)
                {
                    Console.WriteLine(scoreList[i].Name + " " + scoreList[i].Points);
                }
            }
        }

        public void SaveIfHighScore()
        {
            int playersScore = Player.Instance.PlayerScore;
            scoreList = scoreList.OrderByDescending(o => o.Points).ToList();


        }

        public void DisplayHighScores()
        {
            
        }
    }

    public class Score
    {

        public Score(string newName, int newPoints)
        {
            Name = newName;
            Points = newPoints;
        }

        /// <summary>
        /// The Name property for the Score object
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The Points property for the Score objec
        /// </summary>
        public int Points { get; set; }
    }


}
