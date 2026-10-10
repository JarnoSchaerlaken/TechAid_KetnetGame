using UnityEngine;

public class TestInteractable : InteractableBase
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Interact()
    {
        Debug.LogWarning("This is a test interactable object.");
        base.Interact();
    }
}
