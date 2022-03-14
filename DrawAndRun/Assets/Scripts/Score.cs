using UnityEngine;
using System;

public class Score : MonoBehaviour
{
    public event Action OnScoreAmountChanged = null;

    private int _amount = 0;

    public int ScorePoint
    {
        get => _amount;
        set
        {
            _amount = value;
            OnScoreAmountChanged?.Invoke();
        }
    }

    private void Start()
    {
        ScorePoint = 0;
    }
}
