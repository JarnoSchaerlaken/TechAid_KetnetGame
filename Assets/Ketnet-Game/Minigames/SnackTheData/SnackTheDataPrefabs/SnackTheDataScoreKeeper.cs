using TMPro;
using UnityEngine;

namespace SnackTheData{
    public class SnackTheDataScoreKeeper : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _scoreText;

        [SerializeField]
        private TextMeshProUGUI _endScoreText;

        private static int _score;

        [SerializeField]
        private int _snackTheDataIndex = 0;

        [SerializeField]
        private int _dataPointThreshold = 1000; // needs to be a multiplication of the score given by the pickups

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _score = 0;
        }

        // Update is called once per frame
        void Update()
        {
            _scoreText.text = _score.ToString("D7");

            _endScoreText.text = _score.ToString("D7");

            if (_score == _dataPointThreshold)
            {
                NavigationManager.UpdateDataPoints(true, _snackTheDataIndex);
            }
        }

        public static void AddScore(int score)
        {
            _score += score;
        }
    }
}
