using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Model;

class Entity
{
    public string Type { get; set; }
    public int Health { get; set; }
    public Location EntLoc;

    public Entity(string type, double x, double y)
    {
        Type = type;
        EntLoc.X = x;
        EntLoc.Y = y;
        Health = 10;
    }

    public List<string> Serialize()
    {
        List<string> world = new List<string>();
        world.Add(String.Format("\t\t\tType: {0}",Type));
        world.Add(String.Format("\t\t\t\tHealth: {0}",Health));
        world.Add(String.Format("\t\t\t\tLocation: {0},{1}",EntLoc.X.ToString(),EntLoc.Y.ToString()));
        return world;
    }

    public void Deserialize(string filenme)
    {

    }
}