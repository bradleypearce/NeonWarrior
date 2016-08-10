using UnityEngine;
using System.Collections;

public class OnTrigger : MonoBehaviour 
{
    public bool isshootingrange; 

    void OnTriggerEnter2D(Collider2D _coll)
    {
        if (_coll.tag == "Player")
        {
            if (isshootingrange)
            {
                gameObject.SendMessageUpwards("InShootingRange", true);
                Debug.Log("called");
            }
            else
            {
                Debug.Log("called2");
                gameObject.SendMessageUpwards("PlayerIsNear", true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D _coll)
    {
        if (_coll.tag == "Player")
        {
            if (isshootingrange)
            {
                Debug.Log("called");
                gameObject.SendMessageUpwards("InShootingRange", false);
            }
            else
            {
                Debug.Log("called2");
                gameObject.SendMessageUpwards("PlayerIsNear", false);
            }
        }
    }

   
}
