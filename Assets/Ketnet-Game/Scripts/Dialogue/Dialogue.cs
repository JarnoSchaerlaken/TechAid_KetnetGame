using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [SerializeField]string[] _dialogue;

    public void StartDialogue ()
    {
        DialogueBox.Instance.StartDialogue(_dialogue);
    }
}
