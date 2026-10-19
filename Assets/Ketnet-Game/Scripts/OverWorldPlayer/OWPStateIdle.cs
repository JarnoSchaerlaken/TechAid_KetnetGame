using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class OverWorldPlayer 
{
    public class OWPStateIdle : OWPStateBase
    {
        public OWPStateIdle(OWPFSM fsm) : base(fsm) {}

        List<GameObject> _arrows = new();

        public override void OnEnter()
        {
            Context.transform.position = Context._currentNode.transform.position;
            Context._height = 0;
            Context.transform.rotation = Context._currentNode.transform.rotation;

            //add arrows
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

            Context._currentNode.OnPlayerLandOnNode.Invoke();

            if (_positionMemory.Keys.Contains(SceneManager.GetActiveScene().buildIndex))
            {
                _positionMemory[SceneManager.GetActiveScene().buildIndex] = Context._nodeList.IndexOf(Context._currentNode);
            }
            else
            {
                _positionMemory.Add(SceneManager.GetActiveScene().buildIndex, Context._nodeList.IndexOf(Context._currentNode));
            }

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
