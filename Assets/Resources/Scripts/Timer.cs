using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text text;
    public float timespent = 0;
    double timeShowed;
    public LVLimport lvlImp;

    void Update()
    {
        if (!lvlImp.isPaused)

        {
            //hochzählen der timer variable
            timespent += Time.deltaTime;
            //runde des floats "timespent" auf 2 Nachkommastellen
            timeShowed = System.Math.Round(timespent, 2);
            //Anzeigen des Timer in der IngameUI
            text.text = "Time: " + timeShowed;

            
            if (Input.GetKey(KeyCode.R))
            {
                timespent = 0;
            }
        }
       
    }
}