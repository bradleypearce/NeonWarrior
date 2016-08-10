using UnityEngine;
using System.Collections;

/// GAMEMANAGER
/// 
/// This class handles the main game aspects to control how enemies are spawned into the world
/// and what conditions need to be met to increment waves. This script also handles exiting the game
/// or what happens when the game has been suspended. 
public class GameManager : MonoBehaviour 
{
    ObjectPooler m_enemypooler;
    GameData m_data;
    LevelLoader m_loader;
    public Transform m_spawnlocation0;
    public Transform m_spawnlocation1;
    public Transform m_spawnlocation2;
    public Transform m_spawnlocation3;
    float m_waveclock;
    float m_wavetimer;
    float m_spawntimer;
    float m_timer;
    bool m_waveactive;
    bool m_gameended;
    int m_waveendenemycount;
  
	/// START 
    /// 
	/// Here we initialise our variables and required components such as our enemy object pool 
    /// so we can spawn enemies and load / save data ect. We check also if the data's returning variable
    /// is false and if so we reset our wave and score ect. so the game acts normally on start up. Otherwise
    /// we reset the user back to the wave they left off at. 
	void Start ()
    {
        m_enemypooler = GetComponent<ObjectPooler>();
        m_data = GetComponent<Data>().GetGameData();
        m_loader = GameObject.Find("ApplicationManager").GetComponent<LevelLoader>();

        m_waveactive = false;
        m_gameended = false;
        m_spawntimer = 0;

        if (!m_data.m_returning)
        {
            m_data.m_clock = 0;
            m_data.m_multiplier = 1;
            m_data.m_score = 0;
            m_data.m_wave = 0;
            m_gameended = false;
        }
        else
        {
            m_data.m_wave -= 1;

            if (m_data.m_wave < 0)
            {
                m_data.m_wave = 0;
            }
        }
	}
	
	/// UPDATE
	/// 
    /// Here we control the logic of the game by first of all checkin if the game has ended 
    /// and if not we increment our timer. Then check if our score is more than our highscore
    /// and if so we set the highscore to the current score. Then we check if our timer has gone over 1 and if so
    /// we reset the timer and add to our clock variable. This will act as a timer in seconds for the player and 
    /// adds to the overall score based on the amount of time the player survived. Then we check if the wave timer 
    /// is more than 5 seconds and if so we set the waveactive flag to true, set our wave clock to zero and wave timer to zero, increment our wave variable
    /// and set the amount of enemies to spawn at the end of the wave to 3 multiplied by our wave count. Then, if the wave is active, we increment our spawn 
    /// and wave clocks and check if our spawn timer is more than 5 seconds, if so we set the spawn timer to zero and check if the wave has been active for less than 20
    /// seconds and if our wave end enemy count is more than 0. If so we spawn an enemy. If that's not true we check if the waveclock is more than 20 seconds and if so
    /// we spawn and enemy and decrement our wave end count. This way, our waves don't end untill the player has survived for more tha 20 seconds and the length of each wave
    /// should get bigger due to the amount of enemies that spawn in over time afterwards. Then we also check if the enemy count is 0 and if so we set the wave to inactive. 
    /// If the wave is not active then we set our timer between each wave to increment each frame untill the next wave begins.
	void Update ()
    {

        m_timer += Time.deltaTime;

        if (m_data.m_score > m_data.m_highscore)
        {
            m_data.m_highscore = m_data.m_score;
        }

        if (m_timer > 1)
        {
            m_timer = 0;
            m_data.m_clock++;
        }

        if (m_wavetimer > 5)
        {
            m_waveactive = true;
            m_waveclock = 0;
            m_data.m_wave++;
            m_waveendenemycount = 3 * m_data.m_wave;
            m_wavetimer = 0;
        }

        if (m_waveactive)
        {
            m_spawntimer += Time.deltaTime;
            m_waveclock += Time.deltaTime;

            if (m_spawntimer > 5)
            {
                m_spawntimer = 0;

                if (m_waveclock < 20 && m_waveendenemycount > 0)
                {
                    SpawnEnemy();
                }
                else if (m_waveclock > 20 && m_waveendenemycount > 0)
                {
                    SpawnEnemy();
                    m_waveendenemycount--;
                }
                else if (m_waveendenemycount == 0)
                {
                    m_waveactive = false;
                }

            }
        }
        else
        {
            m_wavetimer += Time.deltaTime;
        }
       
	}

    /// SPAWNENEMY
    /// 
    /// This function simply grabs an enemy from the enemy pool 
    /// sets the enemy to active and then, based on a random number between 
    /// 1 and 4 (0 and 3 starting at 0) and spawns the enemy at 1 of 4 different
    /// locations. 
    void SpawnEnemy()
    {
        GameObject enemy = m_enemypooler.GetPooledObject();
        enemy.SetActive(true);

        int rand = Random.Range(0, 3);
        if (rand == 0)
        {
            enemy.transform.position = m_spawnlocation0.position;

        }
        else if (rand == 1)
        {
            enemy.transform.position = m_spawnlocation1.position;
        }
        else if (rand == 2)
        {
            enemy.transform.position = m_spawnlocation2.position;
        }
        else if (rand == 3)
        {
            enemy.transform.position = m_spawnlocation3.position;
        }
    }

    /// HASGAMEENDED
    /// 
    /// Function to check if game has ended. Returns the value game ended flag.
    public bool HasGameEnded()
    {
        return m_gameended;
    }

    /// GAMEENDED
    /// 
    /// This function waits for a message from the player to end the game. Once the 
    /// function has been called, we add a bonus score based on the wave the player got 
    /// to and the amount of seconds they survived. Then we set the gameended bool to true
    /// and to ensure the game initializes correctly next game we set the returning bool to false.
    /// We then send a mesage to save the game and play data and load back to our menu. The game end bool
    /// is there for later expansion so the game doesnt end immediately after you die. 
    void GameEnded()
    {
        int bonus = (m_data.m_clock + m_data.m_wave) * 10;
        m_data.m_score += bonus;
        m_gameended = true;
        m_data.m_returning = false;
        gameObject.SendMessage("SaveGameData");
        gameObject.SendMessage("SavePlayerData");
        m_loader.LoadLevel("menu");
    }

    /// GAMELEFT
    /// 
    /// This function when called sets the returning variable to true and sends a message to 
    /// save our game nad player data. It then goes back to the menu so the player can go back when they like.
    void GameLeft()
    {
        m_data.m_returning = true;
        gameObject.SendMessage("SaveGameData");
        gameObject.SendMessage("SavePlayerData");
        m_loader.LoadLevel("menu");
    }

    /// ONAPPLICATIONPAUSE
    /// 
    /// This function is called when the app is suspended,
    /// we called the game left function when this is called which 
    /// takes us back to the menu but we can return to our current game
    /// when we play again.
    void OnApplicationPause(bool _pauseStatus)
    {
        GameLeft();
    }
}
