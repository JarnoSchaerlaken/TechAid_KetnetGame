using TMPro;
using UnityEngine;

namespace SocialMediaAlgorithmGame
{
    public class AlgorithmGame : MonoBehaviour
    {
        public AlgorithmTopic[] Topics;

        [SerializeField] GameObject _topicObject;
        [SerializeField] Transform _topicSpawnPoint;

        [SerializeField]TMP_Text _scoreText;
        int _score;

        [SerializeField]TMP_Text _timerText;
        [SerializeField]int _gameLenghtInSeconds;
        int _secondsLeft;
        float _secondTimer;

        [SerializeField]float _spawnTime;
        float _spawnTimer;

        void Start()
        {
            _secondsLeft = _gameLenghtInSeconds;
            SpawnTopic();
        }

        void Update()
        {
            _spawnTimer += Time.deltaTime;
            if (_spawnTimer >= _spawnTime)
            {
                _spawnTimer -= _spawnTime;
                SpawnTopic();
            }

            _secondTimer += Time.deltaTime;
            if (_secondTimer >= 1)
            {
                _secondTimer -= 1;
                _secondsLeft--;
                if (_secondsLeft < 0)
                {
                    _secondsLeft = 0;
                }
            }

            _timerText.text = Mathf.FloorToInt(_secondsLeft / 60f).ToString() + ":" + (_secondsLeft % 60).ToString("00"); 
        }

        public void SpawnTopic ()
        {
            SocialMediaTopic t = Instantiate(_topicObject, _topicSpawnPoint.position, _topicSpawnPoint.rotation).GetComponent<SocialMediaTopic>();
            int randomIndex = Mathf.FloorToInt(Random.value * 0.99f * Topics.Length);
            t.SetTopic(Topics[randomIndex]);
        }

        public void AddScore (int extraScore)
        {
            _score += extraScore;
            if (_score < 0)
            {
                _score = 0;
            }
            _scoreText.text = _score.ToString();
        }
    }

    [System.Serializable]
    public class AlgorithmTopic
    {
        public string Name;
        public Color TopicColor;

        public AlgorithmTopic (Color color)
        {
            TopicColor = color;
        }
    }
}
