// Summary: Controls how many cameras are on screen and instantiates and destroys cameras when needed

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CameraManager : MonoBehaviour 
{
    #region Fields
    private List<Camera> m_cameras = new List<Camera>();
    private List<int> m_cameraIDs = new List<int>();
    private int m_MaxCameras = 13;
	#endregion

    #region Public Functions
    /// <summary>
    /// Initializes the starting camera
    /// </summary>
    public void InitializeCamera()
    {
        //get the player camera
        Camera playerCam = FindObjectOfType<Camera>();

        //get the player
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        //setup the camera with the proper player target and resize all cameras
        SetupCamera(playerCam, player);
        m_cameras.Add(playerCam);
        ResizeCameras();
    }
    #endregion

    #region Protected Functions
    #endregion

    #region Private Functions
    /// <summary>
    /// Setup the data for the camera
    /// </summary>
    /// <param name="cam">camera to setup</param>
    /// <param name="targetPlayer">player to target the camera with</param>
    private void SetupCamera(Camera cam, GameObject targetPlayer)
    {
        SetupID(cam);
        SetupCameraTarget(cam, targetPlayer);
    }

    /// <summary>
    /// Fetch the appropriate ID to set to the camera (will look for the smallest non used number from the list
    /// </summary>
    /// <param name="cam">Camera to setup</param>
    private void SetupID(Camera cam)
    {
        int nextID = 0;
        bool idExists = false;
        if (m_cameraIDs.Count > 0)
        {
            //check each ID to see if it already exists
            for (int i = 0; i < m_MaxCameras; i++)
            {
                //check to see all camera IDs in the list and compare
                for (int j = 0; j < m_cameraIDs.Count; j++)
                {
                    //if id already exists flag and exit
                    if (i == m_cameraIDs[j])
                    {
                        idExists = true;
                        return;
                    }
                }

                //if id doesn't exist use that one
                if (!idExists)
                {
                    nextID = i;
                    return;
                }
            }

            //set the ID to the cam and add that ID to the list
            cam.GetComponent<CameraData>().Id = nextID;
            m_cameraIDs.Add(nextID);
        }
    }

    /// <summary>
    /// Set the player as the target for the camera
    /// </summary>
    /// <param name="cam">camera to setup</param>
    /// <param name="targetPlayer">target player for the camera</param>
    private void SetupCameraTarget(Camera cam, GameObject targetPlayer)
    {
        //go through the player's children top find the cam target and set that as the camera's follow target
        for (int i = 0; i < targetPlayer.transform.childCount; i++)
        {
           if(targetPlayer.transform.GetChild(i).tag == "CamTarget")
            {
                cam.GetComponent<CameraFollowUnit>().cameraTarget = targetPlayer.transform.GetChild(i);
            }
        }

        //set the player as the target player for the cam
        cam.GetComponent<CameraData>().TargetCharacter = targetPlayer;
    }

    //TODO: Julian standardize the calculations so it isn't limited to 4
    /// <summary>
    /// Resizes a camera based on its ID and how many cameras are in the scene
    /// </summary>
    /// <param name="cam">Camera to resize</param>
    private void ResizeCameras()
    {

        //original full screen dimensions
        float startWidth = 1;
        float startHeight = 1;
        float startX = 0;
        float startY = 0;

        //rne dimensions to set
        float newWidth;
        float newHeight;
        float newX;
        float newY;

        //number of rows and columns of cameras
        int rows = 1;
        int columns = 1;

        //checks how many cameras are in the scene and resizes accordingly
        if (m_cameras.Count <= 4)
        {

            //determine how many rows and columns there are based on number of cameras
            if (m_cameras.Count > 1)
                columns = 2;
            if (m_cameras.Count > 2)
                rows = 2;

            //calculate new widht and height based on number of columns and rows
            newWidth = startWidth / columns;
            newHeight = startHeight / rows;

            //which row and column the current camera is in
            int columnCount = 0;
            int rowCount = 0;

            for (int i = 0; i < m_cameras.Count; i++)
            {
                //if we need to add a row
                if (i % 2 == 0 && i > 0)
                {
                    //go to next row and go back to first column
                    rowCount++;
                    columnCount--;
                }

                //if we need to add column
                if(i % 2 != 0)
                {
                    //go to next column
                    columnCount++;
                }

                //place the new x and y in the appropriate row and column based on size
                newX = startX + (columnCount * newHeight);
                newY = startY + (rowCount * newWidth);

                //set the new camera dimensions
                m_cameras[i].rect = new Rect(newX, newY, newWidth, newHeight);
            }
        }
    }

    //TODO: Julian create function to add a camera which will resize cameras at end
    //TODO: Julian create function to remove a camera which will resize the cameras at end
	#endregion
}
