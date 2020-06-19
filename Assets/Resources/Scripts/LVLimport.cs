using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LVLimport : MonoBehaviour
{

    // Variablen
    public Transform parent;

    public Camera cameraPAUSE;
    public Camera camera1;
    public Canvas pauseMenu;

    public Texture2D levelLayout;

    public Vector3 startpos;


    public Player playerpref;
    public Player player;
    public Vector3 posOfPlayer;

    public bool isPaused = false;


    void Awake()
    {
        // Methode, die noch vor der "void Start()" ausgefüghrt wird

        playerpref = Resources.Load<Player>("Prefabs/ForestCube");
        camera1 = playerpref.GetComponentInChildren<Camera>();
        Levelaufbau();


    }

    void Start()
    {
        //Methode die beim aufrufen des scripts ausgeführt wird
        CreatePlayer(startpos);
    }


    void Update()
    {
        //Methode die jeden frame ausgeführt wird.. :P

        if (Input.GetKey(KeyCode.R))
        {
            player.timespent = 0;
            Destroy(player.gameObject);
            CreatePlayer(startpos);
        }

        PauseMenu();


    }


    void Levelaufbau()
    {
        //Methode zum einlesen und auswerten der Level (Bild/PNG) Datei
        for (int y = 0; y < levelLayout.height; y++)
        {
            for (int x = 0; x < levelLayout.width; x++)
            {
                CubeData data = new CubeData();
                data.position = new Vector2(x, y);
                data.layer = -1;
                data.path = "Prefabs/Brick";

                if (levelLayout.GetPixel(x, y) == Color.black)
                {
                    //Erstellen der LevelCubes durch finden der richtigen Farbe in der Bild-Datei
                    data.layer = 9;
                    data.color = Color.white;
                    data.material = Resources.Load<Material>("Materials/BrickText");
                    Debug.Log(data.material);

                }
                else if (levelLayout.GetPixel(x, y) == Color.red)
                    // setzen der StartPosition des Players
                    startpos = new Vector3(x, y, 0);

                else if (levelLayout.GetPixel(x, y) == Color.green)
                {
                    //Erstellen der "tödlichen" Cubes
                    data.layer = 10;
                    data.material = Resources.Load<Material>("Materials/tot");
                    data.color = Color.red;

                }
                else if (levelLayout.GetPixel(x, y) == Color.blue)
                {
                    //Erstellen des Ziel Cubes
                    data.layer = 11;
                    data.material = Resources.Load<Material>("Materials/ziel");
                    data.color = Color.blue;
                }
                if (data.layer != -1)
                    CreateCube(data);

            }
        }

    }
        //Methode zur Erstellung des Cubes
    private void CreateCube(CubeData data)
    {
        GameObject newCube = Instantiate(Resources.Load<GameObject>(data.path));
        newCube.transform.position = data.position;
        newCube.layer = data.layer;
        newCube.GetComponent<Renderer>().material.color = data.color;
        newCube.GetComponent<Renderer>().material = data.material;
        newCube.transform.SetParent(parent, false);

    }
    private class CubeData
    {
        //Klasse für die Cubes
        public string path;
        public Vector2 position;
        public int layer;
        public Color color;
        public Material material;

    }
    public void CreatePlayer(Vector2 position)
    {
        // Methode zum Erstellen des Spielers
        player = Instantiate(playerpref);
        camera1 = player.GetComponentInChildren<Camera>();
        player.transform.position = position;
        player.lvlImp = this;


    }

    void PauseMenu()
    {
        //Methode für das pause menü
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            isPaused = true;
            posOfPlayer = player.transform.position;
        }
        else if (Input.GetKeyDown(KeyCode.F1) && isPaused)
        {
            isPaused = false;
            player.transform.position = posOfPlayer;
        }


        if (isPaused)
        {
            //Aktivieren/Deaktivieren der Cameras und Canvas fürs Pausemenü
            camera1.gameObject.SetActive(false);
            cameraPAUSE.gameObject.SetActive(true);
            pauseMenu.gameObject.SetActive(true);


        }
        else
        {
            //Aktivieren/Deaktivieren der Cameras und Canvas fürs Pausemenü
            cameraPAUSE.gameObject.SetActive(false);
            camera1.gameObject.SetActive(true);
            pauseMenu.gameObject.SetActive(false);
        }
    }
}
