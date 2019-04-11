using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Interface : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject Settings;
    public Image MenuBackGround;
    public bool Showmenu;
    public GameObject Player;
    bool isPlayer;
    public string PlayerName;
    public InputField input;
    public Slider Volume;

    void Start()
    {
        MenuBackGround = GetComponent<Image>();
        Settings.SetActive(false);
        MainMenu.SetActive(true);
        DontDestroyOnLoad(gameObject);
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        Settings.SetActive(false);
        MainMenu.SetActive(false);
        MenuBackGround.enabled = false;
        SceneManager.LoadScene(1);
    }

    public void ShowMainMenu()
    {
        Settings.SetActive(false);
        MainMenu.SetActive(true);
    }

    public void ShowSettingsMenu()
    {
        Settings.SetActive(true);
        MainMenu.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void GetName()
    {
        PlayerName = input.text;
        PlayerPrefs.SetString("PlayerName",PlayerName);
        PlayerPrefs.Save();
    }
    public void ChangeVolume()
    {
        AudioListener.volume = Volume.value;
    }

    void Update()
    {
        if (Application.loadedLevel == 1 && Input.GetKeyDown(KeyCode.Escape))
        {
            if (!Showmenu)
            {
                if (!isPlayer) {
                    Player = GameObject.FindGameObjectWithTag("Player");
                    isPlayer = true;
                }
                Player.SetActive(false);
                Settings.SetActive(false);
                MainMenu.SetActive(true);
                MenuBackGround.enabled = true;
                Time.timeScale = 0;
                Showmenu = true;
            }
            else
            {
                Settings.SetActive(false);
                MainMenu.SetActive(false);
                MenuBackGround.enabled = false;
                Player.SetActive(true);
                Time.timeScale = 1;
                Showmenu = false;
            }
        }
    }
}
