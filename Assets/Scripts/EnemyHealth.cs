using UnityEngine;
using System.Collections;

/// CLASS ENEMYHEALTH 
/// 
/// This class is useful for our combat system. This class essentially waits for a message to call OnHit.
/// If a message is received, we take away the damage given. This makes taking damage after being hit 
/// simple to implement and easily can be modified without having to modify elsewhere in our scripts.
/// </summary>
public class EnemyHealth : MonoBehaviour 
{
    public int m_health;
    public int m_maxhealth;
    public int m_reward;
    PathFindingUnit m_unit;
    PlayerData m_data;
    GameData m_gamedata;

    /// START 
    /// 
    /// We use this function to initialise our components of this script. For our behaviour to work correctly
    /// we require the unit script we can set our range based flags to false when an enemy is killed. We pool our
    /// enemies to avoid instantiation so resetting these variables is essential. We require our data classes so we 
    /// can update our game stats. It's quicker to implement doing it from here however later i would like to not rely
    /// on these components here and use a message to update these variables instead. 
    void Start()
    {
        m_unit = GetComponent<PathFindingUnit>();
        m_data = GameObject.Find("GameManager").GetComponent<Data>().GetPlayerData();
        m_gamedata = GameObject.Find("GameManager").GetComponent<Data>().GetGameData();
    }

    /// ONHIT
    /// 
    /// This function is our message listener which when messaged takes the passed in damage away from our enemy's health. 
    /// Here we check if the enemy's health has gone under 1, therefore dead and gets set back to inactive and waits to be 
    /// spawned again. We also modify our range flags in our unit script and modify our data variables. The combat system is the 
    /// crucial element of the game, without the xp reward, there would be no player progression. 
    void OnHit(int _damage)
    {
        m_health -= _damage;

        if (m_health < 1)
        {
            gameObject.SetActive(false);
            m_unit.PlayerIsNear(false);
            m_unit.InShootingRange(false);
            m_health = m_maxhealth;
            m_data.m_xp += Mathf.RoundToInt(m_reward * m_gamedata.m_multiplier);
            m_gamedata.m_score += Mathf.RoundToInt(m_reward * m_gamedata.m_multiplier);
            m_gamedata.m_multiplier += 0.1f;
        }
    }
}
