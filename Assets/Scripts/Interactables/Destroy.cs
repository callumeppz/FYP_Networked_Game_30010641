using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    /// <summary>
    /// This script is not used within the application at this time,
    /// But maye be used in the future as a way of destroying previous levels.
    /// </summary>
   
    // initialise variables
    public float DestroyTIme = 4f;
    // Start is called before the first frame update
    private void OnEnable()
    {
        Destroy(gameObject, DestroyTIme); // destroy gameobject after 4 seconds
    }
}
