using TMPro;
using UnityEngine;

public class DataPointCounter : MonoBehaviour
{
    [SerializeField]TMP_Text _text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _text.text = "Datapunten: " + ScoreManager.DataPoints.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
