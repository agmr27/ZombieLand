using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beanBehave : MonoBehaviour
{



    public AudioClip cansound;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        cansound = audioSource.clip;
    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            Destroy(gameObject);
        }

        else if (collision.gameObject.CompareTag("Laser"))
        {
            Destroy(gameObject);

        }

        else if (collision.gameObject.CompareTag("Player"))
        {
            audioSource.PlayOneShot(cansound);
            Destroy(gameObject);

        }


    }
}
