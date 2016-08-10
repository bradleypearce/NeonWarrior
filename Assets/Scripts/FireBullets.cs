using UnityEngine;
using System.Collections;

/// CLASS FIREBULLETS
/// 
/// This class acts as our message observer to fire a bullet when called. 
/// We take advantage of the object pooler again to ensure no instantiation 
/// is done during the game loop. 
public class FireBullets : MonoBehaviour 
{
    ObjectPooler m_bulletpooler;
    Transform m_transform;

	/// START
	/// 
    /// Here we retrieve our required components from our gameobject using Getcomponent.
    void Start()
    {
        m_bulletpooler = GetComponent<ObjectPooler>();
        m_transform = GetComponent<Transform>();
    }

    /// FIREBULLET
    /// 
    /// This function, when messaged to call, retrieves a bullet from our bullet pooler 
    /// and sets the position and rotation to this gameobject's position and rotation and 
    /// then sets the bullet to active. 
    void FireBullet()
    {
        GameObject bullet = m_bulletpooler.GetPooledObject();
        bullet.transform.position = m_transform.position;
        bullet.transform.rotation = m_transform.rotation;
        bullet.SetActive(true);
    }
}
