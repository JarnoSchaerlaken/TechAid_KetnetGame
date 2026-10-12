using UnityEngine;
using UnityEngine.Events;

public class InteractableNode : MonoBehaviour
{
    [SerializeField]string _buttonText;
    [SerializeField]UnityEvent _onPressButtonEvent;

    public void OnEnterNode ()
    {
        GenericButton.Instance.SetupButton(_buttonText);
        GenericButton.Instance.ButtonPressed += (s, e) => _onPressButtonEvent.Invoke();
    }

    public void OnLeaveNode ()
    {
        GenericButton.Instance.HideButton();
        GenericButton.Instance.ButtonPressed -= (s, e) => _onPressButtonEvent.Invoke();
    }
}
