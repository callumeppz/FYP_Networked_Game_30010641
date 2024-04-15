using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    /// <summary>
    /// This script is inspired by The Game Guy from a YouTube short released in 2023.
    /// https://www.youtube.com/shorts/hxpUk0qiRGs
    /// </summary>

    // Variables for time tracking and UI elements
    public float time; // cureent time
    public bool timerOn = true; // bool if timer on
    public Text timerText; // set to time text
    public Text finalTime; // final text after time is stopped
    // Start is called before the first frame update
    void Start()
    {
       timerOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Update timer if it's on
        if (timerOn)
        {
            if (time > 0)
            {
                time += Time.deltaTime;
                updateTimer(time);
            }
            
        }
    }
    // Method to update the timer display
    void updateTimer(float currentTime)
    {
        currentTime += 1;
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
        finalTime.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
