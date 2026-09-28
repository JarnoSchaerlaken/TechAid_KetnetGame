using UnityEngine;

public partial class OverWorldPlayer 
{
    public class OWPFSM : FiniteStateMachine
    {
        public OverWorldPlayer Context;

        public OWPStateIdle IdleState;
        public OWPStateMoving MovingState;

        public OWPFSM (OverWorldPlayer context)
        {
            Context = context;

            IdleState = new OWPStateIdle(this);
            MovingState = new OWPStateMoving(this);

            ChangeTo(IdleState);
        }
    }
}
