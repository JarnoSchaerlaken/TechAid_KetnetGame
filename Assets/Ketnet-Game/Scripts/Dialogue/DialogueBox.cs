using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{
    public static DialogueBox Instance;
    [SerializeField]float _characterWriteTime;
    float _timer;
    [SerializeField]TMP_Text _textField;
    [SerializeField]Image _faceImage;
    [SerializeField]GameObject _dialogueBoxObject;
    List<string> _currentDialogue;
    UnityEvent _dialogueEndEvent;
    string _currentLine = "";
    int _currentLineIndex;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer < 0 && _currentLineIndex < _currentLine.Length)
        {
            _textField.text += _currentLine[_currentLineIndex];
            _currentLineIndex++;

            _timer = _characterWriteTime;
        }
    }

    public void StartDialogue (string[] dialogue, Sprite faceSprite, UnityEvent endEvent)
    {
        if (dialogue.Length > 0) 
        {
            _currentDialogue = dialogue.ToList();
            _currentLine = dialogue[0];

            _faceImage.sprite = faceSprite;

            _dialogueBoxObject.SetActive(true);

            _currentLineIndex = 0;

            _dialogueEndEvent = endEvent;
        }
    }

    public void OnNextPressed ()
    {
        _textField.text = "";
        _currentLineIndex = 0;

        if (_currentDialogue.Count <= 1)
        {
            if (_dialogueEndEvent != null) _dialogueEndEvent.Invoke();
            _dialogueBoxObject.SetActive(false);
            _currentDialogue.Clear();
            _currentLine = "";
        }
        else
        {
            _currentDialogue.RemoveAt(0);
            _currentLine = _currentDialogue[0];
        }
    }
}
