using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Self_Destruct : MonoBehaviour
{
    public float m_Lifespan = 0.5f;
    [SerializeField] private GameObject m_ObjectToDestroy;
    [SerializeField] private ParticleSystem m_ParticleSystem;

    void Update()
    {
        m_Lifespan -= 1 * Time.deltaTime;
        if (m_Lifespan <= 0)
        {
            if (m_ObjectToDestroy != null)
            {
                Destroy(m_ObjectToDestroy);

                if (m_ParticleSystem != null)
                {
                    m_ParticleSystem.loop = false;
                    m_Lifespan = m_ParticleSystem.startLifetime;
                }

                Destroy(gameObject, m_Lifespan);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
