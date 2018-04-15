using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    class Arrow
    {
        public event EventHandler<int> ArrowMovedEvent; // Define event
        public event EventHandler<int> ArrowKilledEvent;
        public event EventHandler<int> ArrowSpawnEvent;
        public Location ArrowLocation;
        public Direction ArrowDirection;
        public static int id = 0;
        Random rand = new Random();

        public Arrow(double x, double y, string direction)
        {
            switch (direction)
            {
                case "Down":
                    ArrowDirection = Direction.Down;
                    break;
                case "Up":
                    ArrowDirection = Direction.Up;
                    break;
                case "Left":
                    ArrowDirection = Direction.Left;
                    break;
                case "Right":
                    ArrowDirection = Direction.Right;
                    break;
            }
            ArrowLocation.X = x;
            ArrowLocation.Y = y;

        }

        public void Update()
        {
            if(ArrowDirection == Direction.Left)
            {
                ArrowLocation.X -= 10;
            }
            else if (ArrowDirection == Direction.Right)
            {
                ArrowLocation.X += 10;
            }
            else if(ArrowDirection == Direction.Up)
            {
                ArrowLocation.Y -= 10;
            }
            else if (ArrowDirection == Direction.Down)
            {
                ArrowLocation.Y += 10;
            }
            if(ArrowLocation.X > World.Instance.borderRight || ArrowLocation.X < 0 || ArrowLocation.Y > World.Instance.borderBottom || ArrowLocation.Y <  0)
            {
                Killed();
            }


            if (ArrowMovedEvent != null)  // Is anyone subscribed?
                ArrowMovedEvent(this, 0);   // Raise event

        }
        public void Killed()
        {


            if (ArrowKilledEvent != null)  // Is anyone subscribed?
                ArrowKilledEvent(this, 0);   // Raise event

        }
        public void Spawn()
        {


            if (ArrowSpawnEvent != null)  // Is anyone subscribed?
                ArrowSpawnEvent(this, 0);   // Raise event

        }
        public void KillArrow(Arrow i)
        {

                World.Instance.EntitiesArrow.Remove(i);
            
        }
        public List<string> Serialize()
        {
            List<string> world = new List<string>();
            world.Add(String.Format("\t\t\t\tLocation: {0},{1}", ArrowLocation.X, ArrowLocation.Y));
            world.Add(String.Format("\t\t\t\tDirection: {0}", ArrowDirection));
            return world;
        }
    }
}
