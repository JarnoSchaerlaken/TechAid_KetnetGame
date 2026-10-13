using TMPro;
using UnityEngine;

namespace InfoFilter
{
	public sealed class HUD : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _healthLabel;
		[SerializeField] private TextMeshProUGUI _scoreLabel;
		[SerializeField] private TextMeshProUGUI _themeLabel;

		private InfoFilter.Health _healthObject;
		private InfoFilter.Score _scoreObject;
		private InfoFilter.Theme _themeObject;

		private void Awake()
		{
			var health = FindAnyObjectByType<InfoFilter.Health>();
			var score = FindAnyObjectByType<InfoFilter.Score>();
			var theme = FindAnyObjectByType<InfoFilter.Theme>();
			bool isConfigured = score != null && health != null && theme != null;

			if ( !isConfigured)
			{
				Debug.LogError( "Missing dependencies!", this );
				return;
			}

			_healthObject = health;
			_scoreObject = score;
			_themeObject = theme;
		}

		private void Update()
		{
			Sync();
		}

		private void Sync()
		{
			if ( _healthLabel != null )
			{
				_healthLabel.text = $"Health: {_healthObject._health}";
			}

			if ( _scoreLabel != null )
			{
				_scoreLabel.text = $"Score: {_scoreObject._score}";
			}

			if ( _themeLabel != null )
			{
				_themeLabel.color = _themeObject._theme;
			}
		}
	}
}
