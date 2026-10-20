using Unity.VisualScripting;
using UnityEngine;
namespace SnackTheData
{
    public class SnackTheDataStateTracker : MonoBehaviour
    {
        public static SnackTheDataStateTracker Instance;

        public static bool GameOver;

        [SerializeField]
        private GameObject _endScreen;

        [SerializeField]
        private GameObject _playfield;

        [SerializeField]
        private GameObject _sidePanel;

        [SerializeField]
        private GameObject _controlText;

        public static int TypeOneAmount;

        public static int TypeTwoAmount;

        public static int AllowedAmountBetweenHighestAndLowest = 2;

        public SnackTheDataMovement Player;

        [SerializeField]
        private GameObject _warningType1;

        [SerializeField]
        private GameObject _warningType2;

        private static bool _gameStarted = false;

        private InputSystem_Actions _actions;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            Instance = this;

            _actions = new InputSystem_Actions();

            _actions.Enable();

            _actions.Player.Move.performed += ctx => { if (_gameStarted) return; _controlText.SetActive(false); _gameStarted = true; SnackTheDataSpawner.Enabled = true; };
            _actions.Player.Click.canceled += ctx => { if (_gameStarted) return; _controlText.SetActive(false); _gameStarted = true; SnackTheDataSpawner.Enabled = true; };

            GameOver = false;

            TypeOneAmount = 0;

            TypeTwoAmount = 0;
        }

        private void Update()
        {
            if (!_gameStarted) return;
            if (GameOver)
            {
                _endScreen.SetActive(true);

                _playfield.SetActive(false);

                _sidePanel.SetActive(false);
            }
            else
            {
                if (TypeOneAmount - TypeTwoAmount >= AllowedAmountBetweenHighestAndLowest - 1)
                {
                    _warningType1.SetActive(true);
                }
                else
                {
                    _warningType1.SetActive(false);
                }

                if (TypeTwoAmount - TypeOneAmount >= AllowedAmountBetweenHighestAndLowest - 1)
                {
                    _warningType2.SetActive(true);
                }
                else
                {
                    _warningType2.SetActive(false);
                }

                if (TypeOneAmount - TypeTwoAmount >= AllowedAmountBetweenHighestAndLowest || TypeTwoAmount - TypeOneAmount >= AllowedAmountBetweenHighestAndLowest)
                {
                    EndMinigame();
                }
            }
        }

        public static void EndMinigame()
        {
            GameOver = true;
        }
    }
}
