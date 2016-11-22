// Summary: Holds all generation helper classes

using UnityEngine;
using System.Collections;

public static class Factory
{
    public static class CameraFactory
    {
        /// <summary>
        /// Creates a camera for the alternate timelines
        /// </summary>
        /// <param name="cameraID">ID to give to camera</param>
        /// <param name="playerTarget">Target player for camera</param>
        public static GameObject CreateAlternateCamera()
        {
            GameObject newCamera = new GameObject("Alternate Camera");
            newCamera.AddComponent<Camera>();
            newCamera.AddComponent<CameraFollowUnit>();

            newCamera.transform.position = new Vector3(newCamera.transform.position.x, newCamera.transform.position.y, -10);

            return newCamera;
        }
    }

    /// <summary>
    /// Creates a player for the alternate timelines
    /// </summary>
    public static class PlayerFactory
    {
        public static GameObject CreateAlternatePlayer(Transform newTransform)
        {
            GameObject newPlayer = MonoBehaviour.Instantiate(Resources.Load("TempPlayer"), newTransform) as GameObject;
            newPlayer.transform.parent = null;
            return newPlayer;
        }
    }
}
