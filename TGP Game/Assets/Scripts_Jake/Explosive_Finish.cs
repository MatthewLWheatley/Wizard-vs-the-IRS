using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive_Finish : MonoBehaviour
{
    bool animating = false;
    public GameObject m_AOEffect;
    public float m_Lifespan = 1f;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void OnCollisionEnter(Collision collision)
    {
        animating = true;
        Quaternion baseRotation = Quaternion.LookRotation(transform.forward, Vector3.up);

        Instantiate<GameObject>(m_AOEffect, transform.position + (transform.forward * cam.nearClipPlane * 2f), baseRotation);
    }

    void Update()
    {
        if (animating)
        {
            m_Lifespan -= 1 * Time.deltaTime;
            if(m_Lifespan <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
