using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike_Growth : Weapon_Base
{
    private Animator animator;
    private Camera cam;
    [SerializeField] private GameObject m_spikesPrefab;

    [SerializeField] private Vector3 projectileOrigin;
    [SerializeField] private GameObject sword;

    void Start()
    {
        projectileOrigin = new Vector3(0, 3, 0);
        animator = GetComponent<Animator>();
        cam = Camera.main;
        level = 1;
        rTime = 0.2f;
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

                Quaternion spikeRotation = Quaternion.LookRotation(transform.forward, Vector3.up);
                Debug.Log(transform.position);
                GameObject tempRef = Instantiate<GameObject>(m_spikesPrefab, transform.position + projectileOrigin/* + (transform.forward * distance)*/, spikeRotation);

                tempRef.GetComponent<Damage>().m_level = level;
            }
        }
    }

    private void SwordReappear()
    {
        sword.SetActive(true);
    }
}
