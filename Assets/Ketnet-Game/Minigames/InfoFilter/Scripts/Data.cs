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
		public override void Update()
		{
			base.Update();

			HandleMovement();

			if ( CheckOutOfBounds() )
			{
				Destroy(gameObject);
			}
		}

		private bool CheckOutOfBounds()
		{
			// A primitive solution.
			if ( ( transform.position.x < -_outOfBoundsRadius || transform.position.x > _outOfBoundsRadius )
				&& ( transform.position.y < -_outOfBoundsRadius || transform.position.y > _outOfBoundsRadius ) )
			{
				return true;
			}

			return false;
		}

		private void HandleMovement()
		{
			if (!Dragging)
			{
				// Placeholder logic.
				transform.position = Vector2.MoveTowards( transform.position, new Vector2(0F, 0F), _movementSpeed * Time.deltaTime );
			}
		}

		// TODO: Should be protected, not public.
		public override void OnStartDrag()
		{
			base.OnStartDrag();
			// Cache start position
		}

		// TODO: Should be protected, not public.
		public override void OnStopDrag()
		{
			base.OnStopDrag();
			// Calculate a direction vector with velocity based on distance
			// Release object with new velocity like a sling shot
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
