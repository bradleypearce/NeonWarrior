using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// CLASS OBJECT_POOLER 
///
/// This class has been created to avoid instantiating objects at runtime 
/// and instead, allocate memory for objects before the game starts. This
/// increases efficiency by avoiding garbage collection and instantiation 
/// while the game is running as much as possible. Instead, we reuse previously
/// instantiated objects (while the game loads) and set them to inactive once 
/// as and when necessary. 
[System.Serializable]
public class ObjectPooler : MonoBehaviour 
{
    public int m_poolcount = 50;

    public int m_maxpoolcount = 100;

    public GameObject m_pooledobject;

    public bool m_willgrow = false;

    [SerializeField]
    List<GameObject> m_list;

    /// FUNCTION START
    /// 
    /// For our start function, we define our list where our pooled objects will be stored
    /// as a new list. We then instantiate an object to the amount of pooled objects we 
    /// want to instantiate, which is set in the editor. Within this loop, we set each 
    /// of our instantiated objects to inactive so they will not be all used immediately. 
    /// We then add our instantiated object to the pool. 
	void Awake ()
    {
        m_list = new List<GameObject>();

        for (int i = 0; i < m_poolcount; i++)
        {
            GameObject obj = (GameObject)Instantiate(m_pooledobject);
            obj.SetActive(false);
            m_list.Add(obj);
        }    
	}

    /// FUNCTION GET_POOLED_OBJECT
    /// 
    /// This function is useful for retrieving a pooled object not in use. This will act as our 
    /// faux instantiation and will enable us to activate and deactivate objects from classes that
    /// will utilise the object pooler. Firstly, we iterate to the size of our list in a loop and 
    /// return the first item in the list that isn't active. Sometimes we may want our object pooler
    /// to grow dynamically if needed to accomodate different needs as well as being as efficient as 
    /// possible for every object is may pool. We check if our will-grow bool is set to true and if our 
    /// list isn't over our predefined maximum size, if so we instantiate a new pooled object and add this 
    /// to our list, then return it. If neither of the first two conditions are true, we return nothing.
    /// The max pool count acts as a safe guard so our pooler doesn't infinitely expand and cause performance 
    /// issues when our game is running.
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < m_list.Count; i++)
        {
            if (!m_list[i].activeInHierarchy)
            {
                return m_list[i];
            }
        }

        if (m_willgrow && m_list.Count <= m_maxpoolcount)
        {
            GameObject obj = (GameObject)Instantiate(m_pooledobject);
            m_list.Add(obj);
            return obj;
        }

        return null;
    }
}
