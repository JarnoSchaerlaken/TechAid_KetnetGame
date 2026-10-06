using System;
using UnityEngine;

public class PathArrow : MonoBehaviour
{
    [SerializeField]Collider _collider;
    [SerializeField]Transform _modelPivot;
    Camera _cam;

    [SerializeField]float _hoveredScale;
    bool _isHoveredValue;
    bool _isHovered
    {
        get {return _isHoveredValue;}
        set
        {
            if (_isHoveredValue == value) return;
            _isHoveredValue = value;

            if (value) _modelPivot.localScale = _hoveredScale * Vector3.one;
            else _modelPivot.localScale = Vector3.one;
        }
    }
    public EventHandler ArrowClicked;

    InputSystem_Actions _actions;
    Vector2 _mousePos;

    void Awake()
    {
        _actions = new();
        

        _actions.Player.TouchPosition.performed += ctx =>
        {
            Ray ray = _cam.ScreenPointToRay(ctx.ReadValue<Vector2>());
            if (_collider.Raycast(ray, out RaycastHit hitInfo, 99) && _actions.Player.Click.WasPerformedThisFrame())
            {
                ArrowClicked?.Invoke(this, EventArgs.Empty);
            }
        };
        _actions.Player.MousePos.performed += ctx => _mousePos = ctx.ReadValue<Vector2>();
        _actions.Player.Click.performed += ctx =>
        {
            if (_isHovered)
            {
                ArrowClicked?.Invoke(this, EventArgs.Empty);
            }
        };
    }

    void OnEnable()
    {
        _actions.Enable();
    }

    void OnDisable()
    {
        _actions.Disable();
    }

    void Start()
    {
        _cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        Ray ray = _cam.ScreenPointToRay(_mousePos);
        //Debug.Log(_mousePos);
        if (_collider.Raycast(ray, out RaycastHit hitInfo, 99)) _isHovered = true;
        else _isHovered = false;
    }
}
