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
        //World.Instance.Load("data.txt");
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
        World.Instance.ents.Clear();
        Entity ent1 = new Entity("cat",2,4);
        World.Instance.AddEnt(ent1);
        Entity ent2 = new Entity("zebro",4,7);
        World.Instance.AddEnt(ent2);
        Entity ent3 = new Entity("cow",5,2);
        World.Instance.AddEnt(ent3);
        Entity ent4 = new Entity("alien",9,25);
        World.Instance.AddEnt(ent4);
        World.Instance.Save("data.txt");
        World.Instance.Print();
        //World.Instance.ents.Clear();
    }
}