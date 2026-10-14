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

        [SerializeField]GameObject _tutorial;
        [SerializeField]GameObject _winScreen;
        [SerializeField]GameObject _loseScreen;

        [SerializeField]float _spawnTime;
        float _spawnTimer;

        bool _isGameOngoing;

        void Start()
        {
            _secondsLeft = _gameLenghtInSeconds;
        }

        void Update()
        {
            if (_isGameOngoing) 
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
                        EndGame();
                    }
                }
            }

            _timerText.text = Mathf.FloorToInt(_secondsLeft / 60f).ToString() + ":" + (_secondsLeft % 60).ToString("00"); 
        }

        public void StartGame ()
        {
            SpawnTopic();
            _isGameOngoing = true;
        }

        public void RemoveTutorial ()
        {
            _tutorial.SetActive(false);
        }

        void EndGame ()
        {
            _isGameOngoing = false;

            if (_score >= 1000)
            {
                _winScreen.SetActive(true);
            }
            else
            {
                _loseScreen.SetActive(true);
            }
        }

        public void SpawnTopic ()
        {
            SocialMediaTopic t = Instantiate(_topicObject, _topicSpawnPoint.position, _topicSpawnPoint.rotation).GetComponent<SocialMediaTopic>();
            int randomIndex = Mathf.FloorToInt(Random.value * 0.99f * Topics.Length);
            t.SetTopic(Topics[randomIndex]);
        }

        public void AddScore (int extraScore)
        {
            if (!_isGameOngoing) return;
            
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
