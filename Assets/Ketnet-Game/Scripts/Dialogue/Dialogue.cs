using UnityEngine;
using UnityEngine.Events;

public class Dialogue : MonoBehaviour
{
    [SerializeField]bool _talkOnStart;
    [SerializeField]string[] _dialogue;
    public UnityEvent DialogueEndEvent;

    void Start()
    {
        if (_talkOnStart) StartDialogue();
    }

    public void StartDialogue ()
    {
        DialogueBox.Instance.StartDialogue(_dialogue, DialogueEndEvent);
    }
}
