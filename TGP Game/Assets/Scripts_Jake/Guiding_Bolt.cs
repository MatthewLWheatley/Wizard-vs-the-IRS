using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guiding_Bolt : Weapon_Base
{
    //lots of notes on poison spray code
    private Animator animator;
    public GameObject m_LightPrefab;
    public float m_FiringForce = 10f;
    private Camera cam;

    //diags are the diaganal rotation vectors
    private Vector3 projectileOrigin, projectileOrigin1, projectileOrigin2;
    private float X = 0, Y = 1, Z = 0.3f;
    private float X1 = 0.3f;
    private float X2 = -0.3f;


    Quaternion Diag, Diag2;

    void Start()
    {
        cam = Camera.main;
        projectileOrigin = new Vector3(X, Y, Z);
        projectileOrigin1 = new Vector3(X1, Y, Z);
        projectileOrigin2 = new Vector3(X2, Y, Z);
        animator = GetComponent<Animator>();

        //the other 2 bolts are going at a 35 degree rotation from the point of origin and the original bolt
        Diag2 = Quaternion.Euler(0f, -25f, 0f);
        Diag = Quaternion.Euler(0f, 25f, 0f);
        //Diag2 = new Vector3(-25f, 0f, 0f);
        level = 1;
        rTime = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        reload -= rTime * Time.deltaTime;
        if (reload <= 0)
        {
            reload = 0;
            if (Input.GetMouseButtonDown(fire))
            {
                Quaternion boltRotation = Quaternion.LookRotation(transform.forward, Vector3.up);

                Quaternion boltRotation2 = boltRotation * Diag2;

                Quaternion boltRotation1 = boltRotation * Diag;

                GameObject tempRef1 = Instantiate<GameObject>(m_LightPrefab, transform.position + (transform.forward * cam.nearClipPlane * 2f) + projectileOrigin, boltRotation);

                GameObject tempRef2 = Instantiate<GameObject>(m_LightPrefab, transform.position + (transform.forward * cam.nearClipPlane * 2f) + projectileOrigin1, boltRotation1);

                GameObject tempRef3 = Instantiate<GameObject>(m_LightPrefab, transform.position + (transform.forward * cam.nearClipPlane * 2f) + projectileOrigin2, boltRotation2);

                animator.SetTrigger("Cast");

                Vector3 direction = transform.forward;

                tempRef1.GetComponent<Damage>().m_level = level;
                tempRef2.GetComponent<Damage>().m_level = level;
                tempRef3.GetComponent<Damage>().m_level = level;

                //moves the knife forward
                tempRef1.GetComponent<Rigidbody>().AddForce(tempRef1.transform.forward * m_FiringForce, ForceMode.Impulse);
                tempRef2.GetComponent<Rigidbody>().AddForce(tempRef2.transform.forward * m_FiringForce, ForceMode.Impulse);
                tempRef3.GetComponent<Rigidbody>().AddForce(tempRef3.transform.forward * m_FiringForce, ForceMode.Impulse);
                reload = 1f;
            }
        }
    }
}
