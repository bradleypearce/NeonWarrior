using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

/// CLASS DATA
/// 
/// This class enables our data to be persistant through the use of binary formatting. The Binary writer
/// and reader classes make it very convinient and fast to save and load data between scenes and when the 
/// application has been shut down. We store both our player and our game data in this class so they are more
/// accessible in our other classes. We set this to execute in the editor to debug more efficiently.
[ExecuteInEditMode]
public class Data : MonoBehaviour 
{
    [SerializeField]
    PlayerData m_playerdata;
    [SerializeField]
    GameData m_gamedata;
    string m_playersavepath;
    string m_gamesavepath;

	/// AWAKE
	/// 
    /// We initially set our save paths for each of our save files using the persistent data path unity
    /// provides. We then call the LoadData functions for each of our save files. 
	void Awake() 
    {
        m_playersavepath = Application.persistentDataPath + "/playersavedata.sav";
        m_gamesavepath = Application.persistentDataPath + "/gamesavedata.sav";
        //SavePlayerData();
        //SaveGameData();
        LoadPlayerData();
        LoadGameData();
	}

    /// SAVEPLAYERDATA
    /// 
    /// For this function, we initially create a new Binary writer local to the function. 
    /// We then, for each variable in our player data class, we write to our file. After this 
    /// is done we closed the writer. 
    /// </summary>
    public void SavePlayerData()
    {
        BinaryWriter m_writer = new BinaryWriter(File.OpenWrite(m_playersavepath));
        m_writer.Write(System.Convert.ToSingle(m_playerdata.m_turnrate));
        m_writer.Write(System.Convert.ToSingle(m_playerdata.m_firerate));
        m_writer.Write(System.Convert.ToInt32(m_playerdata.m_speed));
        m_writer.Write(System.Convert.ToInt32(m_playerdata.m_xp));
        m_writer.Write(System.Convert.ToInt32(m_playerdata.m_level));
        m_writer.Write(System.Convert.ToInt32(m_playerdata.m_unlocktokencount));
        m_writer.Write(System.Convert.ToInt32(m_playerdata.m_health));
        m_writer.Write(System.Convert.ToInt32(m_playerdata.m_bulletdamage));
        m_writer.Write(System.Convert.ToInt32(m_playerdata.m_powerlimit));
        m_writer.Close();
    }

    /// SAVEGAMEDATA
    /// 
    /// For this function, we initially create a new Binary writer and for each variable in our 
    /// GameData class, same as for our player data class, we write to our gamedata file. After this 
    /// is done we closed the writer. 
    public void SaveGameData()
    {
        BinaryWriter m_writer = new BinaryWriter(File.OpenWrite(m_gamesavepath));
        m_writer.Write(System.Convert.ToInt32(m_gamedata.m_highscore));
        m_writer.Write(System.Convert.ToInt32(m_gamedata.m_wave));
        m_writer.Write(System.Convert.ToInt32(m_gamedata.m_clock));
        m_writer.Write(System.Convert.ToInt32(m_gamedata.m_score));
        m_writer.Write(System.Convert.ToSingle(m_gamedata.m_multiplier));
        m_writer.Write(System.Convert.ToBoolean(m_gamedata.m_returning));
        m_writer.Close();
    }

    /// LOADPLAYERDATA
    /// 
    /// For this function we initialise a binary reader local to the function and for each variable
    /// in our playerdata class, we read from our file path. After this is done we close the 
    /// reader.
    public void LoadPlayerData()
    {
        BinaryReader m_reader = new BinaryReader(File.OpenRead(m_playersavepath));
        m_playerdata.m_turnrate = m_reader.ReadSingle();
        m_playerdata.m_firerate = m_reader.ReadSingle();
        m_playerdata.m_speed = m_reader.ReadInt32();
        m_playerdata.m_xp = m_reader.ReadInt32();
        m_playerdata.m_level = m_reader.ReadInt32();
        m_playerdata.m_unlocktokencount = m_reader.ReadInt32();
        m_playerdata.m_health = m_reader.ReadInt32();
        m_playerdata.m_bulletdamage = m_reader.ReadInt32();
        m_playerdata.m_powerlimit = m_reader.ReadInt32();
        m_reader.Close();
    }

    /// LOADGAMEDATA
    /// 
    /// For this function we initialise a binary reader local to the function and for each variable
    /// in our gamedata class, we read from our file path. After this is done we close the 
    /// reader.
    public void LoadGameData()
    {
        BinaryReader m_reader = new BinaryReader(File.OpenRead(m_gamesavepath));
        m_gamedata.m_highscore = m_reader.ReadInt32();
        m_gamedata.m_wave = m_reader.ReadInt32();
        m_gamedata.m_clock = m_reader.ReadInt32();
        m_gamedata.m_score = m_reader.ReadInt32();
        m_gamedata.m_multiplier = m_reader.ReadSingle();
        m_gamedata.m_returning = m_reader.ReadBoolean();
        m_reader.Close();
    }

    /// GETPLAYERDATA
    /// 
    /// Returns a reference to our playerdata class. 
    public PlayerData GetPlayerData()
    {
        return m_playerdata;
    }

    /// GETGAMEDATA 
    /// 
    /// Returns a reference to our gamedata class. 
    public GameData GetGameData()
    {
        return m_gamedata;
    }
}
