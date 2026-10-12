using System.Collections;
using UnityEngine;

namespace InfoFilter
{
	public sealed class DataSpawner : MonoBehaviour
	{
		[SerializeField] private float _spawnRadius = 12F;
		[SerializeField] private float _spawnInterval = 2F;

		private void Start()
		{
			// What's this you ask?
			// Read this: https://stackoverflow.com/questions/70538568/unity-startcoroutine
			StartCoroutine( SpawnRoutine() );
		}

		private IEnumerator SpawnRoutine()
		{
			while ( true )
			{
				MoveToRandomPosition();
				// TODO: Spawn Data object.
				yield return new WaitForSeconds(_spawnInterval);
			}
		}

		private void MoveToRandomPosition()
		{
			// Need a trigonometry refresher?
			// I got you covered: https://medium.com/eincode/trigonometry-for-game-development-34acca285ae3
			float angle = Random.Range( 0F, 2F * Mathf.PI );
			Vector2 position = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * _spawnRadius;
			transform.position = position;
		}
	}
}
