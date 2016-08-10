using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// HIGHSCORE DISPLAY
/// 
/// This class simply displays the highscore to the screen. 
[ExecuteInEditMode]
public class HighScoreDisplay : MonoBehaviour
{
    Text m_text;
    GameData m_data;
    public bool m_isgame;

    /// START
    /// 
    /// This function simply initialises our text component and also checks if the game is active or not and finds
    /// a different game object based on a public bool set in the editor. Without this the script will not work. 
    void Start()
    {
        m_text = GetComponent<Text>();
        if (!m_isgame)
        {
            m_data = GameObject.Find("ApplicationManager").GetComponent<Data>().GetGameData();
        }
        else
        {
            m_data = GameObject.Find("GameManager").GetComponent<Data>().GetGameData();
        }
    }

   
    /// UPDATE
    /// 
    /// Simply outputs the highscore to the screen using the text component.
    void Update()
    {
        m_text.text = "HighScore: " + m_data.m_highscore;
    }
}
