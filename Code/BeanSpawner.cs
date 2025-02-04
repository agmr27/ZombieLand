using UnityEngine;

public class RandomPrefabSpawner : MonoBehaviour
{
    public GameObject prefabToSpawn; // The prefab you want to spawn
    public float spawnInterval = 10.0f; // Time interval between spawns

    void Start()
    {
        // Start spawning at regular intervals
        InvokeRepeating("SpawnPrefab", 0, spawnInterval);
    }

    void SpawnPrefab()
    {
        // Define a random spawn position within the scene boundaries
        float spawnX = Random.Range(-8, 8); // Adjust the range based on your scene size
        float spawnY = Random.Range(-4, 4); // Adjust the range based on your scene size

        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0);

        // Spawn the prefab at the random position
        Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
    }



    
}
