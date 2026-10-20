using TMPro;
using UnityEngine;

public class ScoreReader : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _minigameScore;

    [SerializeField]
    private int _minigameIndex;

    [SerializeField]bool _isHighScore;

    void Update()
    {
        if (_isHighScore)
        {
            _minigameScore.text = ScoreManager.GetHighScore(_minigameIndex).ToString();
        }
        else
        {
            _minigameScore.text = ScoreManager.GetScore(_minigameIndex).ToString();
        }
    }
}
