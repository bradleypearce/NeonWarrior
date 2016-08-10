using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// CLASS MULTIPLIERDISPLAY
/// 
/// This class simply displays the multiplier to the UI using text.
[ExecuteInEditMode]
public class MultiplierDisplay : MonoBehaviour 
{
    Text m_text;
    GameData m_data;
    
    /// START
    ///
    /// This function is used initialise our variables. We use the game data to retrieve the 
    /// current multiplier. 
    void Start()
    {
        m_text = GetComponent<Text>();
        m_data = GameObject.Find("GameManager").GetComponent<Data>().GetGameData();
    }

    /// UPDATE 
    /// 
    /// This function simply outputs the multiplier as a string to the text component.
    void Update()
    {
        m_text.text = "x : " + m_data.m_multiplier;
    }
}
