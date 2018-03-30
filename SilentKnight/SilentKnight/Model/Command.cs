using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    abstract class Command
    {
        public abstract void Execute();
    }

    /// <summary>
    /// Creates new enemies
    /// </summary>
    class DoCreate : Command
    {
        Enemy enemy;
        /// <summary>
        /// Takes `entType` to determine the Enemy type
        /// This then creates a new Enemey entity
        /// </summary>
        /// <param name="entType"></param>
        public DoCreate(string entType)
        {
            switch(entType)
            {
                case "Skeleton":
                    enemy = new Skeleton();
                    break;
            }
        }
        /// <summary>
        /// Adds the enemy to the list
        /// </summary>
        public override void Execute()
        {
            World.Instance.Entities.Add(enemy);
        }
    }
}
