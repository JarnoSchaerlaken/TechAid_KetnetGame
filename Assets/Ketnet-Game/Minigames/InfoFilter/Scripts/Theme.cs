using UnityEngine;

namespace InfoFilter
{
	public sealed class Theme : MonoBehaviour
	{
		private Color _activeTheme = Color.red;

		public Color ActiveTheme { get { return _activeTheme; } }
	}
}
