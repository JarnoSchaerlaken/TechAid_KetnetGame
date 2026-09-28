using TMPro;
using UnityEngine;

public class ScoreWriter : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _minigameScore;

    [SerializeField]
    private int _minigameIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        int score = int.Parse(_minigameScore.text);
        if (score > ScoreRecordKeeper.MinigameScores[_minigameIndex])
        {
            ScoreRecordKeeper.UpdateScore(score, _minigameIndex);
        }
    }
}
