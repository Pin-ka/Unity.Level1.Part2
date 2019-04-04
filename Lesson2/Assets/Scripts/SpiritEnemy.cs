using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritEnemy : MonoBehaviour
{
    public int health = 2;
    public Transform Player;
    bool Active;
    float X;
    float Y;
    int RayDistance;
    public LayerMask Mask;
    bool Patrol;
    bool right;
    Vector3 direction;
    Vector3 locDir;
    float speed = 3f;
    public Rigidbody2D _rigidbody;
    public GameObject enemyBullPref;
    bool Shooting=true;
    public AudioClip[] Sounds;
    public AudioSource _audio;
    bool Die;
    Collider2D EnemyColl;

    private void Start()
    {
        if (GetComponent<Rigidbody2D>())
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }
        X = 3;
        Y = -3;
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        Patrol = true;
        RayDistance=2;
        direction = Vector3.right;
        _audio = GetComponent<AudioSource>();
        EnemyColl = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

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
                rg.AddForce(new Vector2(-3, 3), ForceMode2D.Impulse);
            }
            PlTransform.GetComponent<CharacterController>().health--;
        }
    }

    private void OnBecameInvisible()
    {
        Active = false;
    }
    private void OnBecameVisible()
    {
        Active = true;
    }
    private void FixedUpdate()
    {
        if (Active)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(X,Y),RayDistance,Mask);
            //Debug.DrawRay(transform.position, new Vector2(X, Y), Color.red);
            if (Patrol)
            {
                Y = -3;
                RayDistance = 2;
                if (hit.collider)
                {
                    Move();
                }
                else
                {
                    if (_rigidbody.velocity.y == 0)
                    {
                        Flip();
                    }
                }
            }
            else
            {
                Y = 0;
                RayDistance = 3;
                if (Mathf.Abs(transform.position.x - Player.transform.position.x) <= RayDistance)
                {
                    if (hit.collider && hit.collider.tag == "Player" && Shooting)
                    {
                        _audio.clip = Sounds[0];
                        _audio.Play();
                        StartCoroutine(enemyShoot());
                        Shooting = false;
                    }
                }
                else
                {
                    if (direction != GetDirection())
                    {
                        Flip();
                        direction = GetDirection();
                    }
                    Move();
                }
            }
        }
    }
    private void Update()
    {
        if (health <= 0)
        {
            if (!Die)
            {
                _audio.clip = Sounds[1];
                _audio.Play();
                _rigidbody.AddForce(Vector2.up * 3, ForceMode2D.Impulse);
                Die = true;
            }
            EnemyColl.enabled = false;
            Destroy(gameObject, 2);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Patrol = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Patrol = true;
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

    void Flip()
    {
        right = !right;
        Vector2 sc = transform.localScale;
        sc.x *= -1;
        transform.localScale = sc;
        X *= -1;
        direction *= -1;
    }
    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position+direction,Time.deltaTime*speed);
    }
    IEnumerator enemyShoot()
    {
        GameObject temp = Instantiate(enemyBullPref,transform.position,Quaternion.identity);
        temp.name = "enemyBullet";
        if (right)
        {
            temp.GetComponent<Bullet>().direction = -1;
            temp.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            temp.GetComponent<Bullet>().direction = 1;
            temp.GetComponent<SpriteRenderer>().flipX = false;
        }
        yield return new WaitForSeconds(2);
        Shooting = true;
    }
}
