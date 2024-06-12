using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateNum : MonoBehaviour
{
    [SerializeField] private GameObject m_prefab; 
    void Start()
    {
        m_prefab.GetComponentInChildren<FloatAndFade>().m_IsCritical = true;
        Instantiate(m_prefab);
    }

}
