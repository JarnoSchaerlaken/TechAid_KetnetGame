using System;
using TMPro;
using UnityEngine;

public class GenericButton : MonoBehaviour
{
    public static GenericButton Instance;
    [SerializeField]TMP_Text _text;
    [HideInInspector]public InteractableNode CurrentNode;

    void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    public void SetupButton (string text, InteractableNode currentNode)
    {
        gameObject.SetActive(true);
        CurrentNode = currentNode;
        _text.text = text;
    }

    public void HideButton ()
    {
        gameObject.SetActive(false);
        CurrentNode = null;
        _text.text = "";
    }

    public void PressButton ()
    {
        if (CurrentNode != null)
        {
            CurrentNode.OnButtonPressed();
        }
    }
}
