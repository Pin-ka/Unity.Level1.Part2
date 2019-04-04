using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    Animator anim;
    public AudioClip Sound;
    public AudioSource _audio;
    public GameObject coins;
    void Start()
    {
        anim = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            anim.SetBool("isOpen", true);
            _audio.clip = Sound;
            _audio.Play();
            Invoke("endAnim",0.1f);
            coins.SetActive(true);
        }
    }

    void endAnim()
    {
        anim.SetBool("isOpen", false);
    }
}
