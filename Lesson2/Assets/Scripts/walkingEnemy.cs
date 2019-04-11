using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walkingEnemy : MonoBehaviour
{
    public float speed = 7f;
    public float speedRotate;
    public int health = 2;
    public Transform Player;
    public Rigidbody2D _rigidbody;
    public AudioClip DieSound;
    public AudioSource _audio;
    bool Die;
    Collider2D EnemyColl;

    private void Start()
    {
        if (GetComponent<Rigidbody2D>())
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        speedRotate = -GetDirection().x*3f;
        _audio = GetComponent<AudioSource>();
        EnemyColl = GetComponent<Collider2D>();
    }

    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Vector3 enemyScale = transform.localScale;
            enemyScale.x *= -1;
            transform.localScale = enemyScale;
            speedRotate *= -1;
        }
        if (collision.gameObject.tag == "Player")
        {
            Transform PlTransform = collision.transform;
            Rigidbody2D rg = PlTransform.GetComponent<Rigidbody2D>();
            if (transform.position.x < PlTransform.position.x)
            {
                rg.AddForce(new Vector2(3, 3), ForceMode2D.Impulse);
            }
            else
            {
                rg.AddForce(new Vector2(-3, -3), ForceMode2D.Impulse);
            }
            PlTransform.GetComponent<CharacterController>().health--;
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject, 1f);
    }

    void FixedUpdate()
    {
        transform.Rotate(0, 0, speedRotate);
        _rigidbody.velocity = new Vector2(GetDirection().x * transform.localScale.x * speed, _rigidbody.velocity.y);
    }
    private void Update()
    {
        if (health <= 0)
        {
            if (!Die)
            {
                _audio.clip = DieSound;
                _audio.Play();
                _rigidbody.AddForce(Vector2.up * 3, ForceMode2D.Impulse);
                Die = true;
            }
            EnemyColl.enabled = false;
            Destroy(gameObject,2);
        }
    }

    Vector3 GetDirection()
    {
        if (transform.position.x < Player.position.x)
        {
            return Vector3.right;
        }
        else
        {
            return Vector3.left;
        }
    }
}
