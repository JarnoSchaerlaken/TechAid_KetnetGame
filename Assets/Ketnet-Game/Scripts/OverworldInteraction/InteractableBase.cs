using UnityEngine;

public class InteractableBase : MonoBehaviour
{
    virtual public void Interact()
    {
        // The interactable object does require a collider to function

        // This method can be overridden by derived classes to implement specific interaction behavior.
        Debug.Log("Interacted with " + gameObject.name);
    }
}
