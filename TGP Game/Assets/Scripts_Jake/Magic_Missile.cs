using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Magic_Missile : MonoBehaviour
{
    private GameObject[] m_nearestEnemy;

    private float m_distance, m_howClose = 1000f;
    private int m_currentClosest;
    private float m_minDistance = 10f;

    void Awake()
    {
        m_nearestEnemy = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < m_nearestEnemy.Length; i++)
        {
            m_distance = Vector3.Distance(m_nearestEnemy[i].transform.position, transform.position);
            if (m_distance < m_minDistance & m_distance < m_howClose)
            {
                m_currentClosest = i;
                m_howClose = m_distance;
            }
        }
    }

    private void FixedUpdate()
    {
        if (m_currentClosest != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, m_nearestEnemy[m_currentClosest].transform.position, 0.05f);
        }
    }
}
