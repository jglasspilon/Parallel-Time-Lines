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
    public Text deathDescription;
    public Text lost;
    public Text lostDescription;
    public Text win;
    public Text winDescription;
    public Button backToHome;
    #endregion

    #region Auto Properties
    public bool EndReached { get; set; }
    public bool PlayerWon { get; set; }
    #endregion

    #region Unity Functions
    public void Start()
    {
        //find all proper references and initialize all the controler scripts (i.e. cameraManager, etc)
        m_CameraManager = GetComponent<CameraManager>();
        m_CameraManager.InitializeCamera();

        EndReached = false;
        PlayerWon = false;
    }

    public void Update()
    {
        m_CameraManager.RemoveCamera();
        m_CameraManager.RemoveQuadrant();

        if (EndReached)
        {
            if(PlayerWon)
                StartCoroutine(DisplayUI(you, win, winDescription));
            else
                StartCoroutine(DisplayUI(you, lost, lostDescription));
        }
        else if (!EndReached && PlayerCam.GetComponent<CameraFollowUnit>().cameraTarget == null)
        {
            StartCoroutine(DisplayUI(you, died, deathDescription));
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
    private IEnumerator DisplayUI(Text word1, Text word2, Text description)
    {
        playerDeathUI.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        word1.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.3f);

        word2.gameObject.SetActive(true);
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
