using UnityEngine;

public partial class OverWorldPlayer 
{
    public class OWPStateBase : IState
    {
        public OWPFSM FSM;
        public OverWorldPlayer Context => FSM.Context;

        public OWPStateBase (OWPFSM fsm)
        {
            FSM = fsm;
        }

        public virtual void OnEnter() {}

        public virtual void OnExit() {}

        public virtual void Update(float deltaTime) {}
    }
}
