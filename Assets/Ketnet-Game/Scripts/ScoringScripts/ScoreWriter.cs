using TMPro;
using UnityEngine;

public class ScoreWriter : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _minigameScore;

    [SerializeField]
    private int _minigameIndex;

    void Update()
    {
        int score = int.Parse(_minigameScore.text);
        ScoreManager.UpdateScore(score, _minigameIndex);
    }
}
