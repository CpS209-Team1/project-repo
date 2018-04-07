using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

class Test
{
    public static void Main()
    {
        GameController ctrl = new GameController();
        Player.Instance.Login("Bob","data.txt",ctrl);
        ctrl.Load("data.txt");
        ctrl.Print();
        World.Instance.Entities.Clear();
        Skeleton ent1 = new Skeleton(Spawn.Instance.observer,2,4,"cat.png");
        Skeleton ent2 = new Skeleton(Spawn.Instance.observer,2,4,"bat.png");
        World.Instance.AddEntity(ent1);
        World.Instance.AddEntity(ent2);
        ctrl.Save("data.txt");
        ctrl.Print();
        World.Instance.Entities.Clear();
    }
}
