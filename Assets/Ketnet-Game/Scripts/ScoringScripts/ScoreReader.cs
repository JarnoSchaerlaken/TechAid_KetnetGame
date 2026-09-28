using TMPro;
using UnityEngine;

public class ScoreReader : MonoBehaviour
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
        _minigameScore.text = ScoreRecordKeeper.MinigameScores[_minigameIndex].ToString();
    }
}
