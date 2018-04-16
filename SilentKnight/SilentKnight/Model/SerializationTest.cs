
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
        public void Test_LoadGame()
        {
            GameController ctrl = new GameController();
            Player.Instance.Login("Mike");
            ctrl.Load("data.txt");
            World.Instance.Entities.Clear();
            Skeleton ent1 = new Skeleton(Spawn.Instance.observer, 2, 4, "cat.png",4);
            Skeleton ent2 = new Skeleton(Spawn.Instance.observer, 2, 4, "bat.png",6);
            World.Instance.AddEntity(ent1);
            World.Instance.AddEntity(ent2);
            ctrl.Save("data.txt");
            ctrl.Print();
            World.Instance.Entities.Clear();
        }

    }
}



