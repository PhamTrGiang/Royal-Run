using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TMP_Text scoreTect;

    private int score = 0;

    public void IncreseScore(int amount)
    {

        if (gameManager.GameOver) return;

        score += amount;
        scoreTect.text = score.ToString();
    }
}
