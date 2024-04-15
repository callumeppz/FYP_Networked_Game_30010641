using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // A script used to initialise a camera for each concurent user

    public Transform target; // target (player) to follow
    public Vector3 offset;  // offset from targets actual position

    void Update()
    {
        if (target != null)
        {
          // calculation of the tagrets position for camera
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            transform.position = targetPosition + offset; // setting cameras position
        }
    }
}
