using UnityEngine;
using System;

public static class ScoreManager
{
    public static int Score { get; private set; }
    public static event Action<int> OnScoreChanged;

    public static void AddScore(int amount)
    {
        Score += amount;
        OnScoreChanged?.Invoke(Score);
    }
}
