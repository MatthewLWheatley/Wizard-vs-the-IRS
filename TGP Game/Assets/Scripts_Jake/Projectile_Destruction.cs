using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Destruction : MonoBehaviour
{
    [SerializeField] private GameObject m_ObjectToDestroy;
    [SerializeField] private float m_DestroyTime;
    [SerializeField] private ParticleSystem m_ParticleSystem;

    private void OnCollisionEnter(Collision collision)
    {
        // if an object has been set in object to destroy then that object will be destroyed
        // if not then the gameobject the script is on will be destroyed
        if (m_ObjectToDestroy == null)
        {
            Destroy(gameObject);
        }
        else
        {
            Destroy(m_ObjectToDestroy);
            
            if (m_ParticleSystem != null)
            {
                m_ParticleSystem.loop = false;
                m_DestroyTime = m_ParticleSystem.startLifetime;
            }

            Destroy(gameObject, m_DestroyTime);
        }
    }
}
