// Summary: Holds any relevant data about a camera

using UnityEngine;
using System.Collections;

public class CameraData : MonoBehaviour 
{
    #region Fields
    private int m_Id;
    private GameObject m_TargetCharacter;

    public int Id
    {
        get { return m_Id; }
        set { m_Id = value; }
    }

    public GameObject TargetCharacter
    {
        get { return m_TargetCharacter; }
        set { m_TargetCharacter = value; }
    }
	#endregion

    public void LateUpdate()
    {
        if(m_Id != 0 && m_TargetCharacter == null)
        {
            Destroy(this.gameObject);
        }
    }
}
