using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


namespace SnackTheData
{
    public class SnackTheDataMovement : MonoBehaviour
    {
        public Image Player;

        public int TypeOneAmount;

        public int TypeTwoAmount;

        public int AllowedAmountBetweenHighestAndLowest = 2;

        public List<GameObject> Tail = new List<GameObject>();

        public List<Vector3> TailSegmentDirections = new List<Vector3>();

        private float _speed = Screen.height / 7.5f;

        public float TailSegmentSpacing = 65f;

        public Vector3 Direction = Vector3.zero;

        public bool GameOver = false;

        [SerializeField]
        private GameObject _endScreen;

        public GameObject PlayField;

        [SerializeField]
        private GameObject _sidePanel;

        [SerializeField]
        private GameObject _TailTestPart1;

        [SerializeField]
        private GameObject _TailTestPart2;

        private GameObject _segmentToBeAdded;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (GameOver)
            {
                _sidePanel.SetActive(false);
                PlayField.SetActive(false);
                _endScreen.SetActive(true);
            }
            else
            {
                MovePlayer();

                MoveTail();

                CheckDeathStatus();
            }

            UpdateTailSegments();
        }


        public void MovePlayer()
        {
            Player.transform.position += Direction * _speed * Time.deltaTime;
        }

        public void MoveTail()
        {
            if (Direction != Vector3.zero)
            {
                for (int i = 0; i < Tail.Count; i++)
                {
                    if (i == 0)
                    {
                        if (Tail[i].transform.position.x == Player.transform.position.x)
                        {
                            TailSegmentDirections[i] = (Player.transform.position - Tail[i].transform.position).normalized;
                            Tail[i].transform.position += _speed * Time.deltaTime * TailSegmentDirections[i];
                        }
                        else if (Tail[i].transform.position.y == Player.transform.position.y)
                        {
                            TailSegmentDirections[i] = (Player.transform.position - Tail[i].transform.position).normalized;
                            Tail[i].transform.position += _speed * Time.deltaTime * TailSegmentDirections[i];
                        }
                        else
                        {
                            Tail[i].transform.position += _speed * Time.deltaTime * TailSegmentDirections[i];
                            if (TailSegmentDirections[i].x > 0)
                            {
                                if (Tail[i].transform.position.x >= Player.transform.position.x)
                                {
                                    Tail[i].transform.position = new Vector3(Player.transform.position.x, Tail[i].transform.position.y, 0);
                                    TailSegmentDirections[i] = (Player.transform.position - Tail[i].transform.position).normalized;
                                }
                            }
                            else if (TailSegmentDirections[i].x < 0)
                            {
                                if (Tail[i].transform.position.x <= Player.transform.position.x)
                                {
                                    Tail[i].transform.position = new Vector3(Player.transform.position.x, Tail[i].transform.position.y, 0);
                                    TailSegmentDirections[i] = (Player.transform.position - Tail[i].transform.position).normalized;
                                }
                            }
                            else if (TailSegmentDirections[i].y > 0)
                            {
                                if (Tail[i].transform.position.y >= Player.transform.position.y)
                                {
                                    Tail[i].transform.position = new Vector3(Tail[i].transform.position.x, Player.transform.position.y, 0);
                                    TailSegmentDirections[i] = (Player.transform.position - Tail[i].transform.position).normalized;
                                }
                            }
                            else if (TailSegmentDirections[i].y < 0)
                            {
                                if (Tail[i].transform.position.y <= Player.transform.position.y)
                                {
                                    Tail[i].transform.position = new Vector3(Tail[i].transform.position.x, Player.transform.position.y, 0);
                                    TailSegmentDirections[i] = (Player.transform.position - Tail[i].transform.position).normalized;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (Tail[i].transform.position.x == Tail[i - 1].transform.position.x)
                        {
                            TailSegmentDirections[i] = (Tail[i - 1].transform.position - Tail[i].transform.position).normalized;
                            Tail[i].transform.position += _speed * Time.deltaTime * TailSegmentDirections[i];
                        }
                        else if (Tail[i].transform.position.y == Tail[i - 1].transform.position.y)
                        {
                            TailSegmentDirections[i] = (Tail[i - 1].transform.position - Tail[i].transform.position).normalized;
                            Tail[i].transform.position += _speed * Time.deltaTime * TailSegmentDirections[i];
                        }
                        else
                        {
                            Tail[i].transform.position += _speed * Time.deltaTime * TailSegmentDirections[i];
                            if (TailSegmentDirections[i].x > 0)
                            {
                                if (Tail[i].transform.position.x >= Tail[i - 1].transform.position.x)
                                {
                                    Tail[i].transform.position = new Vector3(Tail[i - 1].transform.position.x, Tail[i].transform.position.y, 0);
                                    TailSegmentDirections[i] = (Tail[i - 1].transform.position - Tail[i].transform.position).normalized;
                                }
                            }
                            else if (TailSegmentDirections[i].x < 0)
                            {
                                if (Tail[i].transform.position.x <= Tail[i - 1].transform.position.x)
                                {
                                    Tail[i].transform.position = new Vector3(Tail[i - 1].transform.position.x, Tail[i].transform.position.y, 0);
                                    TailSegmentDirections[i] = (Tail[i - 1].transform.position - Tail[i].transform.position).normalized;
                                }
                            }
                            else if (TailSegmentDirections[i].y > 0)
                            {
                                if (Tail[i].transform.position.y >= Tail[i - 1].transform.position.y)
                                {
                                    Tail[i].transform.position = new Vector3(Tail[i].transform.position.x, Tail[i - 1].transform.position.y, 0);
                                    TailSegmentDirections[i] = (Tail[i - 1].transform.position - Tail[i].transform.position).normalized;
                                }
                            }
                            else if (TailSegmentDirections[i].y < 0)
                            {
                                if (Tail[i].transform.position.y <= Tail[i - 1].transform.position.y)
                                {
                                    Tail[i].transform.position = new Vector3(Tail[i].transform.position.x, Tail[i - 1].transform.position.y, 0);
                                    TailSegmentDirections[i] = (Tail[i - 1].transform.position - Tail[i].transform.position).normalized;
                                }
                            }
                        }
                    }
                }
            }
        }

        public void CheckDeathStatus()
        {
            if (Direction != Vector3.zero)
            {
                if (Player.transform.position.x < 50 || Player.transform.position.x > Screen.width / 2 - 50 || Player.transform.position.y < 50 || Player.transform.position.y > Screen.height - 50)
                {
                    GameOver = true;
                }

                if (TypeOneAmount - TypeTwoAmount >= AllowedAmountBetweenHighestAndLowest || TypeTwoAmount - TypeOneAmount >= AllowedAmountBetweenHighestAndLowest)
                {
                    GameOver = true;
                }

                if (Tail.Count > 1) 
                {
                    for (int i = 1; i < Tail.Count; i++)
                    {
                        if (Tail[i].GetComponent<SnackTheDataTailTrigger>().HitTail)
                        {
                            GameOver = true;
                        }
                    }
                }
            }
        }


        public void UpdateTailSegments()
        {
            if (_segmentToBeAdded != null)
            {
                Tail.Add(_segmentToBeAdded);

                TailSegmentDirections.Add(Vector3.zero);

                _segmentToBeAdded = null;
            }
        }

        public void SetSegmentToBeAdded(GameObject segment)
        {
            _segmentToBeAdded = segment;
        }

        public void MoveUp()
        {
            if (Direction != Vector3.down) Direction = Vector3.up;
        }

        public void MoveDown()
        {
            if (Direction != Vector3.up) Direction = Vector3.down;
        }

        public void MoveLeft()
        {
            if (Direction != Vector3.right) Direction = Vector3.left;
        }

        public void MoveRight()
        {
            if (Direction != Vector3.left) Direction = Vector3.right;
        }
    }
}
