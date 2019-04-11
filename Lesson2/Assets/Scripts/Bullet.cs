using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float speed = 0.3f;
    public int direction = 1;
    bool playerBull;

    private void Start()
    {
        if (gameObject.name == "bullet")
        {
            playerBull = true;
        }
        else
        {
            playerBull = false;
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject,1f);
    }

    void Update()
    {
        transform.position += Vector3.right * direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerBull)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                GameObject temp = collision.gameObject;
                temp.GetComponent<walkingEnemy>().health--;
                Destroy(gameObject);
            }
            else if (collision.gameObject.tag == "Spirit")
            {
                GameObject temp = collision.gameObject;
                temp.GetComponent<SpiritEnemy>().health--;
                Destroy(gameObject);
            }
            else if (collision.gameObject.tag != "Gun" && collision.gameObject.tag != "Player")
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (collision.gameObject.tag == "Player")
            {
                GameObject temp = collision.gameObject;
                temp.GetComponent<CharacterController>().health--;
                Destroy(gameObject);
            }
            
        }

        
    }
}
