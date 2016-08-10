using UnityEngine;
using System.Collections;


public class OnBulletHit : MonoBehaviour 
{
    [SerializeField]
    public int m_bulletdamage = 20;

    void Start()
    {
        if (gameObject.tag == "Bullet")
        {
            m_bulletdamage = GameObject.Find("GameManager").GetComponent<Data>().GetPlayerData().m_bulletdamage;
        }
        else
        {
            m_bulletdamage = 20;
        }
    }

    void OnCollisionEnter2D(Collision2D _coll)
    {
        if (_coll.gameObject.tag == "Enemy")
        {
            _coll.gameObject.SendMessage("OnHit", m_bulletdamage);
        }
        else if (_coll.gameObject.tag == "Player")
        {
            _coll.gameObject.SendMessage("OnHit", m_bulletdamage);
        }
    }
}
