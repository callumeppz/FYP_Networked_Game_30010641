using UnityEngine;

public class QuitGame : MonoBehaviour
{
    // A script used for quiting of an applicaiton
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Quit(); // quit applicaiton if escape pressed
        }
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Test: Game is exiting"); // quit method for quit buttons, test debug
    }
}
