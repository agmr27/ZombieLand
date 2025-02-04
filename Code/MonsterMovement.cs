using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public Transform target; // The target (player) to move toward
    public float moveSpeed = 3.0f;


    //roar
    public AudioClip roarSound;

    private AudioSource audioSource;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        roarSound = audioSource.clip;
    }

    void Update()
    {
        if (target != null)
        {
            // Calculate the direction vector towards the target (player)
            Vector3 direction = target.position - transform.position;

            // Normalize the direction vector to have a length of 1
            direction.Normalize();

            // Move the monster towards the player
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }

    public void SetTarget(Transform newTarget, float speed)
    {
        target = newTarget;
        moveSpeed = speed;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Laser"))
        {
            
            Destroy(gameObject);
            //audioSource.PlayOneShot(roarSound);
        }

    }





}