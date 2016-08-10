using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[ExecuteInEditMode]
public class PowerUpReady : MonoBehaviour 
{
    Text m_text;
    SlowTime m_power;

	// Use this for initialization
	void Start ()
    {
        m_power = GameObject.Find("player").GetComponent<SlowTime>();
        m_text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (m_power.m_allowed)
        {
            m_text.text = "Slo-mo Active";
        }
        else
        {
            if (m_power.m_timer < m_power.m_limit && m_power.m_timeslowed)
            {
                m_text.text = (m_power.m_limit - m_power.m_timer).ToString();
            }
            else
            {
                m_text.text = "";
            }
        }
	}
}
