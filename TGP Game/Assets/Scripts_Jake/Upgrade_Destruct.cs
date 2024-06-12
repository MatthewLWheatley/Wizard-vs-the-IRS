using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade_Destruct : MonoBehaviour
{
    [SerializeField] private float m_Lifespan;
    [SerializeField] private float m_DeathDelay = 1.33f;
    [SerializeField] private Animator m_Animator;

    private void Start()
    {
        m_Lifespan = 7f + GetComponent<Damage>().m_level;
    }

    void FixedUpdate()
    {
        m_Lifespan -= 1 * Time.fixedDeltaTime;

        if (m_Lifespan <= 0)
        {
            m_Animator.SetTrigger("WallDrop");
            Destroy(gameObject, m_DeathDelay);
        }
    }
}
