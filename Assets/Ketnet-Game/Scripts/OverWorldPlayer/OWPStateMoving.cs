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

            Vector3 A = Context._currentNode.transform.position;
            Vector3 B = Target.transform.position;

            Vector3 AtoB = B - A;
            Vector3 Atangent = Vector3.Cross(Vector3.Cross(Context._currentNode.transform.up, AtoB.normalized), Context._currentNode.transform.up);
            Vector3 Btangent = Vector3.Cross(Vector3.Cross(Target.transform.up, -AtoB.normalized), Target.transform.up);

            Vector3 p1 = Vector3.Lerp(A, A + Atangent, smoothStepT);
            Vector3 p2 = Vector3.Lerp(B + Btangent, B, smoothStepT);
            Context.transform.position = Vector3.Lerp(p1, p2, smoothStepT);

            Quaternion rotation = Quaternion.Lerp(Context._currentNode.transform.rotation, Target.transform.rotation, smoothStepT);
            Context.transform.rotation = rotation;

            Context._height = TToParabola(smoothStepT) * Context._jumpHeight;

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
