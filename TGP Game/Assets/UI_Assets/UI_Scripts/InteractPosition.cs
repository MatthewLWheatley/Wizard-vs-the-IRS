using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractPosition : MonoBehaviour
{
    public Vector3 m_SpawningPosition;
    [SerializeField] private Vector3 m_Offset = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        GetComponentInChildren<Transform>().position = m_SpawningPosition+m_Offset;
    }

}
