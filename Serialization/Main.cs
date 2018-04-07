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
        Player.Instance.Login("Sam","data.txt");
        World.Instance.Load("data.txt");
        World.Instance.Print();
        // World.Instance.AddUserData("data.txt");
        // World.Instance.Print();
        // World.Instance.ents.Clear();

        // Player.Instance.Login("Sam");
        // World.Instance.Load("data.txt");
        // World.Instance.Print();
        // World.Instance.ents.Clear();

        // Player.Instance.Login("Sam","data.txt");
        // World.Instance.RemovePlayerData("data.txt");
        Entity ent1 = new Entity("moose",2,4);
        World.Instance.AddEnt(ent1);
        Entity ent2 = new Entity("zebra",4,7);
        World.Instance.AddEnt(ent2);
        Entity ent3 = new Entity("cow",5,2);
        World.Instance.AddEnt(ent3);
        World.Instance.Save("data.txt");
        World.Instance.Print();
        //World.Instance.ents.Clear();
    }
}