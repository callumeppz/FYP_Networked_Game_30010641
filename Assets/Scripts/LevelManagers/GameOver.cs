using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviourPun
{

    public GameObject youWin;
    public GameObject InvalidAmountOfKeys;
    public GameObject Display;
    public TimerScript timerScript;
    // Start is called before the first frame update

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (KeyScript.keyNumber == 9) 
        {
            youWin.SetActive(true);
            timerScript.timerOn = false;
            Debug.Log("Start Button displays");
        }
        else
        {
            Display.SetActive(true);
            int remainingKeys = 9 - KeyScript.keyNumber;
            ShowRemainingKeysMessage(remainingKeys);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
            if (youWin != null && youWin.activeSelf)
            {
                youWin.SetActive(false);
            }
            if (Display != null && Display.activeSelf)
            {
                Display.SetActive(false);
            }
    }

    void ShowRemainingKeysMessage(int remainingKeys)
    {
        InvalidAmountOfKeys.GetComponent<Text>().text = "Keys remaining: " + remainingKeys.ToString();
    }
}
