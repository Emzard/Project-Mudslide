using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public GameObject[] powerupPrefab;
    public GameObject[] collectiblePrefab;

    private Vector3 obstaclePos;
    private Vector3 collectiblePos;
    private Vector3 powerupPos;
    private float spawnRangeX = 4.5f;
    private int powerupIndex;
    private int obstacleIndex;
    private int collectibleIndex;
    // Start is called before the first frame update
    void Start()
    {
        // These spawn the different objects at different time intervals
        InvokeRepeating("SpawnObstacle", 3.0f, 1.3f);
        InvokeRepeating("SpawnCollectible", 5f, 1.8f);
        InvokeRepeating("SpawnPowerup", 10f, 20f);
    }

    // Update is called once per frame
    void Update()
    {
        // Gets a random position to spawn each object
        obstaclePos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0.075f, 70);
        collectiblePos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0.25f, 70);
        powerupPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 1, 70);

        // Decides which of the powerups to spawn
        powerupIndex = Random.Range(0, powerupPrefab.Length);
        obstacleIndex = Random.Range(0, obstaclePrefabs.Length);
        collectibleIndex = Random.Range(0, collectiblePrefab.Length);

    }

    void SpawnObstacle()
    {
        Instantiate(obstaclePrefabs[obstacleIndex], obstaclePos, obstaclePrefabs[obstacleIndex].transform.rotation);
    }

    void SpawnCollectible()
    {
        Instantiate(collectiblePrefab[collectibleIndex], collectiblePos, collectiblePrefab[collectibleIndex].transform.rotation);
    }

    void SpawnPowerup()
    {
        Instantiate(powerupPrefab[powerupIndex], powerupPos, powerupPrefab[powerupIndex].transform.rotation);
    }
}
