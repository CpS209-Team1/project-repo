using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// This file contains part of a state machine
/// </summary>
namespace Model
{
    /// <summary>
    /// This class contains the player's range state logic
    /// </summary>
    class RangedState : IState
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public RangedState() { }

        /// <summary>
        /// This has no function just needs to be in here since it is inherited from IState
        /// </summary>
        public void Update()
        {
        }

        /// <summary>
        /// Handles new input from the user and decides what to do with it
        /// </summary>
        /// <param name="data"></param>
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

        /// <summary>
        /// This has no function just needs to be in here since it is inherited from IState
        /// </summary>
        public void Change()
        {

        }

        /// <summary>
        /// This has no function just needs to be in here since it is inherited from IState
        /// </summary>
        public void Exit()
        {

        }

    }
}
