using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


namespace SnackTheData
{
    public class SnackTheDataMovement : MonoBehaviour
    {
        public Image Player;

        public List<GameObject> Tail = new List<GameObject>();

        public List<Vector3> TailSegmentDirections = new List<Vector3>();

        private float _speed = Screen.height / 7.5f;

        public Vector3 Direction = Vector3.zero;

        private InputSystem_Actions _actions;

        private float _swipeDistance = 1f;

        public GameObject PlayField;

        private Vector2 _swipeDirection;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _actions = new InputSystem_Actions();

            _actions.Enable();

            _actions.Player.Click.canceled += TouchComplete;

            _actions.Player.Swipe.performed += ProcessSwipe;

            _actions.Player.Move.performed += ctx =>
            {
                if (ctx.ReadValue<Vector2>().x == 1)
                {
                    MoveRight();
                }
                else if (ctx.ReadValue<Vector2>().x == -1)
                {
                    MoveLeft();
                }
                else if (ctx.ReadValue<Vector2>().y == -1)
                {
                    MoveDown();
                }
                else if (ctx.ReadValue<Vector2>().y == 1)
                {
                    MoveUp();
                }
            };
        }

        // Update is called once per frame
        void Update()
        {
            MovePlayer();

            MoveTail();
        }


        private void MovePlayer()
        {
            Player.transform.position += Direction * _speed * Time.deltaTime;
        }

        private void MoveTail()
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

        private void TouchComplete(InputAction.CallbackContext ctx)
        {
            if (Mathf.Abs(_swipeDirection.magnitude) < _swipeDistance) return;

            if (_swipeDirection.x > 0 && Mathf.Abs(_swipeDirection.x) > Mathf.Abs(_swipeDirection.y))
            {
                MoveRight();
            }
            else if (_swipeDirection.x < 0 && Mathf.Abs(_swipeDirection.x) > Mathf.Abs(_swipeDirection.y))
            {
                MoveLeft();
            }
            else if (_swipeDirection.y > 0 && Mathf.Abs(_swipeDirection.x) < Mathf.Abs(_swipeDirection.y))
            {
                MoveUp();
            }
            else if (_swipeDirection.y < 0 && Mathf.Abs(_swipeDirection.x) < Mathf.Abs(_swipeDirection.y))
            {
                MoveDown();
            }
        }

        private void ProcessSwipe(InputAction.CallbackContext ctx)
        {
            _swipeDirection = ctx.ReadValue<Vector2>();
        }
    }
}
