using UnityEngine;

public partial class OverWorldPlayer 
{
    public class OWPStateMoving : OWPStateBase
    {
        public OWPStateMoving(OWPFSM fsm) : base(fsm) {}
        public PathNode Target;

        float _t = 0;

        public override void OnEnter()
        {
            _t = 0;

            base.OnEnter();
        }

        public override void Update(float deltaTime)
        {
            _t += deltaTime / Context._moveTime;
            float smoothStepT = SmoothStep(_t);
            Quaternion rotation = Quaternion.Lerp(Context._currentNode.transform.rotation, Target.transform.rotation, smoothStepT);
            Context._rotation = rotation;

            Context._height = Context._planetRadius + (TToParabola(smoothStepT) * Context._jumpHeight);

            if (_t > 1)
            {
                Context._currentNode = Target;
                FSM.ChangeTo(FSM.IdleState);
            }

            base.Update(deltaTime);
        }

        float SmoothStep (float t)
        {
            return Mathf.Clamp01((3 * t * t) - (2 * t * t * t));
        }

        float TToParabola (float t)
        {
            return 4 * (t - (t * t));
        }
    }
}
