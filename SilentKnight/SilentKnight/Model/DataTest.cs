using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;

[TestClass]
public class DataTest
{
    
    [TestMethod]
    public void TestLoad()
    {
        Data dat = new Data();
        dat.Load("./testLoad.txt");
        Assert.IsTrue(dat.PlayerName == "mbrun138");
        Assert.IsTrue(dat.Difficulty == "medium");
        Assert.IsTrue(dat.Points == 450);
        Assert.IsTrue(dat.HighScore == 200);
        Assert.IsTrue(dat.Level == 2);
        Assert.IsTrue(dat.CurrentLevel == 2);
        List<string> val = new List<string>{"3,skeleton,skeleton,skeleton"};
        Assert.IsTrue(dat.WorldEntities.SequenceEqual(val));
    }

    [TestMethod]
    public void TestSave()
    {
        Data dat = new Data();
        dat.Load("./testSave.txt");
        dat.Difficulty = "easy";
        dat.Points = 500;
        dat.Save("./testSave.txt");
        Assert.IsTrue(dat.PlayerName == "mbrun138");
        Assert.IsTrue(dat.Difficulty == "easy");
        Assert.IsTrue(dat.Points == 500);
    }
}
