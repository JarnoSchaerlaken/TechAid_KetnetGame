using TMPro;
using UnityEngine;

public class ScoreReader : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _minigameScore;

    [SerializeField]
    private int _minigameIndex;

    void Update()
    {
        _minigameScore.text = ScoreManager.GetScore(_minigameIndex).ToString();
    }
}
