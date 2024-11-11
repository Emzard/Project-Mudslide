using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundReset : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed = 5f;
    public float resetDistance = 20f;
    public GameObject objectToSpawn;
    public int Obstacles = 2;
    public Vector3 spawnArea = new Vector3(10f, 0, 10f);

    private Vector3 startPosition;
    void Start()
    {
        startPosition = transform.position;
        SpawnObjects();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back*speed*Time.deltaTime);

        if(Vector3.Distance(startPosition, transform.position) >= resetDistance)
        {
            transform.position = startPosition;
        }
    }

    void SpawnObjects()
    {
        for (int i = 0; i < Obstacles; i++)
        {
            Vector3 randomPosition = startPosition + new Vector3(Random.Range(-spawnArea.x/2, spawnArea.x/2), 0, Random.Range(-spawnArea.y/2, spawnArea.z/2));

           GameObject spawnedObject = Instantiate(objectToSpawn, randomPosition, Quaternion.identity);

           spawnedObject.transform.parent = transform;
        }
    }
}
