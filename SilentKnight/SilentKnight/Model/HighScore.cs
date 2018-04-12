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
                    outputFile.WriteLine(scoreList[i].Name + " " + scoreList[i].Points);
                }
            }
        }

        public void SaveIfHighScore()
        {
            Score playerScore = new Score(Player.Instance.PlayerName, Player.Instance.PlayerScore);

            scoreList.Add(playerScore);

            // This sort algorithm is derived from the selection sort in the textbook:
            // An Introduction to Computer Science with C# Spring 2012 Edition
            // by David J Eck and Stephen Schaub 

            // Sort the list into decreasing order, using selection sort
            for (int lastPlace = scoreList.Count - 1; lastPlace > 0; lastPlace--)
            { // Find the smallest item  in the list
              // and move it into position lastPlace 
              // by swapping it with the number that is currently 
              // in position lastPlace.
                int minLoc = 0; // Location of smallest item seen so far.
                for (int j = 1; j <= lastPlace; j++)
                {
                    if (scoreList[j].Points < scoreList[minLoc].Points)
                    { // Since the list[j] is smaller than the minimum we've seen
                      // so far, j is the new location of the minimum value 
                      // we've seen so far. 
                      minLoc = j;
                    }
                }
                // Swap largest item with scoreLIst[lastPlace].
                int tempPoints = scoreList[minLoc].Points;  
                string tempName = scoreList[minLoc].Name;
                scoreList[minLoc].Points = scoreList[lastPlace].Points;
                scoreList[minLoc].Name = scoreList[lastPlace].Name;

                scoreList[lastPlace].Points = tempPoints;
                scoreList[lastPlace].Name = tempName;
            } // end of for loop

            // Remove any high scores entries beyond the set
            // maximum number of entries 
            while (scoreList.Count > maxEntries)
            {
                scoreList.RemoveAt(scoreList.Count - 1);
            }
        }

        public void Reset()
        {
            using (StreamWriter outputFile = new StreamWriter("HighScoresTestData.txt"))
            {
                outputFile.WriteLine("Susie 1000");
                outputFile.WriteLine("John 40000");
                outputFile.WriteLine("Bobby 12121");
                outputFile.WriteLine("Fred 1000");
            }
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

        override public string ToString()
        {
            return String.Format("{0}—{1}", Name, Points);
        }
    }


}



