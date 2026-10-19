using SocialMediaAlgorithmGame;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

namespace InfoFilter
{
	public sealed class Data : DraggableObject
	{
		[SerializeField] private float _movementSpeed = 1F;

		private float _outOfBoundsRadius;
		private Vector3 _velocity;
		private Vector3 _onStartDragPosition;
		private Color _theme;

		public Color Theme { get { return _theme; } }

		protected override void Awake()
		{
			base.Awake();

			var dataSpawner = FindAnyObjectByType<InfoFilter.DataSpawner>();

			if ( dataSpawner == null )
			{
				Debug.LogError("Missing dependencies!", this);
				return;
			}

			_outOfBoundsRadius = dataSpawner.SpawnRadius + 1F;
			SetTheme();
		}

		// TODO: Should be protected, not public.
		public override void Start()
		{
			base.Start();
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

		// TODO: Should be protected, not public.
		public override void OnStartDrag()
		{
			base.OnStartDrag();
			_onStartDragPosition = transform.position;
		}

		// TODO: Should be protected, not public.
		public override void OnStopDrag()
		{
			base.OnStopDrag();
			_velocity = (transform.position - _onStartDragPosition).normalized;
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
	}
}
