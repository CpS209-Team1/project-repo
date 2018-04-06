//using System;
//using System.IO;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Model
//{
//    class HighScore
//    {
//        public List<Score> scoreList = new List<Score>();

//        /// <summary>
//        /// Determines at the end of a game if the player's score is in the top "n" high scores 
//        /// ("n" determined by maxEntries).  If so, it stores the player's score with the high scores.
//        /// </summary>
//        public void ProcessHighScores(int playersScore, string fileName)
//        {
//            // Score/player name pairs are stored one per line.  The player's score is followed by a hyphen and then 
//            // a number "n" which indicates the numberOfOccurrencesOfThisScore-1.  
//            // This augmented score is then followed by a pipe "|" and then the player's name.

//            int maxEntries = 10;

//            List<string> scoreList = new List<string>();

//            // At the end of the game, load the highScores dictionary from the HighScores.txt file while
//            // simultaneously loading the scores into a scoresList.

//            //ReadScores(ref highScoreDictionary, ref scoreList, fileName);

//            // Sort the list in ascending order.

//            scoreList.Sort();

//            // If ending score is greater than the lowest score in the list, drop the lowest score and add the new score
//            // to the list.

//            // SaveIfHighScore(ref scoreList, ref highScoreDictionary, maxEntries, playersScore);

//            // Re-sort the list in descending order.
//            scoreList.Sort();
//            scoreList.Reverse();

//            // https://stackoverflow.com/questions/3309188/how-to-sort-a-listt-by-a-property-in-the-object?
//            // utm_medium =organic&utm_source=google_rich_qa&utm_campaign=google_rich_qa
//            //           objListOrder.Sort((x, y) => x.OrderDate.CompareTo(y.OrderDate));

//            // Display the HighScores screen, omitting the scores' appended dashes and sequence numbers.
//            //DisplayHighScores(highScoreDictionary, scoreList);

//            // Write the score/playerName pairs into the text file.
//            //WriteScores(highScoreDictionary, fileName);
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="scoreList"></param>
//        /// <param name="fileName"></param>
//        public void LoadScores(string fileName)
//        {
//            // Help source: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/
//            // file -system/how-to-write-to-a-text-file
//            string inputLine;

//            using (StreamReader inputFile = new StreamReader(fileName))
//            {

//                inputLine = inputFile.ReadLine();
//                while (inputLine != null && inputLine != "")
//                {
//                    string[] inputArray = inputLine.Split(' ');
//                    Score playerScore = new Score(inputArray[0], Convert.ToInt32(inputArray[1]));
//                    scoreList.Add(playerScore);
//                    inputLine = inputFile.ReadLine();
//                }
//            }
//        }

//        /// <summary>
//        /// Writes the score/playername pair values out to the HighScores.txt file, one per line
//        /// </summary>
//        /// <param name="highScoreDictionary">A dictionary accessible by the entire class where score/player 
//        /// value pairs are kept.</param>
//        /// <param name="outputFile">The file where the high scores are kept.</param>
//        public void WriteScores(Dictionary<string, string> highScoreDictionary, string fileName)
//        {

//            // Help source: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/
//            // file -system/how-to-read-a-text-file-one-line-at-a-time        
//            using (StreamWriter outputFile = new StreamWriter(fileName))
//            {
//                outputFile.WriteLine("value/playername pair value");
//            }
//        }

//        /// <summary>
//        /// Saves the player's score in the HighScore.txt file if it fits in the top [maxEntries] scores.
//        /// </summary>
//        /// <param name="scoreList">Contains the top scores.  Used for sorting the dictionary</param>
//        /// <param name="maxEntries">The number of top scores being maintained.</param>
//        /// <param name="playersScore">The score the player received for this game.</param>
//        public void SaveIfHighScore(ref List<string> scoreList, ref Dictionary<string, string> highScoreDictionary,
//            int maxEntries, int playersScore)
//        {
//            int lowestScore = Convert.ToInt32(scoreList[0].Substring(0, scoreList[0].IndexOf('-')));

//            if (Convert.ToInt32(playersScore) > lowestScore)
//            {
//                string storableScore = MakeStorableScore(playersScore);
//                if (scoreList.Count >= maxEntries)
//                {
//                    scoreList[0] = storableScore.ToString();
//                    // remove lowestScore from Dictionary
//                    // add scoreList[0]
//                }
//                else
//                {
//                    scoreList.Add(storableScore);
//                    // Display the HighScoreAchieved screen, prompting player for his name.   

//                    // Store storableScore and player name in the Dictionary

//                    // Store this unique score into the Dictionary, 
//                    // along with the player's name.                
//                }
//                // Display the HighScores screen
//            }
//        }

//        /// <summary>
//        /// Looks through the highScoreDictionary to find any existing occurrences of the current score.  If any exist, 
//        /// increments the highest appended sequence number by one, and appends it to the player's score, 
//        /// separated by a hyphen. If no other occurrence of the score exists, a zero is appended to the score.
//        /// </summary>
//        /// <param name="playersScore">The "raw" score of the player (the actual score before a sequence number is added).</param>
//        /// <returns></returns>
//        public string MakeStorableScore(int playersScore)
//        {
//            // still needs to be finished
//            string storableScore = "";
//            return storableScore;
//        }

//        /// <summary>
//        /// Displays the HighScores screen, showing the top [maxEntries] scores along with the scoring players' names.
//        /// </summary>
//        /// <param name="highScoreDictionary">A dictionary accessible by the entire class where score/player 
//        /// value pairs are kept.</param>
//        /// <param name="scoreList">A list accessible by the entire class that is used for sorting the 
//        /// scoreDictionary scores.</param>
//        /// <returns></returns>
//        public string DisplayHighScores(Dictionary<string, string> highScoreDictionary, List<string> scoreList)
//        {
//            string storableScore = "";
//            return storableScore;
//        }
//    }

//    class Score
//    {
//        private string name;
//        private int points;

//        public Score(string newName, int newPoints)
//        {
//            name = newName;
//            points = newPoints;
//        }

//        /// <summary>
//        /// The Name property for the Score object
//        /// </summary>
//        public string Name { get; set; }

//        /// <summary>
//        /// The Points property for the Score objec
//        /// </summary>
//        public int Points { get; set; }
//    }


//}
