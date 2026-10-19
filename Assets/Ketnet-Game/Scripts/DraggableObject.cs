using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableObject : MonoBehaviour
{
    InputSystem_Actions _inputActions;
    private Vector2 _mousePosition;
    Camera _cam;

    bool _clicking;
    [HideInInspector]public bool Clicking
    {
        get { return _clicking; }
        set
        {
            //if (_clicking == value) return;
            _clicking = value;

            if (value) OnClick();
            else OnStopClick();
        }
    }

    bool _dragging;
    [HideInInspector]public bool Dragging
    {
        get {return _dragging;}
        set
        {
            if (_dragging == value) return;
            _dragging = value;

            if (value) OnStartDrag();
            else OnStopDrag();
        }
    }

    void Awake()
    {
        _inputActions = new();

        _inputActions.Player.MousePos.performed += ctx => _mousePosition = ctx.ReadValue<Vector2>();

        _inputActions.Player.Click.performed += ctx => Clicking = true;
        _inputActions.Player.Click.canceled += ctx => Clicking = false;

        _inputActions.Player.Touch.performed += ctx => Clicking = true;
        _inputActions.Player.Touch.canceled += ctx => Clicking = false;
        _inputActions.Player.TouchPosition.performed += ctx =>
        {
            _mousePosition = ctx.ReadValue<Vector2>();
        };

        _cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void StartClick ()
    {
        Clicking = true;
    }

    void OnEnable()
    {
        _inputActions.Enable();
    }

    void OnDisable()
    {
        _inputActions.Disable();
    }

    public virtual void Start() {}

    public virtual void Update()
    {
        if (Dragging)
        {
            Vector3 worldMousePos = _cam.ScreenToWorldPoint(_mousePosition);
            worldMousePos.z = 0;
            transform.position = worldMousePos;
        }
    }

    void OnClick ()
    {
        Dragging = IsMouseOverSprite();
    }

    void OnStopClick ()
    {
        Dragging = false;
    }

    public virtual void OnStartDrag () {}

    public virtual void OnStopDrag () {}

    bool IsMouseOverSprite ()
    {
        Vector3 worldMousePos = _cam.ScreenToWorldPoint(_mousePosition);
        worldMousePos.z = 0;

        Vector3 delta = worldMousePos - transform.position;

        return Mathf.Abs(delta.x) < 0.5f * transform.lossyScale.x && Mathf.Abs(delta.y) < 0.5f * transform.lossyScale.y;
    }
}
