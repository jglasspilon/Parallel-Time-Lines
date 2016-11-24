// Summary: Allows the camera to follow the target unit

using UnityEngine;
using System.Collections;

public class CameraFollowUnit : MonoBehaviour 
{
    public Transform cameraTarget;
    public float easFactor = 0.3f;
    public bool setupComplete = false;

    // Update is called once per frame
    void Update()
    {
        if (cameraTarget != null)
        {
            //sets the new camera position to a transition point between the target and current camera position
            Vector3 newPos = Vector3.Lerp(transform.position, cameraTarget.position, easFactor);
            newPos.z = transform.position.z;
            transform.position = newPos;
        }
    }
}
