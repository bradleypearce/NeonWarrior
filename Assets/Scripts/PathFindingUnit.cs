using UnityEngine;
using System.Collections;

/// CLASS PATHFINDINGUNIT
/// 
/// This class is responsible for requesting paths from the pathrequestmanager and making the 
/// enemy follow the path given back by the requet manager. 
public class PathFindingUnit : MonoBehaviour 
{
    Transform m_target;
    Transform m_transform;
    public float m_speed;
    Vector3[] m_path;
    Vector3[] m_temppath;
    int m_targetindex;
    Rigidbody2D m_rb;
    Collider2D m_coll;
    [SerializeField]
    bool m_nearplayer = false;
    [SerializeField]
    bool m_inshootingrange = false;
    float m_timer = 1;
    float m_firetimer = 0;
    public float m_delay = 1;

    /// AWAKE
    /// 
    /// This function initialises our required components.
    /// </summary>
    void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_coll = GetComponentInChildren<Collider2D>();
        m_target = GameObject.Find("player").transform;
        m_transform = GetComponent<Transform>();
    }

    /// UPDATE 
    /// 
    /// This function first of the increments our timers for firing and path requests and 
    /// we then check if the player is not near the enemy and the timer is more than one second 
    /// and if so we request a new path from the request manager and we set the timer to zero. This 
    /// not only is optimal for performance, the movement of the enemies is also improved greatly. 
    /// We then check if the player is in shooting range of the player and if the firetimer is more than
    /// the fire delay. If so we set fire timer to  0 and then send a message to fire a bullet. If the player
    /// is in the shooting range of the enemy, then we also rotate the enemy in the direction of the player so 
    /// the bullet is fired accurately. We calculate this by minusing the position of the enemy from the target and normalizing 
    /// the result. Giving us the direction we want. Then we get the angle we want to rotate by and then apply a smoothed quaternion 
    /// to our rotation. This helps the enemy aim at the player. 
    void Update()
    {
        m_timer += Time.deltaTime;
        m_firetimer += Time.deltaTime;

        if (!m_nearplayer && m_timer >= 1)
        {
            m_timer = 0;
            PathRequestManager.RequestPath(new Vector2(transform.position.x, transform.position.y), m_target.position, OnPathFound);
        }

        if (m_inshootingrange && m_firetimer > m_delay)
        {
            m_firetimer = 0;
            gameObject.SendMessage("FireBullet");
        }

        if (m_inshootingrange)
        {
            Vector3 relativedir = (m_target.position - m_transform.position).normalized;
           

            float angle = Mathf.Atan2(relativedir.y, relativedir.x) * Mathf.Rad2Deg - 90;

            m_transform.rotation = Quaternion.Slerp(m_transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), 0.1f);
        }
 
    }

    /// INSHOOTINGRANGE
    /// 
    /// Returns if the player is in the shooting range of the enemy using a child trigger.
    /// 
    public void InShootingRange(bool _isinrange)
    {
        m_inshootingrange = _isinrange;
    }

    /// ISPLAYERNEAR 
    /// 
    /// Returns if the player is in the range of the enemy using a child trigger (slightly smaller than the shooting range one).
    public void PlayerIsNear(bool _isnear)
    {
        m_nearplayer = _isnear;
    }

    /// ONPATHFOUND
    /// 
    /// This functin takes an array of positions and a success variable, this is used as our callback function one a path has been found
    /// we firstly check if it was successful and if so we check if the path isnt null and if so we assign our temppath to our passed in path. 
    /// else we just assign the path to our passed in path. We then set the target index to 0 (the start of the new path). Then we stop the old coroutine
    /// begin the coroutine follow path. 
    void OnPathFound(Vector3[] _newpath, bool _success)
    {
        if (_success)
        {
            if (m_path != null)
            {
                m_temppath = _newpath;
            }
            else
            {
                m_path = _newpath;
            }
            m_targetindex = 0;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    /// FOLLOWPATH 
    /// 
    /// This function moves the player along the path as best as possible. We firstly check if our temppath is not null and if so we assign our temppath 
    /// to path and set temppath to null again. Then we set our currentwaypoint to the start of the path. Then while were not near the player, we check if 
    /// the position of the player is equal to our currentwaypoint and if so we set our target index up one. Then we check if our target index is the maximum
    /// length of the path and if so, the path becomes null and we stop the coroutine. Otherwise, we set the current way point as our path at target index. 
    /// After that, we add force to our rigidbody in the direction of the waypoint multiplied by speed. If the enemy is near the player then the coroutine will
    /// return.
    IEnumerator FollowPath()
    {
        if (m_temppath != null)
        {
            m_path = m_temppath;
            m_temppath = null;
        }
        Vector3 currentWaypoint = m_path[0];

        while (!m_nearplayer)
        {
            if (transform.position == currentWaypoint)
            {
                m_targetindex++;
                if (m_targetindex >= m_path.Length)
                {
                    m_path = null;
                    yield break;
                }
                currentWaypoint = m_path[m_targetindex];
            }

            m_rb.AddForce((currentWaypoint - transform.position).normalized * m_speed);
            yield break;

        }
        
        if(m_nearplayer)
        {
            yield return null;
        }
    }
}
