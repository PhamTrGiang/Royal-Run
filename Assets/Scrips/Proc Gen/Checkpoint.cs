using TMPro;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private TMP_Text timeExtensionText;

    [Header("Settings")]
    [SerializeField] private float checkpointTimeExtension = 5f;
    [SerializeField] private float obstacleDecreaseTimeAmount = .2f;

    GameManager gameManager;
    ObstacleSpawner obstacleSpawner;

    private const string playerString = "Player";

    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        obstacleSpawner = FindFirstObjectByType<ObstacleSpawner>();
        timeExtensionText.text ="+ "+checkpointTimeExtension;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerString))
        {
            gameManager.IncreaseTime(checkpointTimeExtension);
            obstacleSpawner.DecreaseObstacleSpawnTime(obstacleDecreaseTimeAmount);
        }
    }
}
