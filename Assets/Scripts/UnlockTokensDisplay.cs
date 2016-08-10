using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[ExecuteInEditMode]
public class UnlockTokensDisplay : MonoBehaviour 
{
    Text m_text;
    PlayerData m_data;
	// Use this for initialization
	void Start () 
    {
        m_data = GameObject.Find("ApplicationManager").GetComponent<Data>().GetPlayerData();
        m_text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        m_text.text = "Tokens: " + m_data.m_unlocktokencount.ToString();
	}
}
