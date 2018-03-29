using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    abstract class Enemy
    {
        public int Health { get; set; }
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string Image { get; set; }

        static int nextId;

        public abstract string GetKind();

        public abstract void UpdatePosition();

        public Enemy()
        {
            Health = 10;
            X = 0;
            Y = 0;
            Image = "skeleton.png";
            Id = ++nextId;
        }
    }

    class Skeleton : Enemy
    {
        public Skeleton()
        {

        }
        public override string GetKind()
        {
            return "Skeleton";
        }
        public override void UpdatePosition()
        {
            throw new NotImplementedException();
        }
    }
}
