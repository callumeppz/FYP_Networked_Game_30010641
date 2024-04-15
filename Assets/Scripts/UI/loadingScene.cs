using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    public Slider loadingBar; // Reference to the loading bar UI element

    public void LoadScene(int sceneId)
    {
        StartCoroutine(LoadSceneAsync(sceneId));
    }

    IEnumerator LoadSceneAsync(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        loadingBar.gameObject.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBar.value = progress;
            yield return null;
        }

        // Ensure the loading bar is filled at 100% when the scene is fully loaded
        loadingBar.value = 1f;

        Debug.Log("Loading next scene");

        // Add a small delay before hiding the loading bar to ensure it's fully visible
        yield return new WaitForSeconds(10f);

        // Hide loading bar
        loadingBar.gameObject.SetActive(false);
    }
}
