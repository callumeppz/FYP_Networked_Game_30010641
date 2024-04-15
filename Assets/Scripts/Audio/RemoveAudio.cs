using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private void Awake()
    {
        // Find all audio listeners in the scene
        AudioListener[] audioListeners = FindObjectsOfType<AudioListener>();

        // If there are more than one audio listener, remove the extras
        if (audioListeners.Length > 1)
        {
            for (int i = 1; i < audioListeners.Length; i++)
            {
                Destroy(audioListeners[i].gameObject);
            }
        }

        // If there are no audio listeners, create one
        if (audioListeners.Length == 0)
        {
            gameObject.AddComponent<AudioListener>();
        }
    }
}
