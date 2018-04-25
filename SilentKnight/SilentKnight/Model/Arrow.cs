using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// This file contains the Arrow class
/// </summary>
namespace Model
{
    /// <summary>
    /// This class class contains a custom event handler and also controls the arrow's behavior
    /// </summary>
    class Arrow
    {
        public event EventHandler<int> ArrowMovedEvent; // Define arrow moved event
        public event EventHandler<int> ArrowKilledEvent; // Define arrow killed event
        public event EventHandler<int> ArrowSpawnEvent; // Define arrow spawn event
        public Location ArrowLocation; // Contains the arrow's location in the world
        public Direction ArrowDirection; // Contains the arrow's direction

        /// <summary>
        /// This is the constructor
        /// </summary>
        /// <param name="x">Arrow's x pos</param>
        /// <param name="y">Arrow's y pos</param>
        /// <param name="direction">Arrow's direction</param>
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

        /// <summary>
        /// Updates the arrow's position and if the arrow is out of the game bounds it kills the arrow
        /// </summary>
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

            if (ArrowMovedEvent != null) 
                ArrowMovedEvent(this, 0);   
        }

        /// <summary>
        /// Removes arrow from the game
        /// </summary>
        public void Killed()
        {
            if (ArrowKilledEvent != null) 
                ArrowKilledEvent(this, 0);   
        }

        /// <summary>
        /// Adds an arrow to the game
        /// </summary>
        public void Spawn()
        {
            if (ArrowSpawnEvent != null)  // Is anyone subscribed?
                ArrowSpawnEvent(this, 0);   // Raise event
        }

        /// <summary>
        /// Removes the arrow `i` from the game
        /// </summary>
        /// <param name="i">Instance of an arrow</param>
        public void KillArrow(Arrow i)
        {

                World.Instance.EntitiesArrow.Remove(i);
            
        }

        /// <summary>
        /// Serialization for the arrow
        /// </summary>
        /// <returns></returns>
        public List<string> Serialize()
        {
            List<string> world = new List<string>();
            world.Add(String.Format("\t\t\t\tLocation: {0},{1}", ArrowLocation.X, ArrowLocation.Y));
            world.Add(String.Format("\t\t\t\tDirection: {0}", ArrowDirection));
            return world;
        }
    }
}
