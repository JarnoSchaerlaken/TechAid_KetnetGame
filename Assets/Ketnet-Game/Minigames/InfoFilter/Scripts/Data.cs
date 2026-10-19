using UnityEngine;

namespace InfoFilter
{
	public sealed class Data : MonoBehaviour
	{
		[SerializeField, Min(0F)] private float _movementSpeed = 1F;
		[SerializeField] private bool _swipeMode = false;
		[SerializeField, Min(0f)] private float _flickSpeed = 3f;
		[SerializeField, Min(0F)] private float _minimumSwipeDistance = 0.1F;

		private InputSystem_Actions _inputActions;
		private Camera _cam;
		private Color _theme;
		private Vector3 _onStartDragPosition;
		private Vector3 _velocity;
		private Vector2 _mousePosition;
		private Vector2 _pointerStartPosition;
		private float _outOfBoundsRadius;
		private bool _clicking;
		private bool _dragging;
		private bool _swipeStartedOnSprite;

		public Color Theme { get { return _theme; } }

		[HideInInspector] public bool Clicking
		{
			get { return _clicking; }
			set
			{
				_clicking = value;

				if (value) OnClick();
				else OnStopClick();
			}
		}

		[HideInInspector] public bool Dragging
		{
			get { return _dragging; }
			set
			{
				if (_dragging == value) return;
				_dragging = value;

				if (value) OnStartDrag();
				else OnStopDrag();
			}
		}

		private void Awake()
		{
			var dataSpawner = FindAnyObjectByType<InfoFilter.DataSpawner>();

			if ( dataSpawner == null )
			{
				Debug.LogError("Missing dependencies!", this);
				return;
			}

			_inputActions = new();
			_inputActions.Player.MousePos.performed += ctx => _mousePosition = ctx.ReadValue<Vector2>();
			_inputActions.Player.Click.performed += ctx => Clicking = true;
			_inputActions.Player.Click.canceled += ctx => Clicking = false;
			_inputActions.Player.Touch.performed += ctx => Clicking = true;
			_inputActions.Player.Touch.canceled += ctx => Clicking = false;

			_inputActions.Player.TouchPosition.performed += ctx =>
			{
				_mousePosition = ctx.ReadValue<Vector2>();
			};

			_cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
			_outOfBoundsRadius = dataSpawner.SpawnRadius + 1F;
			SetTheme();
		}

		void OnEnable()
		{
			_inputActions.Enable();
		}

		private void Start()
		{
			_velocity = -transform.position.normalized;
		}

		private void FixedUpdate()
		{
			HandleMovement();

			if (CheckOutOfBounds())
			{
				Destroy(gameObject);
			}
		}

		private void Update()
		{
			if (Dragging && TryGetWorldMousePosition(out Vector3 worldMousePos))
			{
				transform.position = worldMousePos;
			}
		}

		void OnDisable()
		{
			_inputActions.Disable();
		}

		private bool CheckOutOfBounds()
		{
			// A primitive solution.
			if (transform.position.x < -_outOfBoundsRadius || transform.position.x > _outOfBoundsRadius || transform.position.y < -_outOfBoundsRadius || transform.position.y > _outOfBoundsRadius)
			{
				return true;
			}

			return false;
		}

		private void HandleMovement()
		{
			if (!Dragging)
			{
				transform.position += _movementSpeed * Time.fixedDeltaTime * _velocity;
			}
		}

		void OnClick()
		{
			if (_swipeMode)
			{
				_pointerStartPosition = _mousePosition;
				_swipeStartedOnSprite = IsMouseOverSprite();
				return;
			}

			Dragging = IsMouseOverSprite();
		}

		void OnStopClick()
		{
			if (_swipeMode)
			{
				if (!_swipeStartedOnSprite)
				{
					return;
				}

				Vector2 swipeDelta = _mousePosition - _pointerStartPosition;

				if (swipeDelta.sqrMagnitude < _minimumSwipeDistance * _minimumSwipeDistance)
				{
					return;
				}

				_velocity = new Vector3(swipeDelta.x, swipeDelta.y, 0f).normalized;
				_movementSpeed = _flickSpeed;
				_swipeStartedOnSprite = false;
				return;
			}

			Dragging = false;
		}

		private void OnStartDrag()
		{
			if (!_swipeMode)
			{
				_onStartDragPosition = transform.position;
			}
		}

		private void OnStopDrag()
		{
			if (!_swipeMode)
			{
				_velocity = (transform.position - _onStartDragPosition).normalized;
			}
		}

		private void SetTheme()
		{
			float randomValue = Random.Range( 0, 2 );

			switch (randomValue)
			{
				case 0:
					_theme = Color.red;
					gameObject.GetComponent<SpriteRenderer>().color = Color.red;
					break;
				case 1:
					_theme = Color.yellow;
					gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
					break;
			}
		}

		private bool TryGetWorldMousePosition(out Vector3 worldPosition)
		{
			Ray ray = _cam.ScreenPointToRay(_mousePosition);
			Plane plane = new(Vector3.forward, Vector3.zero);

			if (plane.Raycast(ray, out float distance))
			{
				worldPosition = ray.GetPoint(distance);
				return true;
			}

			worldPosition = default;
			return false;
		}

		private bool IsMouseOverSprite()
		{
			if (!TryGetWorldMousePosition(out Vector3 worldMousePos))
			{
				return false;
			}

			Vector3 delta = worldMousePos - transform.position;
			return Mathf.Abs(delta.x) < 0.5f * transform.lossyScale.x && Mathf.Abs(delta.y) <  0.5f * transform.lossyScale.y;
		}
	}
}
