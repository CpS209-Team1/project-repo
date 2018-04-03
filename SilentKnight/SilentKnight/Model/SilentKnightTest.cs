using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Model
{
    [TestClass]
    class SilentKnightTest
    {
        [TestMethod]
        public void TestEnemySpawn()
        {
            Spawn.Instance.DoSpawn(3);
            Assert.IsTrue(World.Instance.Entities.Count == 3);
        }

        [TestMethod]
        public void TestRemovePlayerHealth()
        {
            Player.Instance.RemovePlayerHealth(1);
            Assert.IsTrue(Player.Instance.Health == 9);
        }

        [TestMethod]
        public void TestAddPlayerHealth()
        {
            Player.Instance.AddPlayerHealth(1);
            Assert.IsTrue(Player.Instance.Health == 10);
        }
    }
}
