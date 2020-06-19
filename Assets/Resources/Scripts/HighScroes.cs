using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighScroes : MonoBehaviour
{

    //Variablen
    public Transform parent;
    public Text textpref;
    public Text text;
    public Button back;
    public float score;
    Scene scene;

    //#Keys für Playerpref
    public string progKey = "levelsCleard";
    public string highKey = "highscore";
    public string timeKey = "Time-Level";


    void Awake()
    {
        textpref = Resources.Load<Text>("Prefabs/text");
        score = PlayerPrefs.GetFloat(highKey);


    }
    void Start()
    {
        HighScoreListe();
        back.onClick.AddListener(Back);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("score" + score);
        }

    }
    public void Back()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);

    }

   
    
    
    public void HighScoreListe()
    // #Methode zum Erstellen der Highscore Liste
    {
        for (int i = 1; i <= SceneManager.sceneCountInBuildSettings - 3; i++)

        {
            if (PlayerPrefs.HasKey(highKey + i))        //#Abfrage ob der Highscore in den PlayerPrefs vorhanden ist
            {
                text = Instantiate(textpref);
                text.transform.SetParent(parent, false);
                text.name = "Level " + i;

                text.text = text.name;
                text.transform.localScale = new Vector3(1f, 1f, 1f);

                text.transform.localPosition = (textpref.transform.position) - new Vector3(textpref.transform.position.x + 300, -50 + i * 50, 0);       //Positionierung der Texte

                text = Instantiate(textpref);
                text.transform.SetParent(parent, false);
                text.name = "Level-Score " + i;

                text.text = PlayerPrefs.GetFloat(highKey + i).ToString();  //Anzeigen des PlayerPrefs für den Highscore
                text.transform.localScale = new Vector3(1f, 1f, 1f);

                text.transform.localPosition = (textpref.transform.position) - new Vector3(textpref.transform.position.x, -50 + i * 50, 0);

            }

        }

    }
}
