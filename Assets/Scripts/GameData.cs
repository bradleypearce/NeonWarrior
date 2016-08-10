using UnityEngine;
using System.Collections;
using SimpleJson;
using System.IO;

/// CLASS PLAYERDATA
/// 
/// This class is essentially a container class to store our persistent variables throughout 
/// our game. These variables need to be saved out in a binary format and must be serializable. 
/// Therefore it cannot inherit from monobehaviour. These classes are essentially for accessing 
/// in our program during the game and always retrieving the data when the game is started. 
[System.Serializable]
public class PlayerData
{
    public float m_turnrate;
    public float m_firerate;
    public int m_speed;
    public int m_xp;
    public int m_level;
    public int m_unlocktokencount;
    public int m_health;
    public int m_bulletdamage;
    public int m_powerlimit;
}

/// CLASS GAMEDATA
/// 
/// This class is essentially a container class to store our persistent variables throughout 
/// our game. These variables need to be saved out in a binary format and must be serializable. 
/// Therefore it cannot inherit from monobehaviour. These classes are essentially for accessing 
/// in our program during the game and always retrieving the data when the game is started. 
[System.Serializable]
public class GameData
{
    public int m_wave;
    public int m_highscore;
    public int m_clock;
    public int m_score;
    public float m_multiplier;
    public bool m_returning;
}
