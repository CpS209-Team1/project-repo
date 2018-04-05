using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Model;

class World
{
    public List<Entity> ents = new List<Entity>();

    private static World instance = new World();
    public static World Instance
    {
        get {return instance;}
    }

    public void AddEnt(Entity ent)
    {
        ents.Add(ent);
    }

    public void Print()
    {
        Console.WriteLine(String.Format("Player: {0} at ({1},{2})",
                                                                Player.Instance.Name,
                                                                Player.Instance.PlayerLoc.X,
                                                                Player.Instance.PlayerLoc.Y));
        Console.WriteLine(String.Format("World Entities:"));
        foreach (Entity ent in ents)
        {
            Console.WriteLine(String.Format("\t-> {0} at ({1},{2})",
                                                                ent.Type,
                                                                ent.EntLoc.X,
                                                                ent.EntLoc.Y));
        }
    }

    public int plrAttributes = 2;
    public void Load(string filename)
    {
        string line;
        using (StreamReader rd = new StreamReader(filename))
        {
            while ((line = rd.ReadLine()) != null)
            {
                if (line == String.Format("@{0}:",Player.Instance.Name))
                {
                    while(((line = rd.ReadLine()) != null) && (!line.Contains("@")))
                    {
                        if (line.Contains("- Player:"))
                        {
                            Player.Instance.Deserialize(rd);
                        }
                        else if (line.Contains("- World:"))
                        {
                            World.Instance.Deserialize(rd);
                        }
                    }
                }
            }
        }
    }

    public void Deserialize(StreamReader rd)
    {
        int numEnts = Convert.ToInt32(rd.ReadLine().Trim().Split(' ')[1]);
        for (int i = 0; i < numEnts; ++i)
        {
            string type = rd.ReadLine().Trim().Split(' ')[1];
            int health = Convert.ToInt32(rd.ReadLine().Trim().Split(' ')[1]);
            string[] loc = rd.ReadLine().Trim().Split(' ')[1].Split(',');
            double x = Convert.ToDouble(loc[0]);
            double y = Convert.ToDouble(loc[1]);
            Entity ent = new Entity(type,x,y);
            World.Instance.AddEnt(ent);
        }
    }
}