using UnityEngine;

public class Coin : Pickup
{
    private ScoreManager scoreManager;

    [Header("Settings")]
    [SerializeField] private int scoreAmount = 100;

    public void Init(ScoreManager scoreManager)
    {
        this.scoreManager = scoreManager;
    }

    protected override void OnPickup()
    {
        scoreManager.IncreseScore(scoreAmount);
    }
}
