using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class ShootOnAxisInput : MonoBehaviour 
{
    public string m_HorizontalAxis = "TouchHorizontal"; 
    public string m_VerticalAxis = "TouchVertical";

    public float m_delay = 0.5f;
    float m_timer = 0.0f;
    Transform m_transform;
    bool m_canshoot = true;
    public Transform m_spawner;
    ObjectPooler m_bulletpooler;
    [SerializeField]
    float m_turnrate = 0.1f;
    PlayerData m_playerdata;

	// Use this for initialization
	void Awake () 
    {
        m_transform = GetComponent<Transform>();
        m_bulletpooler = GameObject.Find("PlayerBulletPooler").GetComponent<ObjectPooler>();
        m_turnrate = GameObject.Find("GameManager").GetComponent<Data>().GetPlayerData().m_turnrate;
        m_delay = GameObject.Find("GameManager").GetComponent<Data>().GetPlayerData().m_firerate;
       
    }
	
	// Update is called once per frame
	void Update () 
    {
        float Horizontal = -CrossPlatformInputManager.GetAxis(m_HorizontalAxis);
        float Vertical = CrossPlatformInputManager.GetAxis(m_VerticalAxis);

        float angle = Mathf.Atan2(Horizontal, Vertical) * Mathf.Rad2Deg;
        if (CrossPlatformInputManager.GetAxis(m_HorizontalAxis) != 0 || CrossPlatformInputManager.GetAxis(m_VerticalAxis) != 0)
        {
            m_transform.rotation = Quaternion.Slerp(m_transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), m_turnrate);
            if (m_canshoot)
            {
                SpawnBullets();
                m_canshoot = false;
            }
        }

        m_timer += Time.deltaTime;

        if (m_timer > m_delay)
        {
            m_canshoot = true;
            m_timer = 0.0f;
        }
        
	}

    void SpawnBullets()
    {
        GameObject bullet = m_bulletpooler.GetPooledObject();
        bullet.SetActive(true);
        bullet.transform.position = m_spawner.position;
        bullet.transform.rotation = m_spawner.rotation;
        
    }

    void AddToReactionRate()
    {
        m_turnrate += 1 * 0.01f;
    }
}
