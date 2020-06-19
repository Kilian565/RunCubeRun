using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Variablen
    // Objects für den RayCast
    public GameObject ray1;
    public GameObject ray2;
    public GameObject ray3;
    public GameObject ray4;
    public GameObject ray5;
    public GameObject ray6;

    public new Rigidbody2D rigidbody;

    // Variablen für die PlayerPref und Time
    public string progKey = "levelsCleard";
    public string highKey = "highscore";
    public string timeKey = "Time-Level";
    public float timespent = 0;
    public double timeShowed;


    public int maxVel;
    public float speed;
    public float jumpStrength;
    public int Jumpcount;

    //bools für verschíedene Dinge (Name erklärts)
    bool _isGrounded;
    bool jumped;
    bool canWallJump;
    bool candoublejump;
    private bool isDed;
    bool canjump;

    Vector2 startpos;
    Scene scene;

    public LVLimport lvlImp;
    public bool IsGrounded
    {
        get
        {
            return _isGrounded;
        }
        set
        {
            Debug.Log("Is grounded = " + value);
            _isGrounded = value;
        }
    }
    public Vector2 Inputs   // Variable für meine Inputs
    {
        get
        {
            return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
    }



    void Awake()
    {
        scene = SceneManager.GetActiveScene();
        rigidbody = this.GetComponent<Rigidbody2D>();
    }
   
    void Update()
    {
        if (!lvlImp.isPaused)
        {
            timespent += Time.deltaTime;
            timeShowed = System.Math.Round(timespent, 2);
            MovementLR();
            Jumps();
            LimitSpeed();
            RayCasts();

            if (IsGrounded || Jumpcount > 2)
            {
                Jumpcount = 0;
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                Debug.Log(PlayerPrefs.GetInt(progKey));
                Debug.Log(PlayerPrefs.GetFloat(timeKey + scene.buildIndex.ToString()));
                Debug.Log(timespent);
            }


        }
        
    }

    void MovementLR()
    {

        if (Inputs.x != 0)
        {
            rigidbody.AddForce(new Vector2(Inputs.x, 0) * speed, ForceMode2D.Impulse);
        }
        else
        {
            rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);

        }


    }
    void OnCollisionEnter2D(Collision2D collision2D)
    {
        //Methode um dem Objekt zu sagen was passieren soll, wenn ...
        if (!lvlImp.isPaused)
        {
            if (collision2D.gameObject.layer == 10)
            {
                // Der Tod
                if (!isDed)
                {
                    isDed = true;
                    lvlImp.CreatePlayer(lvlImp.startpos);
                    Destroy(this.gameObject);
                }
            }

            if (collision2D.gameObject.layer == 11)
            {
                // Das Ziel + setzen des ProgressKey und Highscores in den PlayerPrefs

                PlayerPrefs.SetInt(progKey, scene.buildIndex);
                PlayerPrefs.SetFloat(timeKey + scene.buildIndex.ToString(), timespent);
                PlayerPrefs.Save();

                // Bedingung fürs setzen des HIGHSCORES in den PlayerPrefs
                if (PlayerPrefs.GetFloat(timeKey + scene.buildIndex.ToString()) < PlayerPrefs.GetFloat(highKey + scene.buildIndex.ToString()) || !PlayerPrefs.HasKey(highKey + scene.buildIndex.ToString()))
                {
                    PlayerPrefs.SetFloat(highKey + scene.buildIndex.ToString(), value: PlayerPrefs.GetFloat(timeKey + scene.buildIndex.ToString()));
                    Debug.Log("NEW HIGHSCORE: " + PlayerPrefs.GetFloat(highKey + scene.buildIndex.ToString()));
                    PlayerPrefs.Save();
                }


                Debug.Log("Reached End");
                if (scene.buildIndex + 1 < SceneManager.sceneCount)
                {
                    SceneManager.LoadScene(scene.buildIndex + 1, LoadSceneMode.Single);

                }
                else
                {
                    SceneManager.LoadScene(0,LoadSceneMode.Single);
                    PlayerPrefs.Save();
                }

            }
        }

    }


    void RayCasts()
    {
        // Methode für RayCasts
        float offset = 0.15f;

        Vector2 vd1 = new Vector2(ray1.transform.position.x + offset, ray1.transform.position.y);
        Vector2 vd2 = new Vector2(ray2.transform.position.x - offset, ray2.transform.position.y);
        Vector2 vs1 = new Vector2(ray3.transform.position.x, ray3.transform.position.y);
        Vector2 vs2 = new Vector2(ray4.transform.position.x, ray4.transform.position.y);
        Vector2 vs3 = new Vector2(ray5.transform.position.x, ray5.transform.position.y + offset);
        Vector2 vs4 = new Vector2(ray6.transform.position.x, ray6.transform.position.y + offset);

        //RAYS
        RaycastHit2D hit1 = Physics2D.Raycast(vd1, Vector2.down, 0.09f);
        RaycastHit2D hit2 = Physics2D.Raycast(vd2, Vector2.down, 0.09f);
        RaycastHit2D hit3 = Physics2D.Raycast(vs1, Vector2.left, 0.09f);
        RaycastHit2D hit4 = Physics2D.Raycast(vs2, Vector2.right, 0.09f);
        RaycastHit2D hit5 = Physics2D.Raycast(vs3, Vector2.right, 0.09f);
        RaycastHit2D hit6 = Physics2D.Raycast(vs4, Vector2.left, 0.09f);

        //Anzeigen der Rays im Spiel (EDITOR)
        Debug.DrawRay(vd1, Vector2.down * 0.09f, Color.red, 0);
        Debug.DrawRay(vd2, Vector2.down * 0.09f, Color.red, 0);
        Debug.DrawRay(vs1, Vector2.left * 0.09f, Color.red, 0);
        Debug.DrawRay(vs2, Vector2.right * 0.09f, Color.red, 0);
        Debug.DrawRay(vs3, Vector2.right * 0.09f, Color.red, 0);
        Debug.DrawRay(vs4, Vector2.left * 0.09f, Color.red, 0);

        if (hit1.collider != null || hit2.collider != null)
        {
            IsGrounded = true;
            Jumpcount = 0;
        }
        if (hit3.collider != null || hit4.collider != null || hit5.collider != null || hit6.collider != null)
        {

            canWallJump = true;

        }
        else
        {
            canWallJump = false;
        }


    }

    void Jumps()
    {

        // Methode für die Jumps
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump"))
        {
            if (IsGrounded)
            {
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
                rigidbody.AddRelativeForce(Vector2.up * jumpStrength, ForceMode2D.Impulse);
                candoublejump = true;
                IsGrounded = false;


            }

        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump"))
        {
            if (candoublejump)
            {
                candoublejump = false;
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
                rigidbody.AddRelativeForce(Vector2.up * jumpStrength, ForceMode2D.Impulse);


            }



        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump"))
        {
            if (canWallJump && Jumpcount <= 1)
            {
                Jumpcount = 1;
                candoublejump = false;
                canWallJump = false;
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
                rigidbody.AddRelativeForce(Vector2.up * jumpStrength, ForceMode2D.Impulse);
                Jumpcount++;
            }
        }
    }



    void LimitSpeed()
    {
        // Limitierung des speeds
        if (rigidbody.velocity.x >= maxVel)
        {
            rigidbody.velocity = new Vector2(maxVel, rigidbody.velocity.y);
        }
        else if (rigidbody.velocity.x <= -maxVel)
        {
            rigidbody.velocity = new Vector2(-maxVel, rigidbody.velocity.y);
        }
        if (rigidbody.velocity.y >= maxVel * 2)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, maxVel * 2);
        }



    }

}




