using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //used to shoot the lazer
    public GameObject laserPrefab;
    public GameObject overPrefab;// The laser prefab you want to shoot.
              // The point from which the laser will be fired.
    public float laserSpeed = 10f;   // The speed of the laser.
    public float offsetDistance = 0.5f;  // Offset distance in front of the player.


    public float fireRate = 0.5f;  // Adjust this value to set the desired firing rate.
    private float lastFireTime;



    //sounds

    public AudioClip laserSound;

    private AudioSource audioSource;

    //movinhg

    public float moveBoundaryX = 5f; // Adjust these values based on your desired screen boundaries.
    public float moveBoundaryY = 3f;

    public float moveSpeed = 5f;
    public float rotationSpeed = 200f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        audioSource = GetComponent<AudioSource>();
        laserSound = audioSource.clip;
    }

    private void FixedUpdate()
    {
        maneouvur();
        MaybeFire();
        ClampPlayerPosition();
    }


    void maneouvur()
    {
        // Movement input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(horizontalInput, verticalInput).normalized * moveSpeed;

        // Rotation input
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Apply movement and rotation
        rb.velocity = movement;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed* Time.deltaTime);
    }


    void ClampPlayerPosition()
    {
        Vector3 clampedPosition = transform.position;

        // Clamp the X position within the boundary.
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -moveBoundaryX, moveBoundaryX);

        // Clamp the Y position within the boundary.
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, -moveBoundaryY, moveBoundaryY);

        // Apply the clamped position.
        transform.position = clampedPosition;
    }






    void MaybeFire()
    {
        // Check if enough time has passed since the last shot.
        if (Input.GetButton("Fire") && Time.time - lastFireTime >= 1f / fireRate)
        {
            FireLaze();
            lastFireTime = Time.time;  // Update the last shot time.
        }
    }




    private void FireLaze()
    {
        // Instantiate the PlayerOrb prefab in front of the player's ship
        Vector3 spawnPosition = transform.position + transform.right;
        GameObject laser = Instantiate(laserPrefab, spawnPosition, transform.rotation);  // Use transform.rotation to match player's rotation

        Rigidbody2D lazeRB = laser.GetComponent<Rigidbody2D>();
        lazeRB.velocity = laserSpeed * transform.right;

        audioSource.PlayOneShot(laserSound);

        Destroy(laser, 2f);
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Bean"))
        {
            ScoreKeeper.AddToScore(1);
        }

        else if (collision.gameObject.CompareTag("Monster"))
        {
            EndGame();
            Instantiate(overPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        }


    }




    private void DestroyGameObjectsWithTag(string tag)
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);

        foreach (GameObject obj in gameObjects)
        {
            Destroy(obj);
        }
    }


    public void EndGame()
    {
        // Find and delete game objects with specific tags
        DestroyGameObjectsWithTag("Player");
        DestroyGameObjectsWithTag("Monster");
        DestroyGameObjectsWithTag("Bean");
        DestroyGameObjectsWithTag("Laser");
    
    }







}