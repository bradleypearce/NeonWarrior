using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

/// CLASS MOVEONAXISINPUT
/// 
/// This class handles the movement of the player by taking advantage of the dual stick prefab in the Unity Standard assets. 
/// This prefab creates virtual input axis and then, using the cross platform input manager, you can retrieve the values of
/// each axis and apply it to the rigidbody. 
public class MoveOnAxisInput : MonoBehaviour 
{
    [SerializeField]
    float m_speed;

    Rigidbody2D m_rb;
    PlayerData m_playerdata;
    SlowTime m_slomo;

	
    /// START
    /// 
    /// This function initialises our required variables.
	void Start () 
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_playerdata = GameObject.Find("GameManager").GetComponent<Data>().GetPlayerData();
        m_slomo = GetComponent<SlowTime>();
	}
	
	/// FIXEDUPDATE
	/// 
    /// This function adds a force to our rigidbody based on our virtual input axis using the sticks displayed on screen. We multiply each axis by speed
    /// so the behaviour can be modified. We used fixed update to minimise physics function calls.
	void FixedUpdate () 
    {
        if (m_slomo.m_timeslowed)
        {
            m_rb.AddForce(new Vector2(CrossPlatformInputManager.GetAxis("TouchHorizontal") * m_playerdata.m_speed * 2, CrossPlatformInputManager.GetAxis("TouchVertical") * m_playerdata.m_speed * 2), ForceMode2D.Force);
        }
        else
        {
            m_rb.AddForce(new Vector2(CrossPlatformInputManager.GetAxis("TouchHorizontal") * m_playerdata.m_speed, CrossPlatformInputManager.GetAxis("TouchVertical") * m_playerdata.m_speed), ForceMode2D.Force);
        }
	}
}
