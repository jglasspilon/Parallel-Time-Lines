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
	#endregion

	#region Public Functions
	#endregion

	#region Protected Functions
	#endregion

	#region Private Function
	#endregion
}
