using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    float speed;
    public int direction = 1;
    Rigidbody2D _rigidbody;
    Vector2 vect45;
    Animator anim;
    GameObject [] Victims;
    bool isBoom=false;
    public AudioClip Sound;
    public AudioSource _audio;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Invoke("Boom",5f);
        Invoke("Dest", 6f);
        speed = 0.4f;
        vect45 = new Vector2(1, 3);
        if (direction == 1)
        {
            vect45.x = 1;
        }
        else
        {
            vect45.x = -1;
        }
        _rigidbody.AddForce(vect45 * speed, ForceMode2D.Impulse);
        _audio = GetComponent<AudioSource>();
    }

 
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && isBoom)
        {
            collision.gameObject.GetComponent<walkingEnemy>().health = 0;
        }
        if (collision.gameObject.tag == "Player" && isBoom)
        {
            collision.gameObject.GetComponent<CharacterController>().health = 0;
        }
        if (collision.gameObject.tag == "Spirit" && isBoom)
        {
            collision.gameObject.GetComponent<SpiritEnemy>().health = 0;
        }
    }

    void Boom()
    {
        anim.SetBool("isBoom", true);
        isBoom=true;
        _audio.clip = Sound;
        _audio.Play();
    }
    private void Dest()
    {
        Destroy(gameObject, 1f);
    }
}
