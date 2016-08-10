using UnityEngine;
using System.Collections;

/// CLASS HEALTH 
/// 
/// This class represents the player's health in the game and how the health is managed. This class also 
/// sends a message to our game manager if the health has gone under zero. To make the game easier to play
/// and more fun, a recharge system is in place so the player, if skilled enough, can survive. 
public class Health : MonoBehaviour 
{
    GameData m_data;
    PlayerData m_playerdata;
    GameManager m_manager;
    float m_timer;
    float m_hittimer;
    [SerializeField]
    bool m_canrecharge;

    /// START 
    /// 
    /// This function simply initialises our components required for this script. 
	void Start () 
    {
        m_playerdata = GameObject.Find("GameManager").GetComponent<Data>().GetPlayerData();
        m_manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        m_data = m_manager.gameObject.GetComponent<Data>().GetGameData();
	}

    /// UPDATE
    /// 
    /// This function acts as our recharge system for the player's health. Essentially we use two timers and 
    /// two flags. If the timer is over 5 seconds and the player can recharge then we increment health each frame and max
    /// it our at 100. Also set the timer to zero. Then we check if the player cant recharge and if not we check if our hit 
    /// timer is more than 3 seconds and if so we set canrecharge to true again. Else we increment the timer. 
    void Update()
    {
        m_timer += Time.deltaTime;

        if (m_timer > 5 && m_canrecharge)
        {
            m_playerdata.m_health++;

            if (m_playerdata.m_health > 100)
            {
                m_playerdata.m_health = 100;
                m_timer = 0;
            }
        }

        if (!m_canrecharge)
        {
            if (m_hittimer > 3)
            {
                m_hittimer = 0;
                m_canrecharge = true;
            }
            else
            {
                m_hittimer += Time.deltaTime;
            }
        }
    }

    /// ONHIT
    /// 
    /// This function is called when a message is sent to call it. The passed in damage is 
    /// subtracted from our player's health and our multiplier is set to 1. The hit timer is also 
    /// set to zero so the player cant recharge again and we set can recharge to false. If the player's health is 
    /// below zero, we set the player to inactive and send a message to the game manager to end the game. 
    void OnHit(int _damage)
    {
        m_playerdata.m_health -= _damage;
        m_data.m_multiplier = 1;
        m_hittimer = 0;
        m_canrecharge = false;
        if (m_playerdata.m_health <= 0)
        {
            m_playerdata.m_health = 0;
            gameObject.SetActive(false);
            m_manager.gameObject.SendMessage("GameEnded");
        }
    }
}
