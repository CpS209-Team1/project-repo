using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Model;

class World
{
    public List<Entity> ents = new List<Entity>();
    public int plrAttributes = 2;

    public List<string> Users = new List<string>();

    private static World instance = new World();
    public static World Instance
    {
        get {return instance;}
    }

    public void AddEnt(Entity ent)
    {
        ents.Add(ent);
    }
    
    public bool ValidateUser(string name,string filename)
    {
        string[] file = File.ReadAllText(filename).Split('\n');
        List<string> contents = new List<string>(file);
        foreach (string line in contents)
        {
            if (line.Contains("@"))
            {
                World.Instance.Users.Add(line.Substring(1,line.IndexOf(":") - 1));
            }
        }
        foreach(string user in World.Instance.Users)
        {
            //Console.WriteLine(user);
        }
        return World.Instance.Users.Contains(name);
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

    public void Load(string filename)
    {
        string line;
        using (StreamReader rd = new StreamReader(filename))
        {
            while ((line = rd.ReadLine()) != null)
            {
                //Console.WriteLine(line);
                if (line == String.Format("@{0}:",Player.Instance.Name))
                {
                    while(((line = rd.ReadLine()) != null) && (!line.Contains("@")))
                    {
                        //Console.WriteLine(line);
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

    public void AddUserData(string filename)
    {
        string[] file = File.ReadAllText(filename).Split('\n');
        List<string> outfile = new List<string>(file);
        outfile.RemoveAll(String.IsNullOrWhiteSpace);
        foreach(string line in outfile) { Console.WriteLine(line); }


        File.WriteAllLines(filename, outfile.ToArray(), Encoding.UTF8);
    }

    public void RemovePlayerData(string filename)
    {
        int startInd = 0;
        int endInd = 0;
        string[] file = File.ReadAllText(filename).Split('\n');
        List<string> contents = new List<string>(file);
        contents.RemoveAll(String.IsNullOrWhiteSpace);
        List<string> outfile = contents;

        int size = contents.Count();
        bool found = false;
        int ind = 0;
        foreach(string line in contents)
        {
            string curline = line.Trim();
            if (curline == String.Format("@{0}:",Player.Instance.Name))
            {
                startInd = ind;
                //Console.WriteLine("***Start:" + ind);
                found = true;
            }
            else if (((curline.Contains("@") || (ind == contents.Count - 1)) && found))
            {
                if (curline.Contains("@")) { endInd = ind - 1; }
                else { endInd = ind; }
                //Console.WriteLine("***End:" + ind);
                break;
            }
            ++ind;
        }
        if (found)
        {
            for (int i = startInd; i <= endInd; ++i)
            {
                outfile.RemoveAt(startInd);
            }
            File.WriteAllLines(filename, outfile.ToArray(), Encoding.UTF8);
        }
        if (!found) Console.WriteLine("NOT FOUND");
    }

    public void Save(string filename)
    {
        World.Instance.RemovePlayerData(filename);
        Console.WriteLine("REMOVED");
        string[] file = File.ReadAllText(filename).Split('\n');
        List<string> contents = new List<string>(file);
        contents.RemoveAll(String.IsNullOrWhiteSpace);
        List <string> plr = Player.Instance.Serialize();
        List <string> world = World.Instance.Serialize();
        foreach(string attr in plr) { contents.Add(attr); }
        foreach(string elem in world) { contents.Add(elem); }
        File.WriteAllLines(filename, contents.ToArray(), Encoding.UTF8);
    }

    public List<string> Serialize()
    {
        List<string> world = new List<string>();
        world.Add("\t- World:");
        world.Add(String.Format("\t\tAttributes: ",entNum));
        world.Add(String.Format("\t\t\tEntities: {0}",entNum));
        int entNum = World.Instance.ents.Count();
        world.Add(String.Format("\t\tEntities: {0}",entNum));
        foreach (Entity ent in World.Instance.ents)
        {
            List<string> attrs = ent.Serialize();
            foreach(string attr in attrs) { world.Add(attr); }
        }
        return world;
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