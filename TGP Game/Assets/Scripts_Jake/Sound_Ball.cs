using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Ball : Weapon_Base
{
    private Animator animator;
    public GameObject m_ArrowPrefab;
    public float m_FiringForce = 10f;
    private Camera cam;

    public Vector3 projectileOrigin;
    public float X = 0, Y = 1, Z = 0;


    void Start()
    {
        cam = Camera.main;
        projectileOrigin = new Vector3(X, Y, Z);
        animator = GetComponent<Animator>();
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
                Quaternion arrowRotation = Quaternion.LookRotation(transform.forward, Vector3.up);

                GameObject tempRef = Instantiate<GameObject>(m_ArrowPrefab, transform.position + (transform.forward * cam.nearClipPlane * 2f) + projectileOrigin, arrowRotation);

                animator.SetTrigger("Cast");

                Vector3 direction = transform.forward;

                tempRef.GetComponent<Damage>().m_level = level;

                tempRef.GetComponent<Rigidbody>().AddForce(direction * m_FiringForce, ForceMode.Impulse);
                reload = 1f;
            }
        }
    }
}
