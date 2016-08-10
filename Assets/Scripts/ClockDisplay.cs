using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// CLASS CLOCKDISPLAY
/// 
/// This class simply displays the clock variable in our game data class to the 
/// canvas. We require the use of the GameData class to going back to an imcomplete 
/// round is easier and more convinient for the player. We set this to execute in the editor 
/// so the development process is easier. 
[ExecuteInEditMode]
public class ClockDisplay : MonoBehaviour 
{
    Text m_text;
    GameData m_data;

	/// START
	/// 
	/// This is used to get our components required for outputting the 
    /// seconds passed in our game. the data is stored in the GameManager
    /// object so we use find to locate this component.
	void Start () 
    {
        m_text = GetComponent<Text>();
        m_data = GameObject.Find("GameManager").GetComponent<Data>().GetGameData();
	}
	
	/// UPDATE
	/// 
	/// This function simply sets the text component to display our clock variable. 
    /// We add this variable to our string "Time: "
	void Update ()
    {
        m_text.text = "Time: " + m_data.m_clock;
	}
}
