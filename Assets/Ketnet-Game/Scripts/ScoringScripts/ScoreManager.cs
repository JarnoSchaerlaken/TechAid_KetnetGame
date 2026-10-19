using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreManager
{
	private static List<int> minigameScores = new List<int>(2) { 0, 0 };
	public static int DataPoints
	{
		get
		{
			int output = 0;
			foreach(int s in minigameScores)
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
		minigameScores[minigameIndex] = score;
	}

	public static int GetScore(int minigameIndex)
	{
		if (minigameIndex >= minigameScores.Count) return 0;
		return minigameScores[minigameIndex];
	}

	
}
