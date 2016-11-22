// Summary: object that holds up to 4 cameras and places them based on its origin
//
// Created by: Julian Glass-Pilon


using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Quadrant 
{
    //list of cameras linked to this quadrant
    private List<Camera> m_Cameras = new List<Camera>();
    public List<Camera> Cameras
    {
        get { return m_Cameras; }
    }

    //origin of this quadrant (either 0 or 0.5)
    private Vector2 m_Origin;
    public Vector2 Origin
    {
        get { return m_Origin; }
        set { m_Origin = value; }
    }

    //size fo this quadrant (either 1 or 0.5)
    private Vector2 m_Size;
    public Vector2 Size
    {
        get { return m_Size; }
        set { m_Size = value; }
    }

    private bool m_playerQuadrant;
    public bool PlayerQuadrant
    {
        get { return m_playerQuadrant; }
    }

    public Quadrant(bool playerQuadrant)
    {
        m_playerQuadrant = playerQuadrant;
    }

    /// <summary>
    /// Resizes all cameras for this quadrant based on number of cameras in quadrant
    /// </summary>
    public void ResizeCameras()
    {
        float newWidth;
        float newHeight;
        switch (m_Cameras.Count)
        {
            case 1:
                {
                    newWidth = Size.x;
                    newHeight = Size.y;
                    m_Cameras[0].rect = new Rect(Origin.x, Origin.y, newWidth, newHeight);
                    break;
                }
            case 2:
                {
                    newWidth = Size.x;
                    newHeight = Size.y / 2;
                    m_Cameras[0].rect = new Rect(Origin.x, Origin.y + newHeight, newWidth, newHeight);
                    m_Cameras[1].rect = new Rect(Origin.x, Origin.y, newWidth, newHeight);
                    break;
                }
            case 3:
                {
                    newWidth = Size.x / 2;
                    newHeight = Size.y / 2;
                    m_Cameras[0].rect = new Rect(Origin.x, Origin.y + newHeight, newWidth*2, newHeight);
                    m_Cameras[1].rect = new Rect(Origin.x, Origin.y, newWidth, newHeight);
                    m_Cameras[2].rect = new Rect(Origin.x + newWidth, Origin.y, newWidth, newHeight);
                    break;
                }
            case 4:
                {
                    newWidth = Size.x / 2;
                    newHeight = Size.y / 2;
                    m_Cameras[0].rect = new Rect(Origin.x, Origin.y + newHeight, newWidth, newHeight);
                    m_Cameras[1].rect = new Rect(Origin.x, Origin.y, newWidth, newHeight);
                    m_Cameras[2].rect = new Rect(Origin.x + newWidth, Origin.y, newWidth, newHeight);
                    m_Cameras[3].rect = new Rect(Origin.x + newWidth, Origin.y + newHeight, newWidth, newHeight);
                    break;
                }
        }
    }

    /// <summary>
    /// Adds a camera to the list
    /// </summary>
    /// <param name="cam"></param>
    public void AddCamera(Camera cam)
    {
        if (m_Cameras.Count < 4)
        {
            m_Cameras.Add(cam);
            ResizeCameras();
        }

        else
        {
            Debug.LogWarning("Too Many cameras attached to this quadrant!");
        }
    }
}
