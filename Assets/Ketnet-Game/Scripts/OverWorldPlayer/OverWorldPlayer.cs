using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class OverWorldPlayer : MonoBehaviour
{
    [SerializeField]float _moveTime;
    [SerializeField]float _jumpHeight;
    [SerializeField]float _planetRadius;
    [SerializeField]GameObject _arrowObject;
    [SerializeField]Transform _modelPivot;
    [SerializeField]List<PathNode> _nodeList = new();
    static Dictionary<int, int> _positionMemory = new();
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

        if (_positionMemory.Keys.Contains(SceneManager.GetActiveScene().buildIndex))
        {
            _currentNode = _nodeList[_positionMemory[SceneManager.GetActiveScene().buildIndex]];
        }
    }

    void Start()
    {
        _fsm = new(this);

        transform.position = _currentNode.transform.position;
        transform.rotation = _currentNode.transform.rotation;       
    } 

    void Update()
    {
        _fsm.Update(Time.deltaTime);
    }
}
