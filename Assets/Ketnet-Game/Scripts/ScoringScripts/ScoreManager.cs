using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreManager
{
	private static List<int> _minigameScores = new();
	private static List<int> _minigameHighScores = new();
	public static int DataPoints
	{
		get
		{
			int output = 0;
			foreach(int s in _minigameHighScores)
			{
				if (s >= 1000)
				{
					output++;
				}
			}
			return output;
		}
	}

	public static void UpdateScore(int score, int minigameIndex)
	{
		// TODO: Safeguards!
		while(minigameIndex >= _minigameScores.Count)
		{
			_minigameHighScores.Add(0);
			_minigameScores.Add(0);
		}

		_minigameScores[minigameIndex] = score;
		if (score > _minigameHighScores[minigameIndex])
		{
			_minigameHighScores[minigameIndex] = score;
		}
	}

	public static int GetScore(int minigameIndex)
	{
		if (minigameIndex >= _minigameScores.Count) return 0;
		return _minigameScores[minigameIndex];
	}

	public static int GetHighScore(int minigameIndex)
	{
		if (minigameIndex >= _minigameHighScores.Count) return 0;
		return _minigameHighScores[minigameIndex];
	}	
}
