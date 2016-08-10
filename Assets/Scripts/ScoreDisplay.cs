using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ScoreDisplay : MonoBehaviour
{
    Text m_text;
    GameData m_data;
	// Use this for initialization
	void Start () 
    {
        m_text = GetComponent<Text>();
        m_data = GameObject.Find("GameManager").GetComponent<Data>().GetGameData();
	}
	
	// Update is called once per frame
	void Update () 
    {
        m_text.text = "Score: " + m_data.m_score;
	}
}
