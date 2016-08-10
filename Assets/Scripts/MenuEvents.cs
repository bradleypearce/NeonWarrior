using UnityEngine;
using System.Collections;

/// MENUEVENTS
/// 
/// This class is used for key events in our menu scene when ui buttons are pressed. 
public class MenuEvents : MonoBehaviour
{
    LevelLoader m_loader;
    PlayerData m_data;
    Data m_saver;

    /// START
    /// 
    /// This function is used for initialising our game and player data and our Data class so we can save changes we make here.
    void Start()
    {
        m_loader = GameObject.Find("ApplicationManager").GetComponent<LevelLoader>();
        m_data = GameObject.Find("ApplicationManager").GetComponent<Data>().GetPlayerData();
        m_saver = GameObject.Find("ApplicationManager").GetComponent<Data>();
    }

    /// PLAYBUTTONPRESSED
    /// 
    /// Here we call our save player data function and then 
    /// load our map. This should only happen when the play button 
    /// in the UI is pressed. 
    public void PlayButtonPressed()
    {
        m_saver.SavePlayerData();
        m_loader.LoadLevel("map");
    }

    /// ONEXITBUTTONPRESSED 
    /// 
    /// This function calls application quit and exits the game. Before quitting, we save our data so nothing 
    /// is lost. 
    public void OnExitButtonPressed()
    {
        m_saver.SavePlayerData();
        m_saver.SaveGameData();
        Application.Quit();
    }

    /// ONUPGRADESPEED 
    ///
    /// This function is called when the speed upgrade button is pressed. It firstly checks
    /// if the player has any unlocktokens and if so takes a token from the player and adds to 
    /// the player's speed. Then for safety saves the data.
    public void OnUpgradeSpeed()
    {
        if (m_data.m_unlocktokencount > 0)
        {
            m_data.m_unlocktokencount -= 1;
            m_data.m_speed += 10;
            m_saver.SavePlayerData();
        }
    }

    /// ONUPGRADETURN 
    ///
    /// This function is called when the Turn upgrade button is pressed. It firstly checks
    /// if the player has any unlocktokens and if so takes a token from the player and adds to 
    /// the player's turnrate. Then for safety saves the data.
    public void OnUpgradeTurn()
    {
        if (m_data.m_unlocktokencount > 0)
        {
            m_data.m_unlocktokencount -= 1;
            m_data.m_turnrate += 0.1f;
            m_saver.SavePlayerData();
        }
    }

    /// ONUPGRADEFIRERATE
    ///
    /// This function is called when the firerate upgrade button is pressed. It firstly checks
    /// if the player has any unlocktokens and if so takes a token from the player and adds to 
    /// the player's firerate. Then for safety saves the data.
    public void OnUpgradeFireRate()
    {
        if (m_data.m_unlocktokencount > 0)
        {
            m_data.m_unlocktokencount -= 1;
            m_data.m_firerate -= 0.1f;
            m_saver.SavePlayerData();
        }
    }
}
