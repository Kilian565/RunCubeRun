using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//SCRIPT FÜRS LOADLVL
public class Load : MonoBehaviour
{

    public Button buttonpref;
    public Button button;
    public Button back;
    public Text text;
    public Transform parent;
    public int prog;

    void Awake()
    {
        buttonpref = Resources.Load<Button>("Prefabs/Button");


    }
    void Start()
    {
        prog = PlayerPrefs.GetInt("levelsCleard");

        for (int i = 1; i <= SceneManager.sceneCountInBuildSettings - 4; i++)
        {

            if (prog >= i)
            {
                string lvl = "Level " + i;
                button = Instantiate(buttonpref);
                Text Name = button.GetComponentInChildren<Text>();
                button.name = lvl;
                Name.text = lvl;
                button.transform.SetParent(parent, false);

                button.transform.localScale = new Vector3(1f, 1f, 1f);
                button.transform.position = buttonpref.transform.position + new Vector3(0, -i + 4, 0);

                //da man bei .onClick.AddListener nur ein unity event (also eine methode mit keinem parameter) 
                //einfügen kann, muss dem AddListener eine Funktion delegiert werden
                button.onClick.AddListener(delegate { Loadlvl(lvl); });

            }

        }
        back.onClick.AddListener(Back);
    }

    public void Loadlvl(string lvl)
    {

        SceneManager.LoadScene(lvl, LoadSceneMode.Single);


    }
    public void Back()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);

    }

}
