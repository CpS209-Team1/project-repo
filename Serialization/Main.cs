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
        Player.Instance.Login("Mike");
        World.Instance.Load("data.txt");
        World.Instance.Print();
        World.Instance.ents.Clear();

        Player.Instance.Login("Sam");
        World.Instance.Load("data.txt");
        World.Instance.Print();
        World.Instance.ents.Clear();

        Player.Instance.Login("Nick");
        World.Instance.Save("data.txt");
        World.Instance.Print();
        World.Instance.ents.Clear();
    }
}