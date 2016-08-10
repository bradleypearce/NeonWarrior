using UnityEngine;
using System.Collections;

/// CLASS BULLETMOVEMENT 
/// 
///  The bullet movement class uses the rigidbody component of our bullet 
///  to launch the bullet forward when it spawns. We also check if the 
///  bullet has reached it's kill time, if so the bullet is set to inactive
///  and it can be spawned again from our object pool.
public class BulletMovement : MonoBehaviour 
{
    Transform m_transform;
    public float m_speed = 1;
    public float m_killtime = 3;
    Transform m_spawner;

    [SerializeField]
    float m_timer;
    Rigidbody2D m_rb;
    Vector3 m_velocity;
    bool m_hasspawned;

	/// START 
	/// 
    /// Here we initialise our components and variables. We use both the transform and 
    /// rigidbody components so we can retrieve the transform's local 'up' direction and 
    /// move the rigidbody in that direction. 
	void Start () 
    {
        m_transform = GetComponent<Transform>();
		m_timer = 0;
        m_rb = GetComponent<Rigidbody2D>();
        m_hasspawned = false;
	}
	
	/// FIXEDUPDATE
	/// 
	/// In this function, we initially check if the object has been spawned. If so we set our 
    /// velocity vector to our local 'up' direction multiplied by our speed. Giving us the force 
    /// we can apply to our object. We then add the velocity to our rigidbody, note we only want
    /// to apply this force once so we use the optional ForceMode parameter and set it to impulse.
    /// Then we set our spawned flag to true so the force is not applied again. We then increment our 
    /// timer and then check if the timer has gone past the time the bullet gets killed. If so, we set 
    /// the velocity to zero, set the object to inactive and set our flag and timer to their default values. 
    /// We use fixed update to do this as using forces can be costly. Minimizing the amount of times these calculations
    /// are done helps keep our frame rate high. 
	void FixedUpdate () 
	{
        if (!m_hasspawned)
        {
            m_velocity = m_transform.up * m_speed;
            m_rb.AddForce(m_velocity, ForceMode2D.Impulse);
            m_hasspawned = true;
        }

        m_timer += Time.deltaTime;

        if (m_timer > m_killtime)
        {
            m_rb.velocity = Vector2.zero;
            gameObject.SetActive(false);
            m_timer = 0;
            m_hasspawned = false;
        }
	}
}
