using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Runtime.ExceptionServices;

public class LevelChanger : MonoBehaviour
{
    // variables initialised for level loading and UI elements
    [SerializeField] string levelName;// name of the level to load
    public GameObject buttonToDisplay;// btton to display for starting game
    public Slider loadingBar;// loading bar to display loading progress
    public float loadingDelay = 1f;// delay before loading level to ensure level is loaded correclty
    public GameObject loadingscreen;// loading screen UI element to display
    public GameObject Title;
    [SerializeField] GameObject levels;
    public Text text;
    private KeyScript keys;// initialising the key script

    /** Start method to ensure clients are synced correctly and button is displayed
 */
    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        buttonToDisplay.SetActive(false);
    }

    /** method to check if collision with player, and if that player is the masterclient
 */
    void OnCollisionEnter2D(Collision2D collision)
    {

        if (PhotonNetwork.IsMasterClient)
        {

            if (collision.gameObject.CompareTag("Player"))
            {
                if (HealthCollectable.healthNumber == 3)
                {
                    buttonToDisplay.SetActive(true);
                }
            }
        }
        else
        {
            text.gameObject.SetActive(true);
        }
    }

    /** method to check player has left collision radius, deactivate button if so
 */
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (buttonToDisplay != null && buttonToDisplay.activeSelf)
            {
                buttonToDisplay.SetActive(false);
            }
        }
    }

    // Method to load the next level

    public void LoadLevel()
    {
        StartCoroutine(LoadLevelWithDelay(levelName));
        Debug.Log("Loading level: " + levelName); 
    }
    // Coroutine to load level with delay
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

        // Load level asynchronously in background
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelName);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBar.value = progress;
            yield return null;
        }

        Debug.Log("Loading next scene");
        // once loaded, set elemetns to inactive
        loadingBar.gameObject.SetActive(false);
        loadingscreen.gameObject.SetActive(false);
        Title.gameObject.SetActive(false);
    }
}
