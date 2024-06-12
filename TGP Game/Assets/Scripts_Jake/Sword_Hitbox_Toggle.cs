using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_Hitbox_Toggle : MonoBehaviour
{
    public string fire;
    public Collider m_Collider;
    public float hitBoxTime = 0f;
    public float animationLength = 1.1f;

    void Start()
    {
        m_Collider = GetComponent<Collider>();
    }

    void Update()
    {
        if (hitBoxTime > 0f)
        {
            m_Collider.enabled = true;
            hitBoxTime -= Time.deltaTime;
        }
        else
            m_Collider.enabled = false;
    }
}
