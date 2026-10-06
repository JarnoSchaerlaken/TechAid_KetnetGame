using UnityEngine;

public class MinigameTest : MonoBehaviour
{
    [SerializeField]int _minigameIndex;
    [SerializeField]int _newScore;

    void Start()
    {
        ScoreManager.UpdateScore(_newScore, _minigameIndex);
    }
}
