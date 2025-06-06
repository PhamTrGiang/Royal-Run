using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] private GameObject fencePrefab;
    [SerializeField] private GameObject applePrefab;
    [SerializeField] private GameObject coinPrefab;

    [SerializeField] private float appleSpawnChance = .3f;
    [SerializeField] private float coinSpawnChance = .5f;
    [SerializeField] private float coinSeperationLength = 2f;

    [SerializeField] private float[] lanes = { -2.5f, 0f, 2.5f };

    private LevelGenerator levelGenerator;
    private ScoreManager scoreManager;


    private List<int> availebleLanes = new List<int> { 0, 1, 2 };


    private void Start()
    {
        SpawnFences();
        SpawnApple();
        SpawnCoin();

    }

    public void Init(LevelGenerator levelGenerator,ScoreManager scoreManager)
    {
        this.levelGenerator = levelGenerator;
        this.scoreManager = scoreManager;
    }

    private void SpawnFences()
    {
        int fencesToSpawn = Random.Range(0, lanes.Length);
        for (int i = 0; i < fencesToSpawn; i++)
        {
            if (availebleLanes.Count <= 0) break;

            int selectedLane = SelectLane();

            Vector3 spawnPosition = new Vector3(lanes[selectedLane], transform.position.y, transform.position.z);
            Instantiate(fencePrefab, spawnPosition, Quaternion.identity, this.transform);
        }
    }

    private void SpawnApple()
    {
        if (Random.value > appleSpawnChance || availebleLanes.Count <= 0) return;

        int selectedLane = SelectLane();

        Vector3 spawnPosition = new Vector3(lanes[selectedLane], transform.position.y, transform.position.z);
        Apple apple = Instantiate(applePrefab, spawnPosition, Quaternion.identity, this.transform).GetComponent<Apple>();
        apple.Init(levelGenerator);

    }

    private void SpawnCoin()
    {
        if (Random.value > coinSpawnChance || availebleLanes.Count <= 0) return;

        int selectedLane = SelectLane();

        int maxCoinsToSpawn = 6;
        int coinsToSpawn = Random.Range(1, maxCoinsToSpawn);

        float topOfChunkZPos = transform.position.z + (coinSeperationLength * 2f);

        for (int i = 0; i < coinsToSpawn; i++)
        {
            float spawnPositionZ = topOfChunkZPos - (i * coinSeperationLength);
            Vector3 spawnPosition = new Vector3(lanes[selectedLane], transform.position.y, spawnPositionZ);
            Coin newCoin = Instantiate(coinPrefab, spawnPosition, Quaternion.identity, this.transform).GetComponent<Coin>();
            newCoin.Init(scoreManager);
        }

    }

    private int SelectLane()
    {
        int randomLaneIndex = Random.Range(0, availebleLanes.Count);
        int selectedLane = availebleLanes[randomLaneIndex];
        availebleLanes.RemoveAt(randomLaneIndex);
        return selectedLane;
    }
}
