// Summary: Controls all game related behaviour

using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
    #region Fields
    private CameraManager m_CameraManager;

	#endregion

	
	#region Unity Functions
    public void Start()
    {
        //find all proper references and initialize all the controler scripts (i.e. cameraManager, etc)
        m_CameraManager = GetComponent<CameraManager>();
        m_CameraManager.InitializeCamera();
    }

    public void Update()
    {
        TestCameraAdding();
        TestRemovePlayer();
        m_CameraManager.RemoveCamera();
        m_CameraManager.RemoveQuadrant();
    }
	#endregion

	#region Public Functions
	#endregion

	#region Protected Functions
	#endregion

	#region Private Function
	#endregion

    private void TestCameraAdding()
    {
        //get the player
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (Input.GetMouseButtonDown(0))
        {
            GameObject newPlayer = Factory.PlayerFactory.CreateAlternatePlayer(player.transform);
            m_CameraManager.AddCamera(newPlayer);
        }
    }

    private void TestRemovePlayer()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if(GameObject.FindGameObjectWithTag("AlternatePlayer") != null)
                Destroy(GameObject.FindGameObjectWithTag("AlternatePlayer").gameObject);
        }
    }
}
