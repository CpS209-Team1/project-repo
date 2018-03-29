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

    class DoCreate : Command
    {
        Enemy enemy;
        public DoCreate(string entType)
        {
            switch(entType)
            {
                case "Skeleton":
                    enemy = new Skeleton();
                    break;
            }
        }
        public override void Execute()
        {
            World.Instance.Entities.Add(enemy);
        }
    }
}
