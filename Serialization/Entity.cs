using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }

    public void Serialize(string filename)
    {

    }

    public void Deserialize(string filenme)
    {

    }
}