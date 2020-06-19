using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class backbutton : MonoBehaviour
{
    public Button backb;

	// Use this for initialization
	void Start ()
    {
        
        backb.onClick.AddListener(Back);
	}
	
	// Update is called once per frame
	
    void Back()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
