using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{

    public Button resume;
    public Button controls;
    public Button back2sm;
    public Button quit;
    public LVLimport lV;
    


    void Start()
    {
        //PAUSEMENU BUTTONS
        back2sm.onClick.AddListener(Back2sm);
        resume.onClick.AddListener(Resume);
        quit.onClick.AddListener(Quit);

    }
   public void Back2sm()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);

    }
    public void Resume()
    {
        lV.isPaused = false;
        lV.player.transform.position = lV.posOfPlayer;
        
    }
    public void Quit()
    {
        PlayerPrefs.Save();
        Application.Quit();
        
    }
}
