
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace SilentKnight.Model
{
    [TestClass]
    public class SerializationTest
    {
        [TestMethod]
        public void Test_SaveGame_Then_LoadGame()
        {
            GameController ctrl = new GameController();
            Player.Instance.Login("Mike");
            World.Instance.Entities.Clear();
            Skeleton ent1 = new Skeleton(Spawn.Instance.observer, 2, 4, "cat.png",4);
            Skeleton ent2 = new Skeleton(Spawn.Instance.observer, 2, 4, "bat.png",6);
            World.Instance.AddEntity(ent1);
            World.Instance.AddEntity(ent2);
            ctrl.Save("data.txt");
            Assert.IsTrue(World.Instance.Entities.Count == 2);
            World.Instance.Entities.Clear();
            Assert.IsTrue(World.Instance.Entities.Count == 0);
            ctrl.Load("data.txt");
            Assert.IsTrue(World.Instance.Entities.Count == 2);
            Assert.IsTrue(Player.Instance.PlayerName == "Mike");
        }

    }
}



