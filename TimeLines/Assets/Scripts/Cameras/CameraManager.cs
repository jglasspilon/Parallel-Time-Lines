// Summary: Controls how many cameras are on screen and instantiates and destroys cameras when needed

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CameraManager : MonoBehaviour 
{
    #region Fields
    private List<Quadrant> m_Quadrants = new List<Quadrant>();
    private int m_MaxCameras = 13;
	#endregion

    #region Public Functions
    /// <summary>
    /// Initializes the starting camera
    /// </summary>
    public void InitializeCamera()
    {
        InitialSetup();
        //TestCameras();
    }
    #endregion

    #region Private Functions
    /// <summary>
    /// Setup the data for the camera
    /// </summary>
    /// <param name="cam">camera to setup</param>
    /// <param name="targetPlayer">player to target the camera with</param>
    private void SetupCamera(Camera cam, GameObject targetPlayer)
    {
        SetupCameraTarget(cam, targetPlayer);
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
                cam.GetComponent<CameraFollowUnit>().setupComplete = true;
            }
        }
    }

    /// <summary>
    /// Initial setup for start of game camera
    /// </summary>
    private void InitialSetup()
    {
        //set up the initial quadrant
        Quadrant quad1 = new Quadrant(true);
        m_Quadrants.Add(quad1);
        ResizeQuadrant();

        //get the player camera and add it to the quadrant
        Camera playerCam = FindObjectOfType<Camera>();
        quad1.AddCamera(playerCam);

        //get the player
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        //setup the camera with the proper player target and resize all cameras
        SetupCamera(playerCam, player);
    }

    //Resizes and repositions the quadrants based on how many there are in the scene
    /// <summary>
    /// Resizes the cameras based on number of cameras in scene
    /// </summary>
    private void ResizeQuadrant()
    {
        switch (m_Quadrants.Count)
        {
            case 1:
                {
                    m_Quadrants[0].Origin = new Vector2(0,0);
                    m_Quadrants[0].Size = new Vector2(1, 1);
                    break;
                }
            case 2:
                {
                    m_Quadrants[0].Origin = new Vector2(0, 0.5f);
                    m_Quadrants[0].Size = new Vector2(1, 0.5f);

                    m_Quadrants[1].Origin = new Vector2(0, 0);
                    m_Quadrants[1].Size = new Vector2(1, 0.5f);
                    break;
                }
            case 3:
                {
                    m_Quadrants[0].Origin = new Vector2(0, 0.5f);
                    m_Quadrants[0].Size = new Vector2(1, 0.5f);

                    m_Quadrants[1].Origin = new Vector2(0, 0);
                    m_Quadrants[1].Size = new Vector2(0.5f, 0.5f);

                    m_Quadrants[2].Origin = new Vector2(0.5f, 0);
                    m_Quadrants[2].Size = new Vector2(0.5f, 0.5f);
                    break;
                }
            case 4:
                {
                    m_Quadrants[0].Origin = new Vector2(0, 0.5f);
                    m_Quadrants[0].Size = new Vector2(0.5f, 0.5f);

                    m_Quadrants[1].Origin = new Vector2(0, 0);
                    m_Quadrants[1].Size = new Vector2(0.5f, 0.5f);

                    m_Quadrants[2].Origin = new Vector2(0.5f, 0);
                    m_Quadrants[2].Size = new Vector2(0.5f, 0.5f);

                    m_Quadrants[3].Origin = new Vector2(0.5f, 0.5f);
                    m_Quadrants[3].Size = new Vector2(0.5f, 0.5f);
                    break;
                }
        }

        //resize all cameras for all quadrants
        foreach (Quadrant quad in m_Quadrants)
        {
            quad.ResizeCameras();
        }
    }

    /// <summary>
    /// Adds a new camera to the scene and resizes all cameras appropriately
    /// </summary>
    public void AddCamera(GameObject player)
    {
        //keeps track of total number of cameras
        int numberOfCameras = 0;
        for(int i = 0; i < m_Quadrants.Count; i++)
        {
            numberOfCameras += m_Quadrants[i].Cameras.Count;
        }

        //only add cameras if max number not reached
        if (numberOfCameras < m_MaxCameras)
        {
            //create a new camera and set it up properly
            GameObject newCameraObject = Factory.CameraFactory.CreateAlternateCamera();
            Camera newCam = newCameraObject.GetComponent<Camera>();
            SetupCamera(newCam, player);

            //if there are less than 4 quadrants create a new one
            if (m_Quadrants.Count < 4)
            {
                AddQuadrant(newCam);
            }

            //otherwise add camera to quadrant with fewest cameras
            else
            {
                //keeps track of quadrant with fewest cameras
                int targetQuadrant = 1;

                //cycle through all quadrants and get the one with the fewest cameras
                for (int i = 1; i < m_Quadrants.Count; i++)
                {
                    if (m_Quadrants[i].Cameras.Count < m_Quadrants[targetQuadrant].Cameras.Count)
                    {
                        targetQuadrant = i;
                    }
                }

                //add the cameras to the proper quadrant
                m_Quadrants[targetQuadrant].AddCamera(newCam);
            }
        }

        else
        {
            Debug.LogWarning("Reached maximum number of cameras allowed!");
        }
    }

    //TODO: Julian create function to remove a camera which will resize appropriately at end
    /// <summary>
    /// removes a camera from the scene and resizes all cameras appropriately
    /// </summary>
    /// <param name="cam"></param>
    public void RemoveCamera()
    {
        List<Camera> camerasToRemove = new List<Camera>();

        for(int i = 0; i < m_Quadrants.Count; i++)
        {
            for(int j = 0; j < m_Quadrants[i].Cameras.Count; j++)
            {
                if (m_Quadrants[i].Cameras[j].GetComponent<CameraFollowUnit>().cameraTarget == null)
                {
                    camerasToRemove.Add(m_Quadrants[i].Cameras[j]);
                    
                }
            }
            foreach (Camera cam in camerasToRemove)
            {
                m_Quadrants[i].Cameras.Remove(cam);
                Destroy(cam.gameObject, 0.1f);
            }
        }
        ResizeQuadrant();
    }

    /// <summary>
    /// Adds a new quadrant to the scene and resizes all the quadrants and their cameras
    /// </summary>
    private void AddQuadrant(Camera cam)
    {
        if (m_Quadrants.Count < 4)
        {
            //create a new quadrant and add it to the list
            Quadrant newQuad = new Quadrant(false);
            m_Quadrants.Add(newQuad);

            //resize the quadrants and add the new camera to it
            ResizeQuadrant();
            newQuad.AddCamera(cam);
        }

        else
        {
            Debug.LogWarning("Too many quadrants!");
        }
    }

    //TODO: Julian create function to remove a quadrant with no cameras and resize Quadrants 
    /// <summary>
    /// Removes a quadrant and resizes all quadrants and their cameras
    /// </summary>
    public void RemoveQuadrant()
    {
        List<Quadrant> quadsToRemove = new List<Quadrant>();
        for(int i = 0; i < m_Quadrants.Count; i++)
        {
            if(m_Quadrants[i].Cameras.Count == 0)
            {
                quadsToRemove.Add(m_Quadrants[i]);
                Debug.Log(m_Quadrants[i].Cameras.Count);
            }
        }

        foreach(Quadrant quad in quadsToRemove)
        {
            m_Quadrants.Remove(quad);
        }
        ResizeQuadrant();
        
    }
    #endregion

    #region Testing Functions
    /// <summary>
    /// Tests the camera resize function
    /// </summary>
    private void TestCameras()
    {
        Camera[] cameras = FindObjectsOfType<Camera>();

        //get the player
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        foreach (Camera cam in cameras)
        {
            SetupCamera(cam, player);
            AddQuadrant(cam);
        }
    }
    #endregion
}
