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

    Quaternion _rotation
    {
        get { return transform.rotation; }
        set
        {
            transform.rotation = value;
        }
    }

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
        _height = _planetRadius;
        _rotation = _currentNode.transform.rotation;
    }

    void Update()
    {
        _fsm.Update(Time.deltaTime);
    }
}
