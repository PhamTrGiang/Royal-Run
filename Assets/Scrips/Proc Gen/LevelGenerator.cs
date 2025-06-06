using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private CameraController cameraController;
    [SerializeField] private GameObject[] chunkPrefab;
    [SerializeField] private GameObject checkpointChunkPrefab;
    [SerializeField] private Transform chunkParent;
    [SerializeField] private ScoreManager scoreManager;

    [Header("Settings")]
    [SerializeField] private int startingChunksAmount = 12;
    [SerializeField] private int checkpointChunkInterval = 8;
    [Tooltip("Do not change chunk length value unless chunk prefab size reflects change")]
    [SerializeField] float chunkLength = 10f;
    [SerializeField] float moveSpeed = 8f;
    [SerializeField] float minMoveSpeed = 2f;
    [SerializeField] float maxMoveSpeed = 20f;
    [SerializeField] float minGravityZ = -22f;
    [SerializeField] float maxGravityZ = -2f;

    private List<GameObject> chunks = new List<GameObject>();
    private int chunksSpawned = 0;

    private void Start()
    {
        SpawnStartingChunks();
    }

    private void Update()
    {
        MoveChunks();
    }

    public void ChangeChunkMoveSpeed(float speedAmount)
    {
        float newMoveSpeed = moveSpeed + speedAmount;
        newMoveSpeed = Mathf.Clamp(newMoveSpeed, minMoveSpeed, maxMoveSpeed);

        if (newMoveSpeed != moveSpeed)
        {
            moveSpeed = newMoveSpeed;

            float newGravity = Physics.gravity.z - speedAmount;
            newGravity = Mathf.Clamp(newGravity, minGravityZ, maxGravityZ);

            Physics.gravity = new Vector3(Physics.gravity.x, Physics.gravity.y, newGravity);

            cameraController.ChangeCameraFOV(speedAmount);
        }



    }

    private void SpawnStartingChunks()
    {
        for (int i = 0; i < startingChunksAmount; i++)
        {
            SpawnChunk();
        }
    }

    private void SpawnChunk()
    {
        float spawnPositionZ = CalculateSpawnPositionZ();
        Vector3 chunkSpawnPos = new Vector3(transform.position.x, transform.position.y, spawnPositionZ);
        GameObject chunkToSpawn = ChooseChunkToSpawn();
        GameObject newChunkGO = Instantiate(chunkToSpawn, chunkSpawnPos, Quaternion.identity, chunkParent);
        chunks.Add(newChunkGO);
        Chunk newChunk = newChunkGO.GetComponent<Chunk>();
        newChunk.Init(this, scoreManager);
        chunksSpawned++;
    }

    private GameObject ChooseChunkToSpawn()
    {
        GameObject chunkToSpawn;

        if (chunksSpawned % checkpointChunkInterval == 0 && chunksSpawned != 0)
            chunkToSpawn = checkpointChunkPrefab;
        else
            chunkToSpawn = chunkPrefab[Random.Range(0, chunkPrefab.Length)];

        return chunkToSpawn;
    }

    private float CalculateSpawnPositionZ()
    {
        float spawnPositionZ;

        if (chunks.Count == 0)
        {
            spawnPositionZ = transform.position.z;
        }
        else
        {
            // spawnPositionZ = transform.position.z + (i * chunkLength);
            spawnPositionZ = chunks[chunks.Count - 1].transform.position.z + chunkLength;
        }

        return spawnPositionZ;
    }

    private void MoveChunks()
    {
        for (int i = 0; i < chunks.Count; i++)
        {
            GameObject chunk = chunks[i];
            chunk.transform.Translate(-transform.forward * (moveSpeed * Time.deltaTime));

            if (chunk.transform.position.z <= (Camera.main.transform.position.z - chunkLength))
            {
                chunks.Remove(chunk);
                Destroy(chunk);
                SpawnChunk();
            }

        }
    }
}
