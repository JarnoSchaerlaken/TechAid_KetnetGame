using System.Runtime.CompilerServices;
using UnityEngine;

namespace InfoFilter
{
	public sealed class Robot : MonoBehaviour
	{
		private InfoFilter.Health _healthObject;
		private InfoFilter.Score _scoreObject;
		private InfoFilter.Theme _themeObject;

		private void Awake()
		{
			var health = FindAnyObjectByType<InfoFilter.Health>();
			var score = FindAnyObjectByType<InfoFilter.Score>();
			var theme = FindAnyObjectByType<InfoFilter.Theme>();
			bool isConfigured = score != null && health != null && theme != null;

			if (!isConfigured)
			{
				Debug.LogError("Missing dependencies!", this);
				return;
			}

			_healthObject = health;
			_scoreObject = score;
			_themeObject = theme;
		}

		void OnTriggerEnter2D(Collider2D collision) => HandleCollision(collision);
		
		private void HandleCollision(Collider2D collision)
		{
			if (collision == null)
			{
				// Can this even happen?
				return;
			}

			Data component = collision.gameObject.GetComponent<InfoFilter.Data>();

			if (component == null)
			{
				return;
			}

			Color objectTheme = component.Theme;

			if ( _themeObject.ActiveTheme == objectTheme )
			{
				_scoreObject._score += 1;
			}
			else
			{
				_healthObject._health -= 1;
			}

			Destroy(collision.gameObject);
		}
	}
}
