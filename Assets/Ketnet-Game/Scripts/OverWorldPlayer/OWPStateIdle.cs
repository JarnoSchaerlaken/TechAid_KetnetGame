using System.Collections.Generic;
using UnityEngine;

public partial class OverWorldPlayer 
{
    public class OWPStateIdle : OWPStateBase
    {
        public OWPStateIdle(OWPFSM fsm) : base(fsm) {}

        List<GameObject> _arrows = new();

        public override void OnEnter()
        {
            _arrows.Clear();
            foreach(PathNode node in Context._currentNode.Conections)
            {
                Vector3 dir = node.transform.position - Context.transform.position;
                Vector3 forward = Vector3.Cross(Context.transform.up, Vector3.Cross(dir.normalized, Context.transform.up));
                Quaternion rotation = Quaternion.LookRotation(forward, Context.transform.up);

                GameObject arrow = Instantiate(Context._arrowObject, Context._modelPivot.position, rotation, Context._modelPivot);
                _arrows.Add(arrow);

                arrow.GetComponent<PathArrow>().ArrowClicked += (s, e) => StartMovingTo(node);
            }

            NavigationManager.SetCurrentNode(Context._currentNode);
            NavigationManager.Instance?.UpdateUI();

            Context._currentNode.OnPlayerLandOnNode.Invoke();

            base.OnEnter();
        }

        public override void OnExit()
        {
            foreach(GameObject arrow in _arrows)
            {
                Destroy(arrow);
            }

            base.OnExit();
        }

        public override void Update(float deltaTime)
        {
            //if (_timer < 0) StartMovingTo(Context._currentNode.Conections[0]);

            base.Update(deltaTime);
        }

        void StartMovingTo (PathNode nextNode)
        {
            Context._currentNode.OnPlayerLeaveNode.Invoke();
            FSM.MovingState.Target = nextNode;
            FSM.ChangeTo(FSM.MovingState);
        }
    }
}
