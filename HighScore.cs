using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_PlayerControls
{
    class HighScores
    {
        /// <summary>
        /// 
        /// </summary>
        public void ProcessHighScores(int playersScore)
        {
            // Score/player name pairs are stored one per line.  The player's score is followed by a dash and then 
            // a number "n" which indicates the numberOfOccurrencesOfThisScore-1.  
            // This augmented score is then followed by a pipe "|" and then the player's name.

            int maxEntries = 10;
            Dictionary<string, string> scoreDictionary = new Dictionary<string, string>();
            List<string> scoreList = new List<string>();

            // At the end of the game, load the highScores dictionary from the HighScores.txt file while
            // simultaneously loading the scores into a scoresList.
           
            ReadScores(ref scoreDictionary, ref scoreList);

            // Sort the list in ascending order.

            scoreList.Sort();

            // If ending score is greater than the lowest score in the list, drop the lowest score and add the new score
            // to the list.

            SaveIfHighScore(ref scoreList, maxEntries, playersScore);


            // Look through the dictionary, find the last occurrence of this score, if any, 
            // and add the next sequential number onto this score.  Store this unique score into the Dictionary, 
            // along with the player's name.

            // Resort the list in descending order.

            // Display the HighScores screen, omitting the scores' appended dashes and sequence numbers.

            // Write the score/playerName pairs into the text file.
            WriteScores(ref scoreDictionary, ref scoreList);
        }
        
        public void ReadScores(ref Dictionary<string, string> scoreDictionary, ref List<string> scoreList)
        {
            // Help source: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/
            // file -system/how-to-write-to-a-text-file
            string line;
            System.IO.StreamReader inputFile = new System.IO.StreamReader(@"HighScores.txt");
            while ((line = inputFile.ReadLine()) != null)
            {
                System.Console.WriteLine(line);
            }
            inputFile.Close();
        }

        public void WriteScores(ref Dictionary<string, string> scoreDictionary, ref List<string> scoreList)
        {

            // Help source: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/
            // file -system/how-to-read-a-text-file-one-line-at-a-time        
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"HighScores.txt"))
            {
                file.WriteLine("hello!");
            }
        }

        public void SaveIfHighScore(ref List<string> scoreList, int maxEntries, int playersScore)
        {
            int lowestScore = Convert.ToInt32(scoreList[0].Substring(0, scoreList[0].IndexOf('-')));

            if (Convert.ToInt32(playersScore) > lowestScore)
            {
                string storableScore = MakeStorableScore(playersScore);
                if (scoreList.Count >= maxEntries)
                {
                    scoreList[0] = storableScore.ToString();
                    // remove lowestScore from Dictionary
                    // add scoreList[0]
                }
                else
                {
                    scoreList.Add(storableScore);
                    // Display the HighScoreAchieved screen, prompting player for his name.   
                    
                    // Store storableScore and player name in the Dictionary
                }
                // Display the HighScores screen
            }
        }

        public string MakeStorableScore(int playersScore)
        {
            string storableScore = "";
            return storableScore;
        }
    }


}
