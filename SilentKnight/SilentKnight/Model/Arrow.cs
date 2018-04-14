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

        public Arrow()
        {
            ArrowDirection = Player.Instance.PlayerDirection;
            ArrowLocation.X = Player.Instance.PlayerLoc.X;
            ArrowLocation.Y = Player.Instance.PlayerLoc.Y;

        }

        public void Update()
        {
            if(ArrowDirection == Direction.Left)
            {
                ArrowLocation.X -= 1;
            }
            else if (ArrowDirection == Direction.Right)
            {
                ArrowLocation.X += 1;
            }
            else if(ArrowDirection == Direction.Up)
            {
                ArrowLocation.Y -= 1;
            }
            else if (ArrowDirection == Direction.Down)
            {
                ArrowLocation.Y += 1;
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
    }
}
