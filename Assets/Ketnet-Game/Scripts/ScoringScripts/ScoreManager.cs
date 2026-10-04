using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
	public static ScoreManager Instance;

	private static List<int> minigameScores = new List<int>(2) { 0, 0 };


    private void Awake()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;
		DontDestroyOnLoad(gameObject);
	}

	private void Start()
	{
		if (ScoreManager.Instance != null)
		{
			// TODO: Write the data to the relevant UI elements.
		}
	}

	public static void UpdateScore(int score, int minigameIndex)
	{
		// TODO: Safeguards!
		minigameScores[minigameIndex] = score;
	}

	public static int GetScore(int minigameIndex)
	{
		return minigameScores[minigameIndex];
	}

	
}
