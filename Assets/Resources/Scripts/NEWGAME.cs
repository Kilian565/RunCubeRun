using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class NEWGAME : MonoBehaviour
{
    //Variablen
    public Button controls;
    public Button newGame;
    public Button BUTTONHighscore;
    public Button LoadGame;
    public Button ResetStats;
    public Button check;
    public Button quit;
    public bool isReset;


    void Start()
    {
        Debug.Log(SceneManager.sceneCount);
        //Aufrufen der Funktionen für die Buttons
        newGame.onClick.AddListener(StartNewGame);
        BUTTONHighscore.onClick.AddListener(StartHighScores);
        LoadGame.onClick.AddListener(LoadLevel);
        ResetStats.onClick.AddListener(Reset);
        quit.onClick.AddListener(Quit);
        controls.onClick.AddListener(Controls);
    }

    void Update()
    {
        // wenn der Reset-Button Gedrückt wird soll das check.gameobject aktiviert werden
        if (isReset)
        {
            check.gameObject.SetActive(true);

        }
        else
        {
            check.gameObject.SetActive(false);

        }

    }
    // Unten folgen die Funktionen für die Buttons, was bei "OnClick" passieren soll
    void StartNewGame()
    {
        SceneManager.LoadScene("Level 1", LoadSceneMode.Single);

    }
    void StartHighScores()
    {
       SceneManager.LoadScene("HIGHscores", LoadSceneMode.Single);
    }
    void LoadLevel()
    {
        SceneManager.LoadScene("Loadlvl", LoadSceneMode.Single);
    }
    void Controls()
    {

        SceneManager.LoadScene("Controls", LoadSceneMode.Single);
    }
    void Reset()
    {
        PlayerPrefs.DeleteAll();
        isReset = true;
        
    }
    void Quit()
    {
        PlayerPrefs.Save();
        Application.Quit();

    }
}