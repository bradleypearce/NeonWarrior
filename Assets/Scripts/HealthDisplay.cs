using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// HEALTHDISPLAY
/// 
/// THis function simply displays our current health variable to the UI.
/// We use the execute in edit mode macro to debug easily. 
[ExecuteInEditMode]
public class HealthDisplay : MonoBehaviour 
{
    Text m_text;
    PlayerData m_data;

	/// START
	/// 
    /// This function simply gets a data file and our text component.
	/// </summary>
	void Start () 
    {
        m_text = GetComponent<Text>();
        m_data = GameObject.Find("GameManager").GetComponent<Data>().GetPlayerData();
	}
	
	/// UPDATE
	/// 
	/// This function simply sets the text element to output the current value of our health. 
	void Update () 
    {
        m_text.text = "Health: " + m_data.m_health.ToString();
	}
}
