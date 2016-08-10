using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[ExecuteInEditMode]
public class RankDisplay : MonoBehaviour 
{
    Text m_text;
    PlayerData m_data;

	// Use this for initialization
	void Start ()
    {
        m_text = GetComponent<Text>();
        m_data = GameObject.Find("ApplicationManager").GetComponent<Data>().GetPlayerData();
	}
	
	// Update is called once per frame
	void Update () 
    {
        m_text.text = "Level: " + m_data.m_level.ToString();
	}
}
