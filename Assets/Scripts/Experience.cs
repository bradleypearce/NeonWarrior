using UnityEngine;
using System.Collections;

/// CLASS EXPERIENCE
/// 
/// This class is a simple class which levels up the player based on the 
/// amount of xp the player has gained since last levelling up. For this 
/// we simply require the game data files so we can get the player's current level 
/// and increment it as well. We also increment our unlock tokens so the player 
/// can spend them on upgrades.
public class Experience : MonoBehaviour 
{
    PlayerData m_data;
    int m_nextlevelxp;
    int m_previouslevelxp;
    public bool m_leveledup;

	/// START
	/// 
    /// Here we simply find our data script on our manager object and get the 
    /// player data class from there.
	void Start () 
    {
        m_data = GameObject.Find("GameManager").GetComponent<Data>().GetPlayerData();
	}
	
	/// FIXEDUPDATE
	/// 
	/// Each frame, we check set our bar to get to the next level (simply the level times 1000, this
    /// can easily be changed to be more interesting) and then we set our progress to rank variable as 
    /// our current xp minus our current level (minus 1 otherwise our progress is wrong) multiplied by a thousand. 
    /// This is the amount of progress we have made since last leveling up. We then check if our currentprogress is 
    /// more than the amount of xp required and if so we set our leveled up flag to be true and increment our level and 
    /// tokencount data. We used fixed update so this script is called fewer times but still updated quickly. 
	void FixedUpdate() 
    {
        m_nextlevelxp = m_data.m_level * 1000 + m_previouslevelxp;

        if (m_data.m_xp > m_nextlevelxp)
        {
            m_leveledup = true;
            m_previouslevelxp += m_data.m_level * 1000;

            m_data.m_level++;
            m_data.m_unlocktokencount++;
        }
	}
}
