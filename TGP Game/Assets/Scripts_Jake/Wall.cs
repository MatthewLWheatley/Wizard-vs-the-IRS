using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : Weapon_Base
{
    private Animator animator;
    private Camera cam;
    [SerializeField] private GameObject m_wallPrefab;
    [SerializeField] private float distance = 4;

    [SerializeField] private GameObject sword;

    void Start()
    {
        animator = GetComponent<Animator>();
        cam = Camera.main;
        level = 1;
        rTime = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        //this whole thing just triggers the animator and tracks reload time
        reload -= rTime * Time.deltaTime;
        if (reload <= 0)
        {
            reload = 0;
            if (Input.GetMouseButtonDown(fire))
            {
                animator.SetTrigger("Spike");
                reload = 1f;
                sword.SetActive(false);
                Invoke(nameof(SwordReappear), 2.1f);

                Quaternion wallRotation = Quaternion.LookRotation(-transform.forward, Vector3.up);

                GameObject tempRef = Instantiate<GameObject>(m_wallPrefab, transform.position + (transform.forward * distance), wallRotation);

                tempRef.GetComponent<Damage>().m_level = level;
            }
        }
    }

    private void SwordReappear()
    {
        sword.SetActive(true);
    }
}
