using UnityEngine;
using System.Collections;

public class SlowTime : MonoBehaviour 
{
    public bool m_timeslowed = false;
    public float m_slomospeed;
    public bool m_allowed = false;
    public float m_timer;
    public float m_limit;

    /// START 
    /// 
    /// This function just initialises our limit variable to our saved data. 
    void Start()
    {
        m_limit = GameObject.Find("GameManager").GetComponent<Data>().GetPlayerData().m_powerlimit;
    }

	/// UPDATE
	/// 
    /// This function checks if firstly if the power up is allowed to execute and that the accelometer's z is more than 0
    /// if so, we set time slowed to true, set allowed to false, reset our timer and set the time scale multiplied by slomospeed.
    /// Otherwise, we increment the timer and check if it's gone over 15. If so we reset allowed to true. If time has been slowed down, we 
    /// increment our timer and then check if our timer has gone over the limit of our power, if so we set time slowed to false and the time scale to 1 again. 
	/// </summary>
	void Update () 
    {
        if (m_allowed && Input.acceleration.z > 0)
        {
            m_timeslowed = true;
            m_allowed = false;
            m_timer = 0;
            Time.timeScale *= m_slomospeed;
        }
        else
        {
            m_timer += Time.deltaTime;

            if (m_timer > 15)
            {
                m_allowed = true; 
            }
        }

        if (m_timeslowed)
        {
            m_timer += Time.deltaTime;

            if (m_timer > 1)
            {
                m_timeslowed = false;
                m_timer = 0;
                Time.timeScale = 1;
            }
        }
	}
}
