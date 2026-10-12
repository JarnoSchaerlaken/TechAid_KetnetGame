using System;
using TMPro;
using UnityEngine;

public class GenericButton : MonoBehaviour
{
    public static GenericButton Instance;
    [SerializeField]TMP_Text _text;
    public EventHandler ButtonPressed;

    void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    public void SetupButton (string text)
    {
        gameObject.SetActive(true);
        _text.text = text;
    }

    public void HideButton ()
    {
        gameObject.SetActive(false);
        _text.text = "";
    }

    public void PressButton ()
    {
        ButtonPressed?.Invoke(this, EventArgs.Empty);
    }
}
