using UnityEngine;

public class InteractionWithWorld : MonoBehaviour
{
    InputSystem_Actions _inputActions;

    private GameObject _touchedObject;

    private Vector2 _mousePos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _inputActions = new InputSystem_Actions();
        _inputActions.Enable();

        _inputActions.Player.TouchPosition.performed += ctx =>
        {
            Ray ray = Camera.main.ScreenPointToRay(ctx.ReadValue<Vector2>());
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 99))
            {
                if (hitInfo.collider.CompareTag("Interactable"))
                {
                    _touchedObject = hitInfo.collider.gameObject;
                }
            }
        };

        _inputActions.Player.MousePos.performed += ctx =>
        {
            _mousePos = ctx.ReadValue<Vector2>();
        };

        _inputActions.Player.Click.performed += ctx =>
        {
            Ray ray = Camera.main.ScreenPointToRay(_mousePos);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 99))
            {
                if (hitInfo.collider.CompareTag("Interactable"))
                {
                    _touchedObject = hitInfo.collider.gameObject;
                }
            }
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (_touchedObject != null)
        {
            _touchedObject.GetComponent<InteractableBase>()?.Interact();
            _touchedObject = null;
        }
    }
}
