using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private ComboSystem comboSystem;
    
    private int currentScore = 0;

    public void AddScore(int baseScore)
    {
        int comboMultiplier = comboSystem.GetCurrentCombo() + 1;
        currentScore += baseScore * comboMultiplier;
    }

    public int GetScore()
    {
        return currentScore;
    }
}
