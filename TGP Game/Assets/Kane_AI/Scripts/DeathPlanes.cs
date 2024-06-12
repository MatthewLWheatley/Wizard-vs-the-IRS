using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DeathPlanes : MonoBehaviour
{
    [SerializeField] private int m_Below;
    [SerializeField] private int m_Above;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (this.GetComponent<Transform>().position.y < m_Below || this.GetComponent<Transform>().position.y > m_Above) 
        { 
            this.gameObject.SetActive(false);
        }
    }
}
