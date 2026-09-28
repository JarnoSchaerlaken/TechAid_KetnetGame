using System.Collections.Generic;

public static class ScoreRecordKeeper
{
    public static List<int> MinigameScores = new List<int>(2) { 0, 0 };
    public static void UpdateScore(int score, int minigameIndex)
    {
        MinigameScores[minigameIndex] = score;
    }
}
