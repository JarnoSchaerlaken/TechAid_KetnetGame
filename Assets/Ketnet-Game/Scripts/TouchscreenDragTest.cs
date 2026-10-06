using UnityEngine;
using UnityEngine.UI;

public class TouchscreenDragTest : MonoBehaviour
{
    InputSystem_Actions _inputActions;

    [SerializeField]
    private Image _object;

    private bool _itemHeld = false;

    private bool _isDragging = false;

    private Vector2 _mousePosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        _inputActions = new InputSystem_Actions(); _inputActions.Enable();

        _inputActions.Player.TouchPosition.performed += ctx =>
        {
            if (Vector2.Distance(ctx.ReadValue<Vector2>(), _object.transform.position) <= 100)
            {
                _object.transform.position = ctx.ReadValue<Vector2>();
                _itemHeld = true;
            }
        };

        _inputActions.Player.MousePos.performed += ctx =>
        {
            _mousePosition = ctx.ReadValue<Vector2>();
        };

        _inputActions.Player.Click.performed += ctx =>
        {
            if (Vector2.Distance(_mousePosition, _object.transform.position) <= 100)
            {
                _object.transform.position = _mousePosition;
                _isDragging = true;
            }
        };

        _inputActions.Player.Click.canceled += ctx =>
        {
            _isDragging = false;
        };
    }

    // Update is called once per frame
    void Update()
    {
       DropItem();
    }

    private void DropItem()
    {
        if (!_itemHeld && _object.transform.position.y > 0 && !_isDragging)
        {
            _object.transform.position = _object.transform.position - new Vector3(0, 40, 0) * Time.deltaTime;
        }
        _itemHeld = false;
        if (_isDragging)
        {
            _object.transform.position = _mousePosition;
        }

    }
}
