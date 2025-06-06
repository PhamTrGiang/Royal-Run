using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private GameObject gameOverText;
    [SerializeField] private float startTime = 5f;

    private float timeLeft;
    private bool gameOver = false;

    public bool GameOver => gameOver;

    private void Start()
    {
        timeLeft = startTime;
    }

    private void Update()
    {
        DecreaseTime();
    }

    public void IncreaseTime(float amount) => timeLeft += amount;

    private void DecreaseTime()
    {
        if (gameOver) return;

        timeLeft -= Time.deltaTime;
        timeText.text = timeLeft.ToString("F1");

        if (timeLeft <= 0f)
        {
            PlayerGameOver();
        }
    }

    private void PlayerGameOver()
    {
        gameOver = true;
        playerController.enabled = false;
        gameOverText.SetActive(true);
        Time.timeScale = .1f;
    }
}
