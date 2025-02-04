using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            Destroy(gameObject);
        }

        else if (collision.gameObject.CompareTag("Bean"))
        {
            Destroy(gameObject);
        }





    }
}
