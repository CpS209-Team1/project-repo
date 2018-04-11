//-----------------------------------------------------------------------------------------------------------------------------------------------------------
//File:   HighScoreTest.cs
//Desc:   This file contains the validation tests (both valid and invalid) for HighScore().
//-----------------------------------------------------------------------------------------------------------------------------------------------------------

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;
using System.Diagnostics;

namespace SilentKnight.Model
{
    [TestClass]
    public class HighScoreTest
    {
        [TestMethod]
        public void ReadScores_LoadScores_Success()
        {
            HighScore highscores = new HighScore();
            highscores.Reset();
            highscores.LoadScores("HighScoresTestData.txt");
            Assert.IsTrue(highscores.scoreList[0].Name == "Susie");
            Assert.IsTrue(highscores.scoreList[2].Points == 12121);
        }

        [TestMethod]

        public void HighScore_ListSort_Success()
        {
            HighScore highscores = new HighScore();
            highscores.Reset();
            highscores.LoadScores("HighScoresTestData.txt");
            Assert.IsTrue(highscores.scoreList[0].Name == "Susie");
            Assert.IsTrue(highscores.scoreList[2].Points == 12121);
        }

        [TestMethod]

        public void HighScore_SaveIfHighScore_Success()
        {
            HighScore highscores = new HighScore();
            highscores.Reset();
            highscores.LoadScores("HighScoresTestData.txt");
            Player.Instance.PlayerName = "Danny";
            Player.Instance.PlayerScore = 30000;
            highscores.SaveIfHighScore();
            highscores.WriteScores("HighScoresTestData.txt");
            Assert.IsTrue(highscores.scoreList[1].Name == "Danny");
            Assert.IsTrue(highscores.scoreList[2].Points == 12121);
        }
    }
}



