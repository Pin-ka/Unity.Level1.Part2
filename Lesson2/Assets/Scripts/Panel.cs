using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel : MonoBehaviour
{
    public Transform[] Lives = new Transform[3];
    CharacterController Player;
    public Text NameText;

    void Start()
    {
        Player = FindObjectOfType<CharacterController>();
        for (int i = 0; i < Lives.Length; i++)
        {
            Lives[i] = transform.GetChild(i);
        }
        NameText.text = PlayerPrefs.GetString("PlayerName");
    }

    public void Refresh()
    {
        for (int i=0;i<Lives.Length;i++)
        {
            if (i < Player.Lives)
            {
                Lives[i].gameObject.SetActive(true);
            }
            else
            {
                Lives[i].gameObject.SetActive(false);
            }
        }
    }

    void Update()
    {
        
    }
}
