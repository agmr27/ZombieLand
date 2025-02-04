using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab;
    public float spawnInterval = 2.0f;
    public float moveSpeed = 6.0f;

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating("SpawnMonster", 0, spawnInterval);
    }

    void SpawnMonster()
    {
        Vector3 spawnPosition = CalculateSpawnPosition();
        GameObject newMonster = Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);
        MonsterMovement monsterMovement = newMonster.GetComponent<MonsterMovement>();

        if (monsterMovement != null)
        {
            monsterMovement.SetTarget(player, moveSpeed);
        }
    }

    Vector3 CalculateSpawnPosition()
    {
        // Calculate a spawn position slightly off the screen
        // You can choose random coordinates outside the camera view
        float spawnX = Random.Range(-2, 2); // Modify these values based on your scene
        float spawnY = Random.Range(-2, 2); // Modify these values based on your scene

        Vector3 screenPosition = new Vector3(spawnX, spawnY, 0);
        return Camera.main.ViewportToWorldPoint(screenPosition);
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
     
        if (collision.gameObject.CompareTag("Bean"))
        {
            spawnInterval = spawnInterval - 0.3f;
        }


    }


}