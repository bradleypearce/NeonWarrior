using UnityEngine;
using System.Collections;

/// MAPORBIT
/// 
/// This class is used to spin our map based on a public variable set in the editor. 
/// This can be used on other objects however this isn't appropriate for anything other 
/// than my map. 
public class MapOrbit : MonoBehaviour 
{
    Transform m_transform;
    public int m_orbitamount;

	/// START
	///
    /// This function is used to initialise our transform component.
	void Start () 
    {
        m_transform = GetComponent<Transform>();
	}
	
    /// UPDATE
    /// 
    /// Here we simply use the rotate function to rotate our map by our amount mulitiplied by 
    /// deltaTime
	void Update () 
    {
        m_transform.Rotate(0, 0, m_orbitamount * Time.deltaTime);
	}
}
