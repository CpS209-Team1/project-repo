using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    class RangedState : IState
    {
        public RangedState() { }
        public void Update()
        {
        }
        public void HandleInput(string data)
        {
            if (data == "ranged" && Player.Instance.PlayerCoolDown == 0)
            {
                Update();
            }
            else if (data == "melee" && Player.Instance.PlayerCoolDown == 0)
            {
                Console.WriteLine("TESTasdfaeswcwawefewasad");
                Player.Instance.PlayerState.Change("melee");
            }

        }

        public void Change()
        {

        }
        public void Exit()
        {

        }

    }
}
