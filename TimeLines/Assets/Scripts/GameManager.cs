// Summary: Controls all game related behaviour

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
    #region Fields
    private CameraManager m_CameraManager;
    public Camera PlayerCam;

    public Canvas playerDeathUI;
    public Text you;
    public Text died;
    public Text description;
    public Button backToHome;
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
        m_CameraManager.RemoveCamera();
        m_CameraManager.RemoveQuadrant();

        if(PlayerCam.GetComponent<CameraFollowUnit>().cameraTarget == null)
        {
            StartCoroutine(DisplayDeathUI());
        }
    }
    #endregion

    #region Public Functions
    public void CreateAlternateTimeline(Transform spawnLocation)
    {
        GameObject newPlayer = Factory.PlayerFactory.CreateAlternatePlayer(spawnLocation);
        newPlayer.GetComponent<CharacterBehaviour>().activeCam = m_CameraManager.AddCamera(newPlayer);
        
    }

    #endregion

    #region Private Function
    private IEnumerator DisplayDeathUI()
    {
        playerDeathUI.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        you.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.3f);

        died.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        description.gameObject.SetActive(true);
        backToHome.gameObject.SetActive(true);

    }
    #endregion

    #region Test Functions
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
    #endregion
}
