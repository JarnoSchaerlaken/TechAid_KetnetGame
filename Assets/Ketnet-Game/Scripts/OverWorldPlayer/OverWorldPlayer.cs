using UnityEngine;

public partial class OverWorldPlayer : MonoBehaviour
{
    [SerializeField]float _moveTime;
    [SerializeField]float _jumpHeight;
    [SerializeField]float _planetRadius;
    [SerializeField]GameObject _arrowObject;
    [SerializeField]Transform _modelPivot;
    PathNode _currentNode;
    OWPFSM _fsm;

    float _height
    {
        get { return _modelPivot.localPosition.y; }
        set
        {
            if (_modelPivot.localPosition.y == value) return;
            _modelPivot.localPosition = new(_modelPivot.localPosition.x, value, _modelPivot.localPosition.z);
        }
    }

    void Awake()
    {
        _currentNode = GameObject.FindGameObjectWithTag("StartNode").GetComponent<PathNode>();
        _fsm = new(this);
    }

    void Start()
    {
        transform.position = _currentNode.transform.position;
    }

    void Update()
    {
        _fsm.Update(Time.deltaTime);
    }
}
