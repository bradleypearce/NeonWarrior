using UnityEngine;
using System.Collections;


public class ResolutionModifier : MonoBehaviour 
{
    [SerializeField]
    int m_screenwidth = 1280;
    [SerializeField]
    int m_screenheight = 720;

	// Use this for initialization
	void Start () 
	{
        Screen.SetResolution(m_screenwidth, m_screenheight, true);
	}
}
