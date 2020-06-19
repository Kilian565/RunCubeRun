using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelName : MonoBehaviour
{

    public Text text;

    void Start()
    {
        text.text = SceneManager.GetActiveScene().name; // Anzeigen des LEVEL namens in der Ingame ui

    }
}
	