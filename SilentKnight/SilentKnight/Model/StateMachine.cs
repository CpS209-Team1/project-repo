using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{

    public interface IState
    {
        void Update();
        void HandleInput(string data);

        void Change();
        void Exit();
    }

    public class StateMachine
    {
        public Dictionary<string, IState> stateDict = new Dictionary<string, IState>();
        public IState currentState = new EmptyState();

        public IState Current { get { return currentState; } }
        public void Add(string id, IState state) { stateDict.Add(id, state); }
        public void Remove(string id) { stateDict.Remove(id); }
        public void Clear() { stateDict.Clear(); }

        public StateMachine()
        {
            Add("melee", new MeleeState());
            Add("ranged", new RangedState());
        }

        public void Change(string id)
        {
            currentState.Exit();
            IState nextState = stateDict[id];
            nextState.Change();
            currentState = nextState;
        }

        public void Update()
        {
            currentState.Update();
        }

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
