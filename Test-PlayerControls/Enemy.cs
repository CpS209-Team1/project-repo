using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


abstract class Enemy
{
    public int Id { get; set; }

    public int X { get; set; }

    public int Y { get; set; }

    public int Health { get; set; }

    public string Image { get; set; }

    static int nextId;

    public abstract void UpdatePosition();

    public abstract void UpdateHealth();

    public Enemy(int xLoc, int yLoc, string img)
    {
        X = xLoc;
        Y = yLoc;
        Image = img;
        Id = ++nextId;
    }
}

class Goblin : Enemy
{
    public Goblin(int X, int Y, string Image) : base(X, Y, Image)
    {
        Health = 10;
    }

    public override void UpdateHealth()
    {
        Health--;
    }
    public override void UpdatePosition()
    {
        if (X < Player.X)
        {
            X++;
        }
        else if (X > Player.X)
        {
            X--;
        }

        if (Y < Player.Y)
        {
            Y++;
        }
        else if (Y > Player.Y)
        {
            Y--;
        }
    }
}


