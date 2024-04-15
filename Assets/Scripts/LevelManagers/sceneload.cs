using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneload : MonoBehaviour
{

    /// <summary>
    /// for the intiial splash screen, allowing users time to connect to photon before processding
    /// </summary>
    // Start is called before the first frame update

    void Start()
    {
        Invoke("SceneManage", 7); // invoke scenemanage method after 7 seconds
    }
    void SceneManage()
    {
        SceneManager.LoadScene("MainMenu"); // loads mainmenu scene
    }
}
