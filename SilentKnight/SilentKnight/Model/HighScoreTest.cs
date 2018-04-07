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
            highscores.LoadScores("HighScoresTestData.txt");
            Assert.IsTrue(highscores.scoreList[0].Name == "Susie");          
            Assert.IsTrue(highscores.scoreList[2].Points == 12121);
        }

        [TestMethod]
        public void HighScore_ScoreSaved_Success()
        {
        }
    }
}


