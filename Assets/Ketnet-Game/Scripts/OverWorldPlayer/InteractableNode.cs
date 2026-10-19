using UnityEngine;
using UnityEngine.Events;

public class InteractableNode : MonoBehaviour
{
    [SerializeField]string _buttonText;
    [SerializeField]UnityEvent _onPressButtonEvent;
    bool _nodeEntered;

    public void OnEnterNode ()
    {
        if (GenericButton.Instance != null)
        {
            GenericButton.Instance.SetupButton(_buttonText);
            GenericButton.Instance.ButtonPressed += (s, e) => _onPressButtonEvent.Invoke();
            GenericButton.Instance.ButtonPressed += (s, e) => OnButtonPressed();
            _nodeEntered = true;
        }
    }

    public void OnLeaveNode ()
    {
        if (_nodeEntered) 
        {
            GenericButton.Instance.HideButton();
            GenericButton.Instance.ButtonPressed -= (s, e) => _onPressButtonEvent.Invoke();
            GenericButton.Instance.ButtonPressed -= (s, e) => OnButtonPressed();
            _nodeEntered = false;
        }
    }

    public virtual void OnButtonPressed ()
    {
        
    }
}
