using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// This file contains logic for the state machine
/// </summary>
namespace Model
{
    /// <summary>
    /// This interface is for the state machine
    /// </summary>
    public interface IState
    {
        void Update();
        void HandleInput(string data);

        void Change();
        void Exit();
    }

    /// <summary>
    /// This class controlls the player's state and the state machine
    /// </summary>
    public class StateMachine
    {
        public Dictionary<string, IState> stateDict = new Dictionary<string, IState>(); // Contains the possible states
        public IState currentState = new EmptyState(); // contains the current state of the player

        public IState Current { get { return currentState; } }

        /// <summary>
        /// Adds the `state` to `stateDict`
        /// </summary>
        /// <param name="id">state id</param>
        /// <param name="state">Player's state</param>
        public void Add(string id, IState state) { stateDict.Add(id, state); }

        /// <summary>
        /// Removes the specified state by `id` from stateDict
        /// </summary>
        /// <param name="id">Reference to the state's key in `stateDict`</param>
        public void Remove(string id) { stateDict.Remove(id); }

        /// <summary>
        /// Clears `stateDict`
        /// </summary>
        public void Clear() { stateDict.Clear(); }

        /// <summary>
        /// Constructor
        /// </summary>
        public StateMachine()
        {
            Add("melee", new MeleeState());
            Add("ranged", new RangedState());
        }

        /// <summary>
        /// Changes the current state
        /// </summary>
        /// <param name="id">state's id</param>
        public void Change(string id)
        {
            currentState.Exit();
            IState nextState = stateDict[id];
            nextState.Change();
            currentState = nextState;
        }

        /// <summary>
        /// Updates the state machine
        /// </summary>
        public void Update()
        {
            currentState.Update();
        }

        /// <summary>
        /// Handles player input
        /// </summary>
        /// <param name="data">specified action to do</param>
        public void HandleInput(string data)
        {
            if(currentState is EmptyState)
            {
                Change(data);
                Console.WriteLine(data);
            }
            currentState.HandleInput(data);
        }
    }


    /// <summary>
    /// This class is meant to be null for defaulting to
    /// </summary>
    public class EmptyState : IState
    {
        StateMachine statemachine;
        public EmptyState()
        {

        }
        public void Update()
        {

        }
        public void HandleInput(string data)
        {
            switch (data)
            {
                case "melee":
                    statemachine.Change("melee");
                    break;

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
