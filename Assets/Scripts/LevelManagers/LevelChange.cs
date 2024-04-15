using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Runtime.ExceptionServices;

public class LevelChange : MonoBehaviour
{
    [SerializeField] string levelName;
    public GameObject buttonToDisplay;
    public GameObject displayableHint;
    public Slider loadingBar;
    public float loadingDelay = 1f;
    public GameObject loadingscreen;
    public GameObject Title;
    [SerializeField] GameObject levels;
    public Text text;

    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

        buttonToDisplay.SetActive(false);
        displayableHint.SetActive(false);

    }


    void OnCollisionEnter2D(Collision2D collision)
    {

        if (PhotonNetwork.IsMasterClient)
        {

            if (collision.gameObject.CompareTag("Player"))
            {
                if (KeyScript.keyNumber >= 3)
                {
                    buttonToDisplay.SetActive(true);
                }
                else
                {
                    displayableHint.SetActive(true);
                }
            }
        }
        else
        {
            text.gameObject.SetActive(true);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (buttonToDisplay != null && buttonToDisplay.activeSelf)
            {
                buttonToDisplay.SetActive(false);
               
            }
            if (displayableHint != null && displayableHint.activeSelf)
            {
                displayableHint.SetActive(false);
            }
        }
    }



    public void LoadLevel()
    {
        StartCoroutine(LoadLevelWithDelay(levelName));
        Debug.Log("Loading level: " + levelName);
    }

    IEnumerator LoadLevelWithDelay(string levelName)
    {
        loadingBar.gameObject.SetActive(true);
        loadingscreen.gameObject.SetActive(true);
        Title.SetActive(true);
        loadingBar.value = 0f;

        float elapsedTime = 0f;
        float duration = 1f;



        while (elapsedTime < duration)
        {
            float progress = elapsedTime / duration;
            loadingBar.value = progress;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        loadingBar.value = 1f;

        yield return new WaitForSeconds(loadingDelay);


        AsyncOperation operation = SceneManager.LoadSceneAsync(levelName);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBar.value = progress;
            yield return null;
        }

        Debug.Log("Loading next scene");

        loadingBar.gameObject.SetActive(false);
        loadingscreen.gameObject.SetActive(false);
        Title.gameObject.SetActive(false);
    }


}
