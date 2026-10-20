using UnityEngine;
using UnityEngine.Events;

public class InteractableNode : MonoBehaviour
{
    [SerializeField]string _buttonText;
    [SerializeField]UnityEvent _onPressButtonEvent;

    public void OnEnterNode ()
    {
        GenericButton.Instance.SetupButton(_buttonText, this);
    }

    public void OnLeaveNode ()
    {
        GenericButton.Instance.HideButton();
    }

    public virtual void OnButtonPressed ()
    {
        _onPressButtonEvent.Invoke();
    }
}
